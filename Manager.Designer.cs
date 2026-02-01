
namespace DatabaseManager
{
    partial class Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manager));
            this.rjButton3 = new RJCodeAdvance.RJControls.RJButton();
            this.Browse = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatabaseLocation = new System.Windows.Forms.TextBox();
            this.txtTimestamp = new System.Windows.Forms.TextBox();
            this.GenerateScripts = new FontAwesome.Sharp.IconButton();
            this.Create = new FontAwesome.Sharp.IconButton();
            this.Backup = new FontAwesome.Sharp.IconButton();
            this.Restore = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.RunScripts = new FontAwesome.Sharp.IconButton();
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDebug = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // rjButton3
            // 
            this.rjButton3.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.rjButton3.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.rjButton3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton3.BorderRadius = 0;
            this.rjButton3.BorderSize = 0;
            this.rjButton3.FlatAppearance.BorderSize = 0;
            this.rjButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton3.ForeColor = System.Drawing.Color.White;
            this.rjButton3.Location = new System.Drawing.Point(130, 490);
            this.rjButton3.Name = "rjButton3";
            this.rjButton3.Size = new System.Drawing.Size(150, 40);
            this.rjButton3.TabIndex = 0;
            this.rjButton3.Text = "rjButton1";
            this.rjButton3.TextColor = System.Drawing.Color.White;
            this.rjButton3.UseVisualStyleBackColor = false;
            // 
            // Browse
            // 
            this.Browse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.Browse.FlatAppearance.BorderSize = 0;
            this.Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Browse.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Browse.ForeColor = System.Drawing.Color.White;
            this.Browse.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            this.Browse.IconColor = System.Drawing.Color.DarkGoldenrod;
            this.Browse.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.Browse.IconSize = 30;
            this.Browse.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Browse.Location = new System.Drawing.Point(3, 64);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(158, 32);
            this.Browse.TabIndex = 432;
            this.Browse.Text = "Location of Database";
            this.Browse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Browse.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Browse.UseVisualStyleBackColor = false;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 433;
            this.label1.Text = "Time Stamp:";
            // 
            // txtDatabaseLocation
            // 
            this.txtDatabaseLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabaseLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseLocation.Location = new System.Drawing.Point(169, 68);
            this.txtDatabaseLocation.Multiline = true;
            this.txtDatabaseLocation.Name = "txtDatabaseLocation";
            this.txtDatabaseLocation.Size = new System.Drawing.Size(250, 25);
            this.txtDatabaseLocation.TabIndex = 435;
            // 
            // txtTimestamp
            // 
            this.txtTimestamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimestamp.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimestamp.Location = new System.Drawing.Point(169, 116);
            this.txtTimestamp.Multiline = true;
            this.txtTimestamp.Name = "txtTimestamp";
            this.txtTimestamp.Size = new System.Drawing.Size(250, 25);
            this.txtTimestamp.TabIndex = 435;
            // 
            // GenerateScripts
            // 
            this.GenerateScripts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.GenerateScripts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateScripts.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateScripts.ForeColor = System.Drawing.Color.White;
            this.GenerateScripts.IconChar = FontAwesome.Sharp.IconChar.Code;
            this.GenerateScripts.IconColor = System.Drawing.Color.Orange;
            this.GenerateScripts.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.GenerateScripts.IconSize = 30;
            this.GenerateScripts.Location = new System.Drawing.Point(12, 165);
            this.GenerateScripts.Name = "GenerateScripts";
            this.GenerateScripts.Size = new System.Drawing.Size(92, 68);
            this.GenerateScripts.TabIndex = 436;
            this.GenerateScripts.Text = "Generate Scripts";
            this.GenerateScripts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.GenerateScripts.UseVisualStyleBackColor = true;
            this.GenerateScripts.Click += new System.EventHandler(this.GenerateTablesViews_Click);
            // 
            // Create
            // 
            this.Create.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Create.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Create.ForeColor = System.Drawing.Color.White;
            this.Create.IconChar = FontAwesome.Sharp.IconChar.FileCirclePlus;
            this.Create.IconColor = System.Drawing.Color.Magenta;
            this.Create.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Create.IconSize = 30;
            this.Create.Location = new System.Drawing.Point(134, 165);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(92, 68);
            this.Create.TabIndex = 436;
            this.Create.Text = "Create";
            this.Create.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Create.UseVisualStyleBackColor = true;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // Backup
            // 
            this.Backup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.Backup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Backup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Backup.ForeColor = System.Drawing.Color.White;
            this.Backup.IconChar = FontAwesome.Sharp.IconChar.Database;
            this.Backup.IconColor = System.Drawing.Color.RoyalBlue;
            this.Backup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Backup.IconSize = 30;
            this.Backup.Location = new System.Drawing.Point(326, 165);
            this.Backup.Name = "Backup";
            this.Backup.Size = new System.Drawing.Size(92, 68);
            this.Backup.TabIndex = 436;
            this.Backup.Text = "Backup";
            this.Backup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Backup.UseVisualStyleBackColor = true;
            this.Backup.Click += new System.EventHandler(this.BackupDatabase_Click);
            // 
            // Restore
            // 
            this.Restore.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.Restore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Restore.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Restore.ForeColor = System.Drawing.Color.White;
            this.Restore.IconChar = FontAwesome.Sharp.IconChar.Recycle;
            this.Restore.IconColor = System.Drawing.Color.LawnGreen;
            this.Restore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Restore.IconSize = 30;
            this.Restore.Location = new System.Drawing.Point(422, 165);
            this.Restore.Name = "Restore";
            this.Restore.Size = new System.Drawing.Size(92, 68);
            this.Restore.TabIndex = 436;
            this.Restore.Text = "Restore";
            this.Restore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Restore.UseVisualStyleBackColor = true;
            this.Restore.Click += new System.EventHandler(this.Restore_Click);
            // 
            // iconButton1
            // 
            this.iconButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this.iconButton1.IconColor = System.Drawing.Color.Red;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 30;
            this.iconButton1.Location = new System.Drawing.Point(230, 165);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(92, 68);
            this.iconButton1.TabIndex = 436;
            this.iconButton1.Text = "Drop";
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButton1.UseVisualStyleBackColor = true;
            this.iconButton1.Click += new System.EventHandler(this.Drop_Click);
            // 
            // RunScripts
            // 
            this.RunScripts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.RunScripts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RunScripts.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RunScripts.ForeColor = System.Drawing.Color.White;
            this.RunScripts.IconChar = FontAwesome.Sharp.IconChar.Play;
            this.RunScripts.IconColor = System.Drawing.Color.LawnGreen;
            this.RunScripts.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.RunScripts.IconSize = 30;
            this.RunScripts.Location = new System.Drawing.Point(518, 165);
            this.RunScripts.Name = "RunScripts";
            this.RunScripts.Size = new System.Drawing.Size(92, 68);
            this.RunScripts.TabIndex = 437;
            this.RunScripts.Text = "Run Scripts";
            this.RunScripts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RunScripts.UseVisualStyleBackColor = true;
            this.RunScripts.Click += new System.EventHandler(this.RunScripts_Click);
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDatabase.FormattingEnabled = true;
            this.cmbDatabase.ItemHeight = 15;
            this.cmbDatabase.Location = new System.Drawing.Point(169, 22);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(250, 23);
            this.cmbDatabase.TabIndex = 438;
            this.cmbDatabase.Text = "SAMPLE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 439;
            this.label3.Text = "Database Name:";
            // 
            // btnDebug
            // 
            this.btnDebug.FlatAppearance.BorderSize = 0;
            this.btnDebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDebug.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDebug.ForeColor = System.Drawing.Color.White;
            this.btnDebug.IconChar = FontAwesome.Sharp.IconChar.Recycle;
            this.btnDebug.IconColor = System.Drawing.Color.Aqua;
            this.btnDebug.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDebug.IconSize = 32;
            this.btnDebug.Location = new System.Drawing.Point(425, 16);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(115, 33);
            this.btnDebug.TabIndex = 440;
            this.btnDebug.Text = "Reload DBs";
            this.btnDebug.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDebug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(41)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(626, 294);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDatabase);
            this.Controls.Add(this.RunScripts);
            this.Controls.Add(this.Create);
            this.Controls.Add(this.Backup);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.Restore);
            this.Controls.Add(this.GenerateScripts);
            this.Controls.Add(this.txtTimestamp);
            this.Controls.Add(this.txtDatabaseLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.rjButton3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DBManager";
            this.Load += new System.EventHandler(this.Manager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private RJCodeAdvance.RJControls.RJButton rjButton3;
        private FontAwesome.Sharp.IconButton Browse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatabaseLocation;
        private System.Windows.Forms.TextBox txtTimestamp;
        private FontAwesome.Sharp.IconButton GenerateScripts;
        private FontAwesome.Sharp.IconButton Create;
        private FontAwesome.Sharp.IconButton Backup;
        private FontAwesome.Sharp.IconButton Restore;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton RunScripts;
        private System.Windows.Forms.ComboBox cmbDatabase;
        private System.Windows.Forms.Label label3;
        private FontAwesome.Sharp.IconButton btnDebug;
    }
}

