using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaovietTool
{
    public partial class frmDangNhap : MaterialSkin.Controls.MaterialForm
    {
        public frmDangNhap()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            //skinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.BlueGrey500, MaterialSkin.Primary.Red700, MaterialSkin.Primary.Yellow500, MaterialSkin.Accent.Amber700, MaterialSkin.TextShade.WHITE);
            btnLogin.Width = 300;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmMain form = new frmMain();
            form.Show();
            this.Hide();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
