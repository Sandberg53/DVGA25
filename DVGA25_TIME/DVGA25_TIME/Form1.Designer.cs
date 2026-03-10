namespace DVGA25_TIME
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnReadFromQueue = new Button();
            rtbStatus = new RichTextBox();
            listViewEmployees = new ListView();
            txtEmployeeID = new TextBox();
            txtName = new TextBox();
            txtShortName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtExtent = new TextBox();
            txtDesignation = new TextBox();
            txtDepartment = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtJoinDate = new TextBox();
            txtEmail = new TextBox();
            txtAge = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            SuspendLayout();
            // 
            // btnReadFromQueue
            // 
            btnReadFromQueue.Location = new Point(274, 298);
            btnReadFromQueue.Name = "btnReadFromQueue";
            btnReadFromQueue.Size = new Size(155, 29);
            btnReadFromQueue.TabIndex = 1;
            btnReadFromQueue.Text = "Read from queue";
            btnReadFromQueue.UseVisualStyleBackColor = true;
            btnReadFromQueue.Click += btnReadFromQueue_Click;
            // 
            // rtbStatus
            // 
            rtbStatus.BackColor = SystemColors.Info;
            rtbStatus.Location = new Point(12, 591);
            rtbStatus.Name = "rtbStatus";
            rtbStatus.Size = new Size(419, 30);
            rtbStatus.TabIndex = 2;
            rtbStatus.Text = "";
            // 
            // listViewEmployees
            // 
            listViewEmployees.BackColor = SystemColors.Info;
            listViewEmployees.FullRowSelect = true;
            listViewEmployees.Location = new Point(13, 344);
            listViewEmployees.MultiSelect = false;
            listViewEmployees.Name = "listViewEmployees";
            listViewEmployees.Size = new Size(418, 241);
            listViewEmployees.TabIndex = 3;
            listViewEmployees.UseCompatibleStateImageBehavior = false;
            listViewEmployees.View = View.Details;
            listViewEmployees.SelectedIndexChanged += listViewEmployees_SelectedIndexChanged_1;
            // 
            // txtEmployeeID
            // 
            txtEmployeeID.BackColor = SystemColors.Info;
            txtEmployeeID.Location = new Point(17, 34);
            txtEmployeeID.Name = "txtEmployeeID";
            txtEmployeeID.ReadOnly = true;
            txtEmployeeID.Size = new Size(125, 27);
            txtEmployeeID.TabIndex = 4;
            // 
            // txtName
            // 
            txtName.BackColor = SystemColors.Info;
            txtName.Location = new Point(159, 34);
            txtName.Name = "txtName";
            txtName.ReadOnly = true;
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 5;
            // 
            // txtShortName
            // 
            txtShortName.BackColor = SystemColors.Info;
            txtShortName.Location = new Point(304, 34);
            txtShortName.Name = "txtShortName";
            txtShortName.ReadOnly = true;
            txtShortName.Size = new Size(125, 27);
            txtShortName.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 12);
            label1.Name = "label1";
            label1.Size = new Size(94, 20);
            label1.TabIndex = 7;
            label1.Text = "Employee ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(159, 12);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 8;
            label2.Text = "Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(304, 10);
            label3.Name = "label3";
            label3.Size = new Size(81, 20);
            label3.TabIndex = 9;
            label3.Text = "Shortname";
            // 
            // txtExtent
            // 
            txtExtent.BackColor = SystemColors.Info;
            txtExtent.Location = new Point(17, 92);
            txtExtent.Name = "txtExtent";
            txtExtent.ReadOnly = true;
            txtExtent.Size = new Size(125, 27);
            txtExtent.TabIndex = 10;
            // 
            // txtDesignation
            // 
            txtDesignation.BackColor = SystemColors.Info;
            txtDesignation.Location = new Point(160, 91);
            txtDesignation.Name = "txtDesignation";
            txtDesignation.ReadOnly = true;
            txtDesignation.Size = new Size(125, 27);
            txtDesignation.TabIndex = 11;
            // 
            // txtDepartment
            // 
            txtDepartment.BackColor = SystemColors.Info;
            txtDepartment.Location = new Point(304, 91);
            txtDepartment.Name = "txtDepartment";
            txtDepartment.ReadOnly = true;
            txtDepartment.Size = new Size(125, 27);
            txtDepartment.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 70);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 13;
            label4.Text = "Extent";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(160, 70);
            label5.Name = "label5";
            label5.Size = new Size(89, 20);
            label5.TabIndex = 14;
            label5.Text = "Designation";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(303, 70);
            label6.Name = "label6";
            label6.Size = new Size(89, 20);
            label6.TabIndex = 15;
            label6.Text = "Department";
            // 
            // txtJoinDate
            // 
            txtJoinDate.BackColor = SystemColors.Info;
            txtJoinDate.Location = new Point(17, 146);
            txtJoinDate.Name = "txtJoinDate";
            txtJoinDate.ReadOnly = true;
            txtJoinDate.Size = new Size(125, 27);
            txtJoinDate.TabIndex = 16;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = SystemColors.Info;
            txtEmail.Location = new Point(17, 200);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new Size(414, 27);
            txtEmail.TabIndex = 17;
            // 
            // txtAge
            // 
            txtAge.BackColor = SystemColors.Info;
            txtAge.Location = new Point(159, 146);
            txtAge.Name = "txtAge";
            txtAge.ReadOnly = true;
            txtAge.Size = new Size(125, 27);
            txtAge.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 124);
            label7.Name = "label7";
            label7.Size = new Size(69, 20);
            label7.TabIndex = 19;
            label7.Text = "Join date";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(163, 126);
            label8.Name = "label8";
            label8.Size = new Size(36, 20);
            label8.TabIndex = 20;
            label8.Text = "Age";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 180);
            label9.Name = "label9";
            label9.Size = new Size(46, 20);
            label9.TabIndex = 21;
            label9.Text = "Email";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(444, 633);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(txtAge);
            Controls.Add(txtEmail);
            Controls.Add(txtJoinDate);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtDepartment);
            Controls.Add(txtDesignation);
            Controls.Add(txtExtent);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtShortName);
            Controls.Add(txtName);
            Controls.Add(txtEmployeeID);
            Controls.Add(listViewEmployees);
            Controls.Add(rtbStatus);
            Controls.Add(btnReadFromQueue);
            Name = "Form1";
            Text = "TIME";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnReadFromQueue;
        private RichTextBox rtbStatus;
        private ListView listViewEmployees;
        private TextBox txtEmployeeID;
        private TextBox txtName;
        private TextBox txtShortName;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtExtent;
        private TextBox txtDesignation;
        private TextBox txtDepartment;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtJoinDate;
        private TextBox txtEmail;
        private TextBox txtAge;
        private Label label7;
        private Label label8;
        private Label label9;
    }
}
