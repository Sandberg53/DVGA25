using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace DVGA25_TIME
{
    public partial class Form1 : Form
    {
        private string strEmployeeDataFile = "";
        private Consumer consumer;
        public Form1()
        {
            InitializeComponent();
            ReadSettings();
            SetupListViewColumns();
            PopulateListViewFromFile();
            consumer = new Consumer();  

        }
        private void ReadSettings()
        {

            //LAB3: ändra eventuellt till annan sökväg till filen ("databasen"):
            TIPS: strEmployeeDataFile = System.AppDomain.CurrentDomain.BaseDirectory + "employee_time.xml";
            //strEmployeeDataFile = @"C:\TEMP\employee_time.xml";
        }
        private void PopulateListViewFromFile()
        {
            //ToDo: read from Queue and save in localfile
            if (!File.Exists(strEmployeeDataFile))
            {
                return;
            }
            try
            {
                listViewEmployees.Items.Clear();
                XmlDocument doc = new();
                doc.Load(strEmployeeDataFile);
                XmlNodeList? entities = doc.SelectNodes("/employees/employee"); //hämta ut alla "Employee"-element

                if (entities is not null && entities.Count > 0)
                {
                    foreach (XmlNode entity in entities) //loopa genom alla "Employee"-element i in-filen
                    {

                        ListViewItem item = new ListViewItem(entity["employee_id"]!.InnerText);
                        item.SubItems.Add(entity["name"]!.InnerText);
                        item.SubItems.Add(entity["extent"]!.InnerText);
                        item.SubItems.Add(entity["designation"]!.InnerText);
                        item.SubItems.Add(entity["department"]!.InnerText);
                        item.SubItems.Add(entity["join_date"]!.InnerText);
                        item.SubItems.Add(entity["email"]!.InnerText);
                        item.SubItems.Add(entity["age"]!.InnerText);
                        item.SubItems.Add(entity["shortname"]!.InnerText);
                        listViewEmployees.Items.Add(item);
                    }
                    listViewEmployees.Items[0].Selected = true; 
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Filen kunde inte öppnas eller var felaktig :" + ex.Message);
            }
        }

        private void SetupListViewColumns()
        {

            ColumnHeader header = new ColumnHeader();
            header.Text = "employee_id";
            header.Width = 70;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "name";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "extent";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "designation";
            header.Width = 150;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "department";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "join_date";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "email";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "age";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "shortname";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);


        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            //wait for loading of application:
            await WaitForIt(1000);

        }

        private async Task WaitForIt(int millisecs)
        {
            await Task.Delay(millisecs);
        }
        
        private void btnReadFromQueue_Click(object sender, EventArgs e)
        {


            try
            {
                consumer.Receive();

                //update data from "database":
                PopulateListViewFromFile();

            }
            catch (Exception ex)
            {
                rtbStatus.Text = ex.Message;

            }
        }

        private void listViewEmployees_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //fill textboxes with current selected: 
            //Assert row selected:
            if (listViewEmployees.SelectedItems.Count > 0)
            {
                txtEmployeeID.Text = listViewEmployees.SelectedItems[0].Text;
                txtName.Text = listViewEmployees.SelectedItems[0].SubItems[1].Text;
                txtExtent.Text = listViewEmployees.SelectedItems[0].SubItems[2].Text;
                txtDesignation.Text = listViewEmployees.SelectedItems[0].SubItems[3].Text;
                txtDepartment.Text = listViewEmployees.SelectedItems[0].SubItems[4].Text;
                txtJoinDate.Text = listViewEmployees.SelectedItems[0].SubItems[5].Text;
                txtEmail.Text = listViewEmployees.SelectedItems[0].SubItems[6].Text;
                txtAge.Text = listViewEmployees.SelectedItems[0].SubItems[7].Text;
                txtShortName.Text = listViewEmployees.SelectedItems[0].SubItems[8].Text;

            }

        }
    }
}
