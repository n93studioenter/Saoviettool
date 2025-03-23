namespace SaovietTool
{
    partial class frmMain
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
            txtPath = new MaterialSkin.Controls.MaterialTextBox();
            materialListView1 = new MaterialSkin.Controls.MaterialListView();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            btnCapnhat = new MaterialSkin.Controls.MaterialButton();
            materialComboBox1 = new MaterialSkin.Controls.MaterialComboBox();
            materialComboBox2 = new MaterialSkin.Controls.MaterialComboBox();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            btnLoc = new MaterialSkin.Controls.MaterialButton();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPath.AnimateReadOnly = false;
            txtPath.BorderStyle = BorderStyle.None;
            txtPath.Depth = 0;
            txtPath.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtPath.LeadingIcon = null;
            txtPath.Location = new Point(112, 24);
            txtPath.MaxLength = 50;
            txtPath.MouseState = MaterialSkin.MouseState.OUT;
            txtPath.Multiline = false;
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(978, 50);
            txtPath.TabIndex = 0;
            txtPath.Text = "";
            txtPath.TrailingIcon = null;
            // 
            // materialListView1
            // 
            materialListView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            materialListView1.AutoSizeTable = false;
            materialListView1.BackColor = Color.FromArgb(255, 255, 255);
            materialListView1.BorderStyle = BorderStyle.None;
            materialListView1.Depth = 0;
            materialListView1.FullRowSelect = true;
            materialListView1.Location = new Point(3, 245);
            materialListView1.MinimumSize = new Size(200, 100);
            materialListView1.MouseLocation = new Point(-1, -1);
            materialListView1.MouseState = MaterialSkin.MouseState.OUT;
            materialListView1.Name = "materialListView1";
            materialListView1.OwnerDraw = true;
            materialListView1.Size = new Size(1276, 438);
            materialListView1.TabIndex = 1;
            materialListView1.UseCompatibleStateImageBehavior = false;
            materialListView1.View = View.Details;
            materialListView1.DoubleClick += materialListView1_DoubleClick;
            materialListView1.MouseClick += materialListView1_MouseClick;
            materialListView1.MouseDoubleClick += materialListView1_MouseDoubleClick;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(12, 43);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(80, 19);
            materialLabel1.TabIndex = 2;
            materialLabel1.Text = "Đường dẫn";
            // 
            // btnCapnhat
            // 
            btnCapnhat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCapnhat.AutoSize = false;
            btnCapnhat.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCapnhat.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnCapnhat.Depth = 0;
            btnCapnhat.HighEmphasis = true;
            btnCapnhat.Icon = null;
            btnCapnhat.Location = new Point(1102, 24);
            btnCapnhat.Margin = new Padding(4, 6, 4, 6);
            btnCapnhat.MouseState = MaterialSkin.MouseState.HOVER;
            btnCapnhat.Name = "btnCapnhat";
            btnCapnhat.NoAccentTextColor = Color.Empty;
            btnCapnhat.Size = new Size(157, 50);
            btnCapnhat.TabIndex = 3;
            btnCapnhat.Text = "Cập nhật";
            btnCapnhat.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnCapnhat.UseAccentColor = false;
            btnCapnhat.UseVisualStyleBackColor = true;
            btnCapnhat.Click += btnCapnhat_Click;
            // 
            // materialComboBox1
            // 
            materialComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            materialComboBox1.AutoResize = false;
            materialComboBox1.BackColor = Color.FromArgb(255, 255, 255);
            materialComboBox1.Depth = 0;
            materialComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            materialComboBox1.DropDownHeight = 174;
            materialComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBox1.DropDownWidth = 121;
            materialComboBox1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialComboBox1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialComboBox1.FormattingEnabled = true;
            materialComboBox1.IntegralHeight = false;
            materialComboBox1.ItemHeight = 43;
            materialComboBox1.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            materialComboBox1.Location = new Point(482, 95);
            materialComboBox1.MaxDropDownItems = 4;
            materialComboBox1.MouseState = MaterialSkin.MouseState.OUT;
            materialComboBox1.Name = "materialComboBox1";
            materialComboBox1.Size = new Size(226, 49);
            materialComboBox1.StartIndex = 0;
            materialComboBox1.TabIndex = 4;
            // 
            // materialComboBox2
            // 
            materialComboBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            materialComboBox2.AutoResize = false;
            materialComboBox2.BackColor = Color.FromArgb(255, 255, 255);
            materialComboBox2.Depth = 0;
            materialComboBox2.DrawMode = DrawMode.OwnerDrawVariable;
            materialComboBox2.DropDownHeight = 174;
            materialComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBox2.DropDownWidth = 121;
            materialComboBox2.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialComboBox2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialComboBox2.FormattingEnabled = true;
            materialComboBox2.IntegralHeight = false;
            materialComboBox2.ItemHeight = 43;
            materialComboBox2.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            materialComboBox2.Location = new Point(864, 95);
            materialComboBox2.MaxDropDownItems = 4;
            materialComboBox2.MouseState = MaterialSkin.MouseState.OUT;
            materialComboBox2.Name = "materialComboBox2";
            materialComboBox2.Size = new Size(226, 49);
            materialComboBox2.StartIndex = 0;
            materialComboBox2.TabIndex = 5;
            // 
            // materialLabel2
            // 
            materialLabel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel2.Location = new Point(392, 112);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(66, 19);
            materialLabel2.TabIndex = 6;
            materialLabel2.Text = "Từ tháng";
            // 
            // materialLabel3
            // 
            materialLabel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel3.Location = new Point(769, 112);
            materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(74, 19);
            materialLabel3.TabIndex = 7;
            materialLabel3.Text = "Đến tháng";
            // 
            // btnLoc
            // 
            btnLoc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoc.AutoSize = false;
            btnLoc.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnLoc.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnLoc.Depth = 0;
            btnLoc.HighEmphasis = true;
            btnLoc.Icon = null;
            btnLoc.Location = new Point(1102, 95);
            btnLoc.Margin = new Padding(4, 6, 4, 6);
            btnLoc.MouseState = MaterialSkin.MouseState.HOVER;
            btnLoc.Name = "btnLoc";
            btnLoc.NoAccentTextColor = Color.Empty;
            btnLoc.Size = new Size(157, 49);
            btnLoc.TabIndex = 8;
            btnLoc.Text = "Lọc";
            btnLoc.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnLoc.UseAccentColor = false;
            btnLoc.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(btnLoc);
            panel1.Controls.Add(txtPath);
            panel1.Controls.Add(materialLabel3);
            panel1.Controls.Add(materialLabel1);
            panel1.Controls.Add(materialLabel2);
            panel1.Controls.Add(btnCapnhat);
            panel1.Controls.Add(materialComboBox2);
            panel1.Controls.Add(materialComboBox1);
            panel1.Location = new Point(6, 67);
            panel1.Name = "panel1";
            panel1.Size = new Size(1270, 172);
            panel1.TabIndex = 9;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1282, 686);
            Controls.Add(panel1);
            Controls.Add(materialListView1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMain";
            WindowState = FormWindowState.Maximized;
            Load += frmMain_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox txtPath;
        private MaterialSkin.Controls.MaterialListView materialListView1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialButton btnCapnhat;
        private MaterialSkin.Controls.MaterialComboBox materialComboBox1;
        private MaterialSkin.Controls.MaterialComboBox materialComboBox2;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialButton btnLoc;
        private Panel panel1;
    }
}