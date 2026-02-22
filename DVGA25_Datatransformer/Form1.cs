using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static DVGA25_Datatransformer.Form1;
using JsonSerializer = System.Text.Json.JsonSerializer;



namespace DVGA25_Datatransformer
{
    public class SkatteverkResponse
    {
        [JsonPropertyName("results")]
        public List<SkatteTabellRow> Results { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }
    }
    public partial class Form1 : Form
    {
        const string HR_FILENAME = "employee_master.csv";

        public Form1()
        {
            InitializeComponent();
        }

        // ===================== KNAPP-HÄNDELSER =====================
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                importFromSFTP(HR_FILENAME);
            }
            catch (Exception ex)
            {
                rtbImportStatus.Text += "Error in import: Exception: " + ex.Message + "\n";
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string? fileName;
            string filePathAndName;

            fileName = saveDataToFile();
            if (fileName != null)
            {
                filePathAndName = @"" + fileName;
                exportFileToSFTP(filePathAndName, fileName);
            }
            else
            {
                rtbExportStatus.Text = "Error: export file could not be created";
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            copyData();
        }

        private async void btnTransform_Click(object sender, EventArgs e)
        {
            copyData();

            transformCSVToXMLIntermediate();
            transformIntermediateToXML_Time();
            await transformIntermediateToXML_Salary(); // 🔹 async
        }

        // ===================== HJÄLPMETODER =====================
        private void copyData()
        {
            rtbOutput.Text = rtbInput.Text;
        }

        private string? saveDataToFile()
        {
            string tempData = rtbOutput.Text;
            string[] textLines = tempData.Split("\n");

            string kau_id = "danisand104";
            string fileDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            string destinationPathAndFileName = @"";
            string destinationFileName;

            try
            {
                destinationFileName = kau_id + "_" + fileDateTime + ".txt";
                destinationPathAndFileName += destinationFileName;

                rtbExportStatus.Text += "Destination filename: " + destinationFileName + "\n";
                StreamWriter sw = new StreamWriter(destinationPathAndFileName);
                rtbExportStatus.Text += "File Stream open. Writing " + textLines.Length.ToString() + " lines.\n";

                for (int i = 0; i < textLines.Length; i++)
                {
                    sw.WriteLine(textLines[i]);
                }
                sw.Close();

                return destinationFileName;
            }
            catch (Exception e)
            {
                rtbExportStatus.Text += "Error in saveDataToFile: Exception: " + e.Message + "\n";
            }
            return null;
        }

        private void exportFileToSFTP(string sourceFileAndPath, string destinationFileName)
        {
            string sshPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.ssh\id_ed25519";
            var privateKeyFile = new PrivateKeyFile(sshPath);
            string kau_ID = "danisand104";

            using SftpClient client = new("vortex.cse.kau.se", 22, kau_ID, privateKeyFile);

            try
            {
                rtbExportStatus.Text += "Connecting...\n";
                client.Connect();
                if (client.IsConnected)
                {
                    rtbExportStatus.Text += "Connected!\n";
                    string ts = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                    client.UploadFile(
                        File.OpenRead(@"salary.xml"),
                        $"out/salary_{kau_ID}_{ts}.xml"
                    );

                    client.UploadFile(
                        File.OpenRead(@"time.xml"),
                        $"out/time_{kau_ID}_{ts}.xml"
                    );

                    rtbExportStatus.Text += "File uploaded.\n";
                    client.Disconnect();
                    rtbExportStatus.Text += "Disconnected.\n";
                }
            }
            catch (Exception e) when (e is SshConnectionException || e is SocketException || e is ProxyException)
            {
                rtbExportStatus.Text += $"Error connecting to server: {e.Message}\n";
            }
            catch (SshAuthenticationException e)
            {
                rtbExportStatus.Text += $"Failed to authenticate: {e.Message}\n";
            }
            catch (SftpPermissionDeniedException e)
            {
                rtbExportStatus.Text += $"Operation denied by the server: {e.Message}\n";
            }
            catch (SshException e)
            {
                rtbExportStatus.Text += $"Sftp Error: {e.Message}\n";
            }
        }

        private void importFromSFTP(string filename)
        {
            string path = "in/" + filename;

            string sshPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.ssh\id_ed25519";
            var privateKeyFile = new PrivateKeyFile(sshPath);
            string kau_ID = "danisand104";

            using SftpClient client = new("vortex.cse.kau.se", 22, kau_ID, privateKeyFile);

            try
            {
                rtbImportStatus.Text += "Trying to connect to sftp \n";

                client.Connect();
                if (client.IsConnected)
                {
                    rtbImportStatus.Text += "Connected to sftp \n";
                    rtbImportStatus.Text += "Reading file " + path + "\n";
                    rtbInput.Text = client.ReadAllText(path);
                    rtbImportStatus.Text += "File read ok \n";
                    client.Disconnect();
                    rtbImportStatus.Text += "Disconnected \n";
                }
            }
            catch (Exception e) when (e is SshConnectionException || e is SocketException || e is ProxyException)
            {
                rtbImportStatus.Text += $"Error connecting to server: {e.Message}\n";
            }
            catch (SshAuthenticationException e)
            {
                rtbImportStatus.Text += $"Failed to authenticate: {e.Message}\n";
            }
            catch (SftpPermissionDeniedException e)
            {
                rtbImportStatus.Text += $"Operation denied by the server: {e.Message}\n";
            }
            catch (SshException e)
            {
                rtbImportStatus.Text += $"Sftp Error: {e.Message}\n";
            }
        }

        // ===================== TRANSFORMER =====================

        private void transformCSVToXMLIntermediate()
        {
            string[] source = rtbInput.Text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            XElement employees = new XElement("Employees");

            for (int i = 0; i < source.Length; i++)
            {
                string[] fields = source[i].Split(',');
                XElement employee = new XElement("Employee");
                employee.Add(new XElement("EmpID", fields[0]));
                employee.Add(new XElement("PID", fields[1]));
                employee.Add(new XElement("Firstname", fields[2]));
                employee.Add(new XElement("Middlename", fields[3]));
                employee.Add(new XElement("Lastname", fields[4]));
                employee.Add(new XElement("EmpType", fields[5]));
                employee.Add(new XElement("Extent", fields[6]));
                employee.Add(new XElement("Designation", fields[7]));
                employee.Add(new XElement("Birthdate", fields[8]));
                employee.Add(new XElement("Address", fields[9]));
                employee.Add(new XElement("AreaCode", fields[10]));
                employee.Add(new XElement("City", fields[11]));
                employee.Add(new XElement("Phone", fields[12]));
                employee.Add(new XElement("WorkEmail", fields[13]));
                employee.Add(new XElement("PrivateEmail", fields[14]));
                employee.Add(new XElement("JoinDate", fields[15]));
                employee.Add(new XElement("Department", fields[16]));
                employee.Add(new XElement("Bank", fields[17]));
                employee.Add(new XElement("Account", fields[18]));
                employee.Add(new XElement("AnualSalary", fields[19]));
                employee.Add(new XElement("VacDays", fields[20]));
                employee.Add(new XElement("DiverLicens", fields[21]));
                employee.Add(new XElement("DiverLicensType", fields[22]));

                employees.Add(employee);
            }
            employees.Save(@"intermediate.xml");
        }

        private void transformIntermediateToXML_Time()
        {
            try
            {
                XmlDocument doc = new();
                doc.Load(@"intermediate.xml");
                XmlNodeList? entities = doc.SelectNodes("/Employees/Employee");
                XElement time = new XElement("employees");

                if (entities != null)
                {
                    foreach (XmlNode e in entities)
                    {
                        XElement employee = new XElement("employee");
                        employee.Add(new XElement("employee_id", e["EmpID"]!.InnerText));
                        employee.Add(new XElement("name", e["Firstname"]!.InnerText + " " + e["Lastname"]!.InnerText));
                        employee.Add(new XElement("extent", e["Extent"]!.InnerText));
                        employee.Add(new XElement("designation", e["Designation"]!.InnerText));
                        employee.Add(new XElement("department", e["Department"]!.InnerText));
                        employee.Add(new XElement("join_date", e["JoinDate"]!.InnerText));
                        employee.Add(new XElement("email", e["WorkEmail"]?.InnerText ?? ""));

                        string birth = e["Birthdate"]!.InnerText;
                        DateTime birthDate = DateTime.ParseExact(birth, "yyyyMMdd", null);
                        int age = DateTime.Today.Year - birthDate.Year;
                        if (birthDate > DateTime.Today.AddYears(-age)) age--;
                        employee.Add(new XElement("age", age));

                        employee.Add(new XElement("shortname",
                            e["Firstname"]!.InnerText.Substring(0, 3) + e["Lastname"]!.InnerText.Substring(0, 3)));

                        time.Add(employee);
                    }
                }
                time.Save(@"time.xml");
            }
            catch (Exception ex)
            {
                rtbExportStatus.Text += "Time transform error: " + ex.Message + "\n";
            }
        }

        // ===================== SKATTETABELL & SALARY =====================

        
        

        public class SkatteTabellRow
        {
            [JsonPropertyName("inkomst fr.o.m.")]
            public string IncomeFromRaw { get; set; }

            [JsonPropertyName("inkomst t.o.m.")]
            public string IncomeToRaw { get; set; }

            [JsonPropertyName("kolumn 1")]
            public string Column1Raw { get; set; }

            // Parsed values
            public int IncomeFrom =>
                int.Parse(IncomeFromRaw, CultureInfo.InvariantCulture);

            public int IncomeTo =>
                int.Parse(IncomeToRaw, CultureInfo.InvariantCulture);

            public int Column1 =>
                int.Parse(Column1Raw, CultureInfo.InvariantCulture);
    }






        private async Task<List<SkatteTabellRow>> GetTaxTableAsync(HttpClient client, int tableNumber, int year)
        {
            int limit = 1000;
            int offset = 0;

            var allResults = new List<SkatteTabellRow>();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string url = $"https://skatteverket.entryscape.net/rowstore/dataset/88320397-5c32-4c16-ae79-d36d95b17b95?tabellnr={tableNumber}&%C3%A5r={year}&_limit={limit}&_offset={offset}";
            while (true)
            {

                var json = await client.GetStringAsync(url);

                var page = JsonSerializer.Deserialize<SkatteverkResponse>(json, options);
                if (!string.IsNullOrEmpty(page.Next))
                {
                    url = page.Next;
                    allResults.AddRange(page.Results);
                }
                else
                    break;
               

            }

            return allResults;
        }




        private int GetTaxReductionFromTable(int monthlySalary,List<SkatteTabellRow> taxTable)
        {
            foreach (var row in taxTable)
            {
                rtbExportStatus.Text += $"checking salery for row {row.IncomeFrom} ";
                if (monthlySalary >= row.IncomeFrom &&
                    monthlySalary <= row.IncomeTo)
                {
                    rtbExportStatus.Text += "tax reduction found!";
                    return row.Column1;
                }
            }

            throw new ArgumentOutOfRangeException(
                nameof(monthlySalary),
                $"Ingen tabellrad matchar lönen.");
        }


        private async Task transformIntermediateToXML_Salary()
        {
            try
            {
                XmlDocument doc = new();
                doc.Load(@"intermediate.xml");
                XmlNodeList? entities = doc.SelectNodes("/Employees/Employee");
                XElement salary = new XElement("employees");

                if (entities != null)
                {
                    using var client = new System.Net.Http.HttpClient();
                    var taxTable = await GetTaxTableAsync(client, 34, 2025);

                    foreach (XmlNode e in entities)
                    {
                        XElement employee = new XElement("employee");
                        employee.Add(new XElement("employee_id", e["EmpID"]!.InnerText));
                        employee.Add(new XElement("firstname", e["Firstname"]!.InnerText));
                        employee.Add(new XElement("lastname", e["Lastname"]!.InnerText));
                        employee.Add(new XElement("adress", e["Address"]?.InnerText ?? ""));
                        employee.Add(new XElement("city", e["City"]!.InnerText));
                        employee.Add(new XElement("extent", e["Extent"]!.InnerText));
                        employee.Add(new XElement("taxing", "34"));

                        int monthlySalary = int.Parse(e["AnualSalary"]!.InnerText) / 12;
                        employee.Add(new XElement("monthly_salary", monthlySalary));

                        int taxReduction = GetTaxReductionFromTable(monthlySalary, taxTable);
                        employee.Add(new XElement("tax_reduction", taxReduction));

                        employee.Add(new XElement("bank_name", e["Bank"]!.InnerText));
                        employee.Add(new XElement("bank_account", e["Account"]!.InnerText));
                        employee.Add(new XElement("vacation_days", e["VacDays"]!.InnerText));
                        employee.Add(new XElement("phone", e["Phone"]!.InnerText));
                        employee.Add(new XElement("email", e["WorkEmail"]?.InnerText ?? ""));
                        employee.Add(new XElement("birth_date", e["Birthdate"]!.InnerText));
                        employee.Add(new XElement("personal_id", e["PID"]!.InnerText));

                        salary.Add(employee);
                    }
                }

                salary.Save(@"salary.xml");
            }
            catch (Exception ex)
            {
                rtbExportStatus.Text += "Salary transform error: " + ex.Message + "\n";
            }
        }
    }
}
