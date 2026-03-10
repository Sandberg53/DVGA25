using System.Xml.Linq;
using System.Xml;
using System.Windows.Forms;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace DVGA25_Salary
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

            //LAB3: använd eventuellt egen sökväg
            strEmployeeDataFile = System.AppDomain.CurrentDomain.BaseDirectory + "employee_salary.xml";
            //strEmployeeDataFile = @"C:\TEMP\employee_salary.xml";
        }
        private void SetupListViewColumns()
        {
            //ToDo: sync columns:

            ColumnHeader header = new ColumnHeader();
            header.Text = "employee_id";
            header.Width = 70;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "firstname";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "lastname";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "address";
            header.Width = 150;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "city";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "extent";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "taxcode";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "monthly_salary";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "tax_reduction";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "bank_name";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "bank_account";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "vacation_days";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "phone";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "email";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "birth_date";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);
            header = new ColumnHeader();
            header.Text = "personal_id";
            header.Width = 100;
            header.TextAlign = HorizontalAlignment.Left;
            listViewEmployees.Columns.Add(header);


        }
        private void PopulateListViewFromFile()
        {
            //ToDo: read from Queue and save in localfile
            if (!File.Exists(strEmployeeDataFile))
            {
                MessageBox.Show(strEmployeeDataFile);
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
                        item.SubItems.Add(entity["firstname"]!.InnerText);
                        item.SubItems.Add(entity["lastname"]!.InnerText);
                        item.SubItems.Add(entity["address"]!.InnerText);
                        item.SubItems.Add(entity["city"]!.InnerText);
                        item.SubItems.Add(entity["extent"]!.InnerText);
                        item.SubItems.Add(entity["taxcode"]!.InnerText);
                        item.SubItems.Add(entity["monthly_salary"]!.InnerText);
                        item.SubItems.Add(entity["tax_reduction"]!.InnerText);
                        item.SubItems.Add(entity["bank_name"]!.InnerText);
                        item.SubItems.Add(entity["bank_account"]!.InnerText);
                        item.SubItems.Add(entity["vacation_days"]!.InnerText);
                        item.SubItems.Add(entity["phone"]!.InnerText);
                        item.SubItems.Add(entity["email"]!.InnerText);
                        item.SubItems.Add(entity["birth_date"]!.InnerText);
                        item.SubItems.Add(entity["personal_id"]!.InnerText);
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


        private void listViewEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fill textboxes with current selected: 
            //Assert row selected:
            if (listViewEmployees.SelectedItems.Count > 0)
            {
                txtEmpID.Text = listViewEmployees.SelectedItems[0].Text;
                txtFirstname.Text = listViewEmployees.SelectedItems[0].SubItems[1].Text;
                txtLastname.Text = listViewEmployees.SelectedItems[0].SubItems[2].Text;
                txtAddress.Text = listViewEmployees.SelectedItems[0].SubItems[3].Text;
                txtCity.Text = listViewEmployees.SelectedItems[0].SubItems[4].Text;
                txtExtent.Text = listViewEmployees.SelectedItems[0].SubItems[5].Text;
                txtTaxCode.Text = listViewEmployees.SelectedItems[0].SubItems[6].Text;
                txtSalary.Text = listViewEmployees.SelectedItems[0].SubItems[7].Text;
                txtTax.Text = listViewEmployees.SelectedItems[0].SubItems[8].Text;
                txtBank.Text = listViewEmployees.SelectedItems[0].SubItems[9].Text;
                txtAccount.Text = listViewEmployees.SelectedItems[0].SubItems[10].Text;
                txtVacation.Text = listViewEmployees.SelectedItems[0].SubItems[11].Text;
                txtPhone.Text = listViewEmployees.SelectedItems[0].SubItems[12].Text;
                txtEmail.Text = listViewEmployees.SelectedItems[0].SubItems[13].Text;
                txtBirthDate.Text = listViewEmployees.SelectedItems[0].SubItems[14].Text;
                txtPersonalID.Text = listViewEmployees.SelectedItems[0].SubItems[15].Text;

            }

        }

        private void btnReadQueue_Click(object sender, EventArgs e)
        {

            try
            {
                //read from queue:
                consumer.Receive();

                //update data from "database":
                PopulateListViewFromFile();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ ex.Message);
            }
          
        }
    }
}

