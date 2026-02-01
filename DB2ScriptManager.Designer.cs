
namespace DatabaseManager
{
    partial class DB2ScriptManager
    {
            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
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
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DB2ScriptManager));
            this.tabControlWorkflow = new System.Windows.Forms.TabControl();
            this.tabPageConnect = new System.Windows.Forms.TabPage();
            this.btnDebug = new FontAwesome.Sharp.IconButton();
            this.btnConnect = new FontAwesome.Sharp.IconButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.btnClear = new FontAwesome.Sharp.IconButton();
            this.btnExecuteData = new FontAwesome.Sharp.IconButton();
            this.btnLoadDataScript = new FontAwesome.Sharp.IconButton();
            this.lblStatusData = new System.Windows.Forms.Label();
            this.txtDataScript = new System.Windows.Forms.TextBox();
            this.tabPageComplete = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnNext = new FontAwesome.Sharp.IconButton();
            this.btnPrevious = new FontAwesome.Sharp.IconButton();
            this.tabPageExcelToSQL = new System.Windows.Forms.TabPage();
            this.btnSaveSql = new System.Windows.Forms.Button();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGenerateSql = new System.Windows.Forms.Button();
            this.btnLoadExcel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clbSkipColumns = new System.Windows.Forms.CheckedListBox();
            this.txtSqlOutput = new System.Windows.Forms.TextBox();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tabControlWorkflow.SuspendLayout();
            this.tabPageConnect.SuspendLayout();
            this.tabPageData.SuspendLayout();
            this.tabPageComplete.SuspendLayout();
            this.tabPageExcelToSQL.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlWorkflow
            // 
            this.tabControlWorkflow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlWorkflow.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlWorkflow.Controls.Add(this.tabPageConnect);
            this.tabControlWorkflow.Controls.Add(this.tabPageData);
            this.tabControlWorkflow.Controls.Add(this.tabPageComplete);
            this.tabControlWorkflow.Controls.Add(this.tabPageExcelToSQL);
            this.tabControlWorkflow.ItemSize = new System.Drawing.Size(100, 34);
            this.tabControlWorkflow.Location = new System.Drawing.Point(0, 0);
            this.tabControlWorkflow.Name = "tabControlWorkflow";
            this.tabControlWorkflow.SelectedIndex = 0;
            this.tabControlWorkflow.Size = new System.Drawing.Size(815, 606);
            this.tabControlWorkflow.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControlWorkflow.TabIndex = 0;
            // 
            // tabPageConnect
            // 
            this.tabPageConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.tabPageConnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageConnect.Controls.Add(this.btnDebug);
            this.tabPageConnect.Controls.Add(this.btnConnect);
            this.tabPageConnect.Controls.Add(this.label6);
            this.tabPageConnect.Controls.Add(this.cmbDatabase);
            this.tabPageConnect.Controls.Add(this.lblStatus);
            this.tabPageConnect.Controls.Add(this.txtPassword);
            this.tabPageConnect.Controls.Add(this.txtUsername);
            this.tabPageConnect.Controls.Add(this.txtServer);
            this.tabPageConnect.Controls.Add(this.label3);
            this.tabPageConnect.Controls.Add(this.label2);
            this.tabPageConnect.Controls.Add(this.label1);
            this.tabPageConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageConnect.Location = new System.Drawing.Point(4, 38);
            this.tabPageConnect.Name = "tabPageConnect";
            this.tabPageConnect.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnect.Size = new System.Drawing.Size(807, 564);
            this.tabPageConnect.TabIndex = 0;
            this.tabPageConnect.Text = "Connect";
            // 
            // btnDebug
            // 
            this.btnDebug.FlatAppearance.BorderSize = 0;
            this.btnDebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDebug.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDebug.ForeColor = System.Drawing.Color.White;
            this.btnDebug.IconChar = FontAwesome.Sharp.IconChar.Database;
            this.btnDebug.IconColor = System.Drawing.Color.Aqua;
            this.btnDebug.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDebug.Location = new System.Drawing.Point(171, 169);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(79, 76);
            this.btnDebug.TabIndex = 19;
            this.btnDebug.Text = "Reload DBs";
            this.btnDebug.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDebug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.IconChar = FontAwesome.Sharp.IconChar.Connectdevelop;
            this.btnConnect.IconColor = System.Drawing.Color.SpringGreen;
            this.btnConnect.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConnect.Location = new System.Drawing.Point(78, 169);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(79, 76);
            this.btnConnect.TabIndex = 19;
            this.btnConnect.Text = "Connect";
            this.btnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(14, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "Databases";
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDatabase.FormattingEnabled = true;
            this.cmbDatabase.ItemHeight = 15;
            this.cmbDatabase.Location = new System.Drawing.Point(78, 12);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(172, 23);
            this.cmbDatabase.TabIndex = 16;
            this.cmbDatabase.Text = "SAMPLE";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(75, 265);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(90, 15);
            this.lblStatus.TabIndex = 15;
            this.lblStatus.Text = "Not connected";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(78, 128);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(172, 25);
            this.txtPassword.TabIndex = 13;
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(78, 88);
            this.txtUsername.Multiline = true;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(172, 25);
            this.txtUsername.TabIndex = 12;
            // 
            // txtServer
            // 
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Location = new System.Drawing.Point(78, 48);
            this.txtServer.Multiline = true;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(172, 25);
            this.txtServer.TabIndex = 11;
            this.txtServer.Text = "localhost";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Server";
            // 
            // tabPageData
            // 
            this.tabPageData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.tabPageData.Controls.Add(this.btnClear);
            this.tabPageData.Controls.Add(this.btnExecuteData);
            this.tabPageData.Controls.Add(this.btnLoadDataScript);
            this.tabPageData.Controls.Add(this.lblStatusData);
            this.tabPageData.Controls.Add(this.txtDataScript);
            this.tabPageData.Location = new System.Drawing.Point(4, 38);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageData.Size = new System.Drawing.Size(807, 564);
            this.tabPageData.TabIndex = 4;
            this.tabPageData.Text = "Load Data";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this.btnClear.IconColor = System.Drawing.Color.OrangeRed;
            this.btnClear.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClear.IconSize = 25;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(156, 530);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(142, 31);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExecuteData
            // 
            this.btnExecuteData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteData.FlatAppearance.BorderSize = 0;
            this.btnExecuteData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecuteData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecuteData.ForeColor = System.Drawing.Color.White;
            this.btnExecuteData.IconChar = FontAwesome.Sharp.IconChar.Play;
            this.btnExecuteData.IconColor = System.Drawing.Color.Lime;
            this.btnExecuteData.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExecuteData.IconSize = 25;
            this.btnExecuteData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecuteData.Location = new System.Drawing.Point(646, 526);
            this.btnExecuteData.Name = "btnExecuteData";
            this.btnExecuteData.Size = new System.Drawing.Size(152, 31);
            this.btnExecuteData.TabIndex = 22;
            this.btnExecuteData.Text = "Execute Data Script";
            this.btnExecuteData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecuteData.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnExecuteData.UseVisualStyleBackColor = true;
            this.btnExecuteData.Click += new System.EventHandler(this.btnExecuteData_Click);
            // 
            // btnLoadDataScript
            // 
            this.btnLoadDataScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadDataScript.FlatAppearance.BorderSize = 0;
            this.btnLoadDataScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadDataScript.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadDataScript.ForeColor = System.Drawing.Color.White;
            this.btnLoadDataScript.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            this.btnLoadDataScript.IconColor = System.Drawing.Color.DarkGoldenrod;
            this.btnLoadDataScript.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLoadDataScript.IconSize = 25;
            this.btnLoadDataScript.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadDataScript.Location = new System.Drawing.Point(8, 530);
            this.btnLoadDataScript.Name = "btnLoadDataScript";
            this.btnLoadDataScript.Size = new System.Drawing.Size(142, 31);
            this.btnLoadDataScript.TabIndex = 21;
            this.btnLoadDataScript.Text = "Load Script File";
            this.btnLoadDataScript.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadDataScript.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadDataScript.UseVisualStyleBackColor = true;
            this.btnLoadDataScript.Click += new System.EventHandler(this.btnLoadDataScript_Click);
            // 
            // lblStatusData
            // 
            this.lblStatusData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusData.AutoSize = true;
            this.lblStatusData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusData.ForeColor = System.Drawing.Color.Moccasin;
            this.lblStatusData.Location = new System.Drawing.Point(377, 538);
            this.lblStatusData.Name = "lblStatusData";
            this.lblStatusData.Size = new System.Drawing.Size(42, 15);
            this.lblStatusData.TabIndex = 8;
            this.lblStatusData.Text = "Status";
            // 
            // txtDataScript
            // 
            this.txtDataScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataScript.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtDataScript.Location = new System.Drawing.Point(8, -1);
            this.txtDataScript.MaxLength = 100000000;
            this.txtDataScript.Multiline = true;
            this.txtDataScript.Name = "txtDataScript";
            this.txtDataScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDataScript.Size = new System.Drawing.Size(791, 525);
            this.txtDataScript.TabIndex = 1;
            this.txtDataScript.WordWrap = false;
            // 
            // tabPageComplete
            // 
            this.tabPageComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.tabPageComplete.Controls.Add(this.label5);
            this.tabPageComplete.Location = new System.Drawing.Point(4, 38);
            this.tabPageComplete.Name = "tabPageComplete";
            this.tabPageComplete.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComplete.Size = new System.Drawing.Size(807, 564);
            this.tabPageComplete.TabIndex = 5;
            this.tabPageComplete.Text = "Complete";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Lime;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(316, 32);
            this.label5.TabIndex = 3;
            this.label5.Text = "Database Setup Complete!";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.IconChar = FontAwesome.Sharp.IconChar.ArrowRight;
            this.btnNext.IconColor = System.Drawing.Color.Lime;
            this.btnNext.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNext.IconSize = 32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(721, 626);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(81, 30);
            this.btnNext.TabIndex = 19;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.btnPrevious.IconColor = System.Drawing.Color.Lime;
            this.btnPrevious.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPrevious.IconSize = 32;
            this.btnPrevious.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrevious.Location = new System.Drawing.Point(12, 626);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(101, 30);
            this.btnPrevious.TabIndex = 19;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // tabPageExcelToSQL
            // 
            this.tabPageExcelToSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(43)))), ((int)(((byte)(57)))));
            this.tabPageExcelToSQL.Controls.Add(this.lblStatus2);
            this.tabPageExcelToSQL.Controls.Add(this.panel2);
            this.tabPageExcelToSQL.Controls.Add(this.panel1);
            this.tabPageExcelToSQL.Controls.Add(this.btnSaveSql);
            this.tabPageExcelToSQL.Controls.Add(this.btnCopyToClipboard);
            this.tabPageExcelToSQL.Controls.Add(this.txtTableName);
            this.tabPageExcelToSQL.Controls.Add(this.label4);
            this.tabPageExcelToSQL.Controls.Add(this.btnGenerateSql);
            this.tabPageExcelToSQL.Controls.Add(this.btnLoadExcel);
            this.tabPageExcelToSQL.ForeColor = System.Drawing.Color.White;
            this.tabPageExcelToSQL.Location = new System.Drawing.Point(4, 38);
            this.tabPageExcelToSQL.Name = "tabPageExcelToSQL";
            this.tabPageExcelToSQL.Size = new System.Drawing.Size(807, 564);
            this.tabPageExcelToSQL.TabIndex = 6;
            this.tabPageExcelToSQL.Text = "Excel To SQL";
            // 
            // btnSaveSql
            // 
            this.btnSaveSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSaveSql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSql.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSql.ForeColor = System.Drawing.Color.White;
            this.btnSaveSql.Location = new System.Drawing.Point(465, 11);
            this.btnSaveSql.Name = "btnSaveSql";
            this.btnSaveSql.Size = new System.Drawing.Size(100, 30);
            this.btnSaveSql.TabIndex = 13;
            this.btnSaveSql.Text = "Save SQL";
            this.btnSaveSql.UseVisualStyleBackColor = false;
            this.btnSaveSql.Click += new System.EventHandler(this.btnSaveSql_Click);
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyToClipboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCopyToClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyToClipboard.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyToClipboard.ForeColor = System.Drawing.Color.White;
            this.btnCopyToClipboard.Location = new System.Drawing.Point(571, 11);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(100, 30);
            this.btnCopyToClipboard.TabIndex = 12;
            this.btnCopyToClipboard.Text = "Copy SQL";
            this.btnCopyToClipboard.UseVisualStyleBackColor = false;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(185, 14);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(200, 20);
            this.txtTableName.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(43)))), ((int)(((byte)(57)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(111, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Table Name:";
            // 
            // btnGenerateSql
            // 
            this.btnGenerateSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateSql.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGenerateSql.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateSql.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateSql.ForeColor = System.Drawing.Color.White;
            this.btnGenerateSql.Location = new System.Drawing.Point(677, 11);
            this.btnGenerateSql.Name = "btnGenerateSql";
            this.btnGenerateSql.Size = new System.Drawing.Size(100, 30);
            this.btnGenerateSql.TabIndex = 9;
            this.btnGenerateSql.Text = "Generate SQL";
            this.btnGenerateSql.UseVisualStyleBackColor = false;
            this.btnGenerateSql.Click += new System.EventHandler(this.btnGenerateSql_Click);
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLoadExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadExcel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadExcel.ForeColor = System.Drawing.Color.White;
            this.btnLoadExcel.Location = new System.Drawing.Point(5, 11);
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.Size = new System.Drawing.Size(100, 30);
            this.btnLoadExcel.TabIndex = 8;
            this.btnLoadExcel.Text = "Load Excel";
            this.btnLoadExcel.UseVisualStyleBackColor = false;
            this.btnLoadExcel.Click += new System.EventHandler(this.btnLoadExcel_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.clbSkipColumns);
            this.panel1.Location = new System.Drawing.Point(8, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 289);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.txtSqlOutput);
            this.panel2.Location = new System.Drawing.Point(8, 342);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(790, 187);
            this.panel2.TabIndex = 15;
            // 
            // clbSkipColumns
            // 
            this.clbSkipColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbSkipColumns.CheckOnClick = true;
            this.clbSkipColumns.FormattingEnabled = true;
            this.clbSkipColumns.Location = new System.Drawing.Point(6, 26);
            this.clbSkipColumns.Name = "clbSkipColumns";
            this.clbSkipColumns.Size = new System.Drawing.Size(142, 244);
            this.clbSkipColumns.TabIndex = 3;
            // 
            // txtSqlOutput
            // 
            this.txtSqlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSqlOutput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSqlOutput.Location = new System.Drawing.Point(0, 0);
            this.txtSqlOutput.Multiline = true;
            this.txtSqlOutput.Name = "txtSqlOutput";
            this.txtSqlOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSqlOutput.Size = new System.Drawing.Size(790, 187);
            this.txtSqlOutput.TabIndex = 4;
            this.txtSqlOutput.WordWrap = false;
            // 
            // lblStatus2
            // 
            this.lblStatus2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus2.Location = new System.Drawing.Point(8, 532);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(787, 23);
            this.lblStatus2.TabIndex = 20;
            this.lblStatus2.Text = "Ready";
            this.lblStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(160, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(627, 261);
            this.dataGridView1.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Skip Columns";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(203, 618);
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(439, 31);
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Visible = false;
            // 
            // DB2ScriptManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(814, 661);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.tabControlWorkflow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DB2ScriptManager";
            this.Text = "DB2ScriptManager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControlWorkflow.ResumeLayout(false);
            this.tabPageConnect.ResumeLayout(false);
            this.tabPageConnect.PerformLayout();
            this.tabPageData.ResumeLayout(false);
            this.tabPageData.PerformLayout();
            this.tabPageComplete.ResumeLayout(false);
            this.tabPageComplete.PerformLayout();
            this.tabPageExcelToSQL.ResumeLayout(false);
            this.tabPageExcelToSQL.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.TabControl tabControlWorkflow;
            private System.Windows.Forms.TabPage tabPageConnect;
            private System.Windows.Forms.TabPage tabPageData;
            private System.Windows.Forms.TabPage tabPageComplete;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.TextBox txtDataScript;
            private System.Windows.Forms.Label lblStatus;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.OpenFileDialog openFileDialog;
            private System.Windows.Forms.Label label6;
            private System.Windows.Forms.Label lblStatusData;
            private FontAwesome.Sharp.IconButton btnDebug;
            private FontAwesome.Sharp.IconButton btnConnect;
            private FontAwesome.Sharp.IconButton btnNext;
            private FontAwesome.Sharp.IconButton btnPrevious;
            private System.Windows.Forms.ComboBox cmbDatabase;
            private System.Windows.Forms.TextBox txtPassword;
            private System.Windows.Forms.TextBox txtUsername;
            private System.Windows.Forms.TextBox txtServer;
            private FontAwesome.Sharp.IconButton btnLoadDataScript;
            private FontAwesome.Sharp.IconButton btnExecuteData;
        private FontAwesome.Sharp.IconButton btnClear;
        private System.Windows.Forms.TabPage tabPageExcelToSQL;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSqlOutput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckedListBox clbSkipColumns;
        private System.Windows.Forms.Button btnSaveSql;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGenerateSql;
        private System.Windows.Forms.Button btnLoadExcel;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }



    }