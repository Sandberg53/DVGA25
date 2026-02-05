using System.Collections;
using System.Collections.Immutable;
using System.Data;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Renci;
using Renci.SshNet;
using Renci.SshNet.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVGA25_Datatransformer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                importFromSFTP("Employee.csv");
                //rtbInput.Text = File.ReadAllText(@"C:\Temp\intermediate.xml");
                //rtbInput.Text = File.ReadAllText(@"C:\Temp\Employee.csv");
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

            fileName = saveDataToFile(); //spara undan data till fil inför export
            if (fileName != null)
            {
                //TODO LAB1: byt eventuellt filsökväg till filen som ska exporteras
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

        private void copyData()
        {
            rtbOutput.Text = rtbInput.Text;
        }
        private string? saveDataToFile()
        {
            //spara data i "exportfönstret" till fil:
            string tempData = rtbOutput.Text;
            string[] textLines = tempData.Split("\n");
            
            //TODO LAB1: ändra till ditt kau-id:
            string kau_id = "danisand104";
            string fileDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            
            //TODO LAB1: byt eventuellt filsökväg till filen som ska exporteras 
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
            finally
            {
            }
            return null; //returnera null om filen inte kunde sparas
        }
        private void exportFileToSFTP(string sourceFileAndPath, string destinationFileName)
        {
            //TODO LAB1: peka ut din privata SSH-nyckel och byt ut till ditt kau-id 
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
                    client.UploadFile(File.OpenRead(@sourceFileAndPath), "out/" + destinationFileName);
                    rtbExportStatus.Text += "File uploaded.\n";

                    //kod för att lista innehållet i "out" mappen om man önskar:
                    //foreach (var sftpFile in client.ListDirectory("out"))
                    //{
                    //   rtbExportStatus.Text += $"\t{sftpFile.FullName}\n";
                    //}
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

            //TODO LAB1: använd dina inloggningsuppgifter
            string sshPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.ssh\id_ed25519";
            var privateKeyFile = new PrivateKeyFile(sshPath);
            string kau_ID = "danisand104";
            //----------------------------------------------------
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

        private void btnTransform_Click(object sender, EventArgs e)
        {

            //LAB1: kopiera den importerade filen 
            copyData();


            //LAB2 a):
            //transformCSVToXMLIntermediate();
            //LAB2 b):
            //transformIntermediateToXML();
 

        }
        private void transformIntermediateToXML()
        {
            try { 
                XmlDocument doc = new();
                doc.Load(@"C:\Temp\intermediate.xml");
                XmlNodeList? entities = doc.SelectNodes("/Employees/Employee"); //hämta ut alla "Employee"-element

                XElement employees = new XElement("employees"); //"Root"-elementet för ut-filen
                if(entities is not null && entities.Count > 0) 
                { 
                    foreach (XmlNode entity in entities) //loopa genom alla "Employee"-element i in-filen
                    {
                        string employee_id = entity["EmpID"]!.InnerText;
                        string name = entity["Firstname"]!.InnerText + " " + entity["Lastname"]!.InnerText; 
                        XElement employee = new XElement("employee"); //barn till "Root"-elementet ("Employees)
                        employee.Add(new XElement("employee_id", employee_id));
                        employee.Add(new XElement("name", name));
                        employees.Add(employee);
                    }
                }
 
                rtbOutput.Text = employees.ToString();
                employees.Save(@"C:\Temp\systemB.xml");
                }
            catch (Exception e)
            {
                rtbExportStatus.Text += "Error in formIntermediateToXML_App: Exception: " + e.Message + "\n";
            }
        }

        private void transformFlatfileToXML_App()
        {
            //string rtbText = rtbInput.Text;
            //string[] source = rtbText.Split('\n'); //dela upp varje rad i flatfilen


            string[] source = File.ReadAllLines(@"C:\Temp\Employee.txt");
           
            XElement employees = new XElement("Employees"); //"Root"-elementet

            for (int i = 0; i < source.Length; i++)
            {
                XElement employee = new XElement("Employee"); //barn till "Root"-elementet ("Employees)
                employee.Add(new XElement("EmpID", source[i].Substring(0, 3)));
                employee.Add(new XElement("Firstname", source[i].Substring(3, 10).TrimEnd()));
                employee.Add(new XElement("Lastname", source[i].Substring(13, 12).TrimEnd()));
                employee.Add(new XElement("Birthdate", source[i].Substring(25, 6)));
                employee.Add(new XElement("Address", source[i].Substring(31, 15).TrimEnd()));
                employee.Add(new XElement("City", source[i].Substring(46, 10).TrimEnd()));
                employee.Add(new XElement("Phone", source[i].Substring(56, 8).TrimEnd()));
                employee.Add(new XElement("Email", source[i].Substring(64, 30).TrimEnd()));
                employees.Add(employee);
            }
            employees.Save(@"C:\Temp\intermediate.xml");


            rtbOutput.Text = employees.ToString();

        }

        private void transformCSVToXML_App()
        {
            string rtbText = rtbInput.Text;

            string[] source = rtbText.Split('\n');
            XElement employees = new XElement("Employees"); //"Root"-elementet

            for (int i = 0; i < source.Length; i++)
            {
                string[] fields = source[i].Split(',');
                XElement employee = new XElement("Employee"); //barn till "Root"-elementet ("Employees)
                employee.Add(new XElement("EmpID", fields[0]));
                employee.Add(new XElement("Firstname", fields[1]));
                employee.Add(new XElement("Lastname", fields[2]));
                employee.Add(new XElement("Birthdate", fields[3]));
                employee.Add(new XElement("Address", fields[4]));
                employee.Add(new XElement("City", fields[5]));
                employee.Add(new XElement("Phone", fields[6]));
                employee.Add(new XElement("Email", fields[7]));
                employees.Add(employee);
            }
            rtbOutput.Text = employees.ToString();

        }
        private void transformCSVToXMLIntermediate()
        {
            string[] source = File.ReadAllLines(@"C:\Temp\Employee.csv");
            XElement employees = new XElement("Employees"); //"Root"-elementet

            for (int i = 0; i < source.Length; i++)
            {
                string[] fields = source[i].Split(',');
                XElement employee = new XElement("Employee"); //barn till "Root"-elementet ("Employees)
                employee.Add(new XElement("EmpID", fields[0]));
                employee.Add(new XElement("Firstname", fields[1]));
                employee.Add(new XElement("Lastname", fields[2]));
                employee.Add(new XElement("Birthdate", fields[3]));
                employee.Add(new XElement("Address", fields[4]));
                employee.Add(new XElement("City", fields[5]));
                employee.Add(new XElement("Phone", fields[6]));
                employee.Add(new XElement("Email", fields[7]));
                employees.Add(employee);
            }
            employees.Save(@"C:\Temp\intermediate.xml");
            
        }

        private XmlDocument JsonToXML(string json)
        {
            XmlDocument doc = new XmlDocument();

            using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max))
            {
                XElement xml = XElement.Load(reader);
                doc.LoadXml(xml.ToString());
            }

            return doc;
        }
        private void transform_XMLToJson()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Temp\systemB.xml");
            foreach (XmlNode node in doc)
            {
                if (node.NodeType == XmlNodeType.XmlDeclaration) 
                {
                    doc.RemoveChild(node); //ta bort xml-deklarationen (första raden) innan konvertering
                }
            }
            string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

            rtbOutput.Text = json;
        }
    }
}
