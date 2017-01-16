namespace ArasBatchUploadCheck
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lab_password = new System.Windows.Forms.Label();
            this.lab_DB = new System.Windows.Forms.Label();
            this.lab_username = new System.Windows.Forms.Label();
            this.lab_serverurl = new System.Windows.Forms.Label();
            this.btn_disconnection = new System.Windows.Forms.Button();
            this.btn_ConnectionAras = new System.Windows.Forms.Button();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_DB = new System.Windows.Forms.TextBox();
            this.txt_serverurl = new System.Windows.Forms.TextBox();
            this.tre_Item = new System.Windows.Forms.TreeView();
            this.imageList_Item = new System.Windows.Forms.ImageList(this.components);
            this.txt_SearchAML = new System.Windows.Forms.TextBox();
            this.txt_SearchItem = new System.Windows.Forms.TextBox();
            this.btn_CheckItem = new System.Windows.Forms.Button();
            this.pro_CheckItem = new System.Windows.Forms.ProgressBar();
            this.btn_FixGarmentStyle = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lab_password);
            this.panel1.Controls.Add(this.lab_DB);
            this.panel1.Controls.Add(this.lab_username);
            this.panel1.Controls.Add(this.lab_serverurl);
            this.panel1.Controls.Add(this.btn_disconnection);
            this.panel1.Controls.Add(this.btn_ConnectionAras);
            this.panel1.Controls.Add(this.txt_username);
            this.panel1.Controls.Add(this.txt_password);
            this.panel1.Controls.Add(this.txt_DB);
            this.panel1.Controls.Add(this.txt_serverurl);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 74);
            this.panel1.TabIndex = 2;
            // 
            // lab_password
            // 
            this.lab_password.AutoSize = true;
            this.lab_password.Location = new System.Drawing.Point(364, 44);
            this.lab_password.Name = "lab_password";
            this.lab_password.Size = new System.Drawing.Size(53, 12);
            this.lab_password.TabIndex = 2;
            this.lab_password.Text = "PassWord";
            // 
            // lab_DB
            // 
            this.lab_DB.AutoSize = true;
            this.lab_DB.Location = new System.Drawing.Point(364, 17);
            this.lab_DB.Name = "lab_DB";
            this.lab_DB.Size = new System.Drawing.Size(41, 12);
            this.lab_DB.TabIndex = 2;
            this.lab_DB.Text = "DBName";
            // 
            // lab_username
            // 
            this.lab_username.AutoSize = true;
            this.lab_username.Location = new System.Drawing.Point(17, 44);
            this.lab_username.Name = "lab_username";
            this.lab_username.Size = new System.Drawing.Size(53, 12);
            this.lab_username.TabIndex = 2;
            this.lab_username.Text = "UserName";
            // 
            // lab_serverurl
            // 
            this.lab_serverurl.AutoSize = true;
            this.lab_serverurl.Location = new System.Drawing.Point(17, 17);
            this.lab_serverurl.Name = "lab_serverurl";
            this.lab_serverurl.Size = new System.Drawing.Size(59, 12);
            this.lab_serverurl.TabIndex = 2;
            this.lab_serverurl.Text = "ServerUrl";
            // 
            // btn_disconnection
            // 
            this.btn_disconnection.Location = new System.Drawing.Point(715, 39);
            this.btn_disconnection.Name = "btn_disconnection";
            this.btn_disconnection.Size = new System.Drawing.Size(87, 23);
            this.btn_disconnection.TabIndex = 1;
            this.btn_disconnection.Text = "Disconnection";
            this.btn_disconnection.UseVisualStyleBackColor = true;
            this.btn_disconnection.Click += new System.EventHandler(this.btn_disconnection_Click);
            // 
            // btn_ConnectionAras
            // 
            this.btn_ConnectionAras.Location = new System.Drawing.Point(715, 12);
            this.btn_ConnectionAras.Name = "btn_ConnectionAras";
            this.btn_ConnectionAras.Size = new System.Drawing.Size(87, 23);
            this.btn_ConnectionAras.TabIndex = 1;
            this.btn_ConnectionAras.Text = "Connection";
            this.btn_ConnectionAras.UseVisualStyleBackColor = true;
            this.btn_ConnectionAras.Click += new System.EventHandler(this.btn_ConnectionAras_Click);
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(82, 41);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(254, 21);
            this.txt_username.TabIndex = 0;
            this.txt_username.Text = "admin";
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(423, 41);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(254, 21);
            this.txt_password.TabIndex = 0;
            this.txt_password.Text = "innovator";
            // 
            // txt_DB
            // 
            this.txt_DB.Location = new System.Drawing.Point(423, 14);
            this.txt_DB.Name = "txt_DB";
            this.txt_DB.Size = new System.Drawing.Size(254, 21);
            this.txt_DB.TabIndex = 0;
            this.txt_DB.Text = "Esquel_PLM_SIT";
            // 
            // txt_serverurl
            // 
            this.txt_serverurl.Location = new System.Drawing.Point(82, 14);
            this.txt_serverurl.Name = "txt_serverurl";
            this.txt_serverurl.Size = new System.Drawing.Size(254, 21);
            this.txt_serverurl.TabIndex = 0;
            this.txt_serverurl.Text = "http://10.20.2.30/InnovatorServer/";
            // 
            // tre_Item
            // 
            this.tre_Item.ImageIndex = 0;
            this.tre_Item.ImageList = this.imageList_Item;
            this.tre_Item.Location = new System.Drawing.Point(12, 186);
            this.tre_Item.Name = "tre_Item";
            this.tre_Item.SelectedImageIndex = 0;
            this.tre_Item.Size = new System.Drawing.Size(741, 488);
            this.tre_Item.TabIndex = 7;
            // 
            // imageList_Item
            // 
            this.imageList_Item.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Item.ImageStream")));
            this.imageList_Item.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Item.Images.SetKeyName(0, "complete.png");
            this.imageList_Item.Images.SetKeyName(1, "start.png");
            this.imageList_Item.Images.SetKeyName(2, "wait.png");
            this.imageList_Item.Images.SetKeyName(3, "error.png");
            // 
            // txt_SearchAML
            // 
            this.txt_SearchAML.Location = new System.Drawing.Point(12, 92);
            this.txt_SearchAML.Multiline = true;
            this.txt_SearchAML.Name = "txt_SearchAML";
            this.txt_SearchAML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_SearchAML.Size = new System.Drawing.Size(406, 66);
            this.txt_SearchAML.TabIndex = 8;
            this.txt_SearchAML.Text = "<Item type=\"Garment\" action=\"get\">\r\n\t<item_number>{0}</item_number>\r\n</Item>";
            // 
            // txt_SearchItem
            // 
            this.txt_SearchItem.Location = new System.Drawing.Point(424, 92);
            this.txt_SearchItem.Multiline = true;
            this.txt_SearchItem.Name = "txt_SearchItem";
            this.txt_SearchItem.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_SearchItem.Size = new System.Drawing.Size(412, 66);
            this.txt_SearchItem.TabIndex = 8;
            // 
            // btn_CheckItem
            // 
            this.btn_CheckItem.Location = new System.Drawing.Point(759, 584);
            this.btn_CheckItem.Name = "btn_CheckItem";
            this.btn_CheckItem.Size = new System.Drawing.Size(75, 23);
            this.btn_CheckItem.TabIndex = 9;
            this.btn_CheckItem.Text = "Check";
            this.btn_CheckItem.UseVisualStyleBackColor = true;
            this.btn_CheckItem.Click += new System.EventHandler(this.btn_CheckItem_Click);
            // 
            // pro_CheckItem
            // 
            this.pro_CheckItem.Location = new System.Drawing.Point(11, 164);
            this.pro_CheckItem.Name = "pro_CheckItem";
            this.pro_CheckItem.Size = new System.Drawing.Size(823, 16);
            this.pro_CheckItem.TabIndex = 10;
            // 
            // btn_FixGarmentStyle
            // 
            this.btn_FixGarmentStyle.Location = new System.Drawing.Point(759, 617);
            this.btn_FixGarmentStyle.Name = "btn_FixGarmentStyle";
            this.btn_FixGarmentStyle.Size = new System.Drawing.Size(75, 23);
            this.btn_FixGarmentStyle.TabIndex = 11;
            this.btn_FixGarmentStyle.Text = "Fix";
            this.btn_FixGarmentStyle.UseVisualStyleBackColor = true;
            this.btn_FixGarmentStyle.Click += new System.EventHandler(this.btn_FixGarmentStyle_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(759, 651);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 12;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 686);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_FixGarmentStyle);
            this.Controls.Add(this.pro_CheckItem);
            this.Controls.Add(this.btn_CheckItem);
            this.Controls.Add(this.txt_SearchItem);
            this.Controls.Add(this.txt_SearchAML);
            this.Controls.Add(this.tre_Item);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMain";
            this.Text = "Aras Batch Upload Item Check";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lab_password;
        private System.Windows.Forms.Label lab_DB;
        private System.Windows.Forms.Label lab_username;
        private System.Windows.Forms.Label lab_serverurl;
        private System.Windows.Forms.Button btn_disconnection;
        private System.Windows.Forms.Button btn_ConnectionAras;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_DB;
        private System.Windows.Forms.TextBox txt_serverurl;
        private System.Windows.Forms.TreeView tre_Item;
        private System.Windows.Forms.TextBox txt_SearchAML;
        private System.Windows.Forms.TextBox txt_SearchItem;
        private System.Windows.Forms.Button btn_CheckItem;
        private System.Windows.Forms.ProgressBar pro_CheckItem;
        private System.Windows.Forms.ImageList imageList_Item;
        private System.Windows.Forms.Button btn_FixGarmentStyle;
        private System.Windows.Forms.Button btn_Cancel;
    }
}

