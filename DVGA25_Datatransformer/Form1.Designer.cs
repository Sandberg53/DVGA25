namespace DVGA25_Datatransformer
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
            rtbInput = new RichTextBox();
            rtbOutput = new RichTextBox();
            btnImport = new Button();
            btnExport = new Button();
            btnCopy = new Button();
            btnTransform = new Button();
            label1 = new Label();
            label2 = new Label();
            rtbImportStatus = new RichTextBox();
            rtbExportStatus = new RichTextBox();
            SuspendLayout();
            // 
            // rtbInput
            // 
            rtbInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbInput.BackColor = SystemColors.ControlLightLight;
            rtbInput.Location = new Point(14, 48);
            rtbInput.Margin = new Padding(3, 4, 3, 4);
            rtbInput.Name = "rtbInput";
            rtbInput.ReadOnly = true;
            rtbInput.Size = new Size(483, 534);
            rtbInput.TabIndex = 0;
            rtbInput.Text = "";
            rtbInput.WordWrap = false;
            // 
            // rtbOutput
            // 
            rtbOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbOutput.BackColor = SystemColors.ControlLightLight;
            rtbOutput.Location = new Point(645, 48);
            rtbOutput.Margin = new Padding(3, 4, 3, 4);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(484, 534);
            rtbOutput.TabIndex = 1;
            rtbOutput.Text = "";
            rtbOutput.WordWrap = false;
            // 
            // btnImport
            // 
            btnImport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnImport.Location = new Point(14, 590);
            btnImport.Margin = new Padding(3, 4, 3, 4);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(143, 31);
            btnImport.TabIndex = 2;
            btnImport.Text = "Import from SFTP";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExport.Location = new Point(974, 588);
            btnExport.Margin = new Padding(3, 4, 3, 4);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(155, 31);
            btnExport.TabIndex = 3;
            btnExport.Text = "Export file to SFTP";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnCopy
            // 
            btnCopy.Location = new Point(504, 48);
            btnCopy.Margin = new Padding(3, 4, 3, 4);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(134, 31);
            btnCopy.TabIndex = 4;
            btnCopy.Text = "Copy >>";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // btnTransform
            // 
            btnTransform.Location = new Point(504, 343);
            btnTransform.Margin = new Padding(3, 4, 3, 4);
            btnTransform.Name = "btnTransform";
            btnTransform.Size = new Size(134, 31);
            btnTransform.TabIndex = 5;
            btnTransform.Text = "> Transform >";
            btnTransform.UseVisualStyleBackColor = true;
            btnTransform.Click += btnTransform_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 24);
            label1.Name = "label1";
            label1.Size = new Size(21, 20);
            label1.TabIndex = 6;
            label1.Text = "In";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(647, 24);
            label2.Name = "label2";
            label2.Size = new Size(33, 20);
            label2.TabIndex = 7;
            label2.Text = "Out";
            // 
            // rtbImportStatus
            // 
            rtbImportStatus.BackColor = SystemColors.Info;
            rtbImportStatus.Location = new Point(14, 627);
            rtbImportStatus.Margin = new Padding(3, 4, 3, 4);
            rtbImportStatus.Name = "rtbImportStatus";
            rtbImportStatus.ReadOnly = true;
            rtbImportStatus.Size = new Size(483, 145);
            rtbImportStatus.TabIndex = 8;
            rtbImportStatus.Text = "";
            // 
            // rtbExportStatus
            // 
            rtbExportStatus.BackColor = SystemColors.Info;
            rtbExportStatus.Location = new Point(647, 626);
            rtbExportStatus.Margin = new Padding(3, 4, 3, 4);
            rtbExportStatus.Name = "rtbExportStatus";
            rtbExportStatus.Size = new Size(483, 145);
            rtbExportStatus.TabIndex = 9;
            rtbExportStatus.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 779);
            Controls.Add(rtbExportStatus);
            Controls.Add(rtbImportStatus);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnTransform);
            Controls.Add(btnCopy);
            Controls.Add(btnExport);
            Controls.Add(btnImport);
            Controls.Add(rtbOutput);
            Controls.Add(rtbInput);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "DVGA25 Data Transformer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbInput;
        private RichTextBox rtbOutput;
        private Button btnImport;
        private Button btnExport;
        private Button btnCopy;
        private Button btnTransform;
        private Label label1;
        private Label label2;
        private RichTextBox rtbImportStatus;
        private RichTextBox rtbExportStatus;
    }
}
