using MaterialSkin.Controls;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SaovietTool
{

    public partial class frmMain : MaterialSkin.Controls.MaterialForm
    {
        public class FileImport
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }
            public FileImport(string name, int age, string address)
            {
                Name = name;
                Age = age;
                Address = address;
            }
        }
        private BindingList<FileImport> people = new BindingList<FileImport>();

        string savedPath = ConfigurationManager.AppSettings["LastFilePath"];
        public frmMain()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;

        }
        private void LoadXmlFiles(string path)
        {
            // Kiểm tra xem thư mục có tồn tại không
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.xml");
                foreach (string file in files)
                {
                    // Lấy tên tệp từ đường dẫn
                    string fileName = Path.GetFileName(file);
                    //people.Add(new FileImport(file,10,"asdsa"));

                    //Đọc từ XML
                    XmlDocument xmlDoc = new XmlDocument();
                    string fullPath = savedPath + "\\" + fileName;
                    xmlDoc.Load(fullPath); // Tải file XML

                    // Lấy phần tử gốc
                    XmlNode root = xmlDoc.DocumentElement;

                    // Lấy phần tử <NDHDon>
                    XmlNode ndhDonNode = root.SelectSingleNode("//NDHDon");
                    if (ndhDonNode != null)
                    {
                        XmlNode nBanNode = ndhDonNode.SelectSingleNode("NBan");
                        if (nBanNode != null)
                        {
                            string ten = nBanNode.SelectSingleNode("Ten")?.InnerText;
                            people.Add(new FileImport(ten, 10, "asds"));
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Thư mục không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateListView()
        {
            materialListView1.Items.Clear();
            if (people.Count > 0)
            {
                foreach (var FileImport in people)
                {
                    var item = new ListViewItem(FileImport.Name);
                    item.SubItems.Add(FileImport.Age.ToString());
                    item.SubItems.Add(FileImport.Address);
                    materialListView1.Items.Add(item);
                }
            }

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            txtPath.Text = savedPath;
            InitializeListView();


            LoadXmlFiles(savedPath);
            UpdateListView();
        }
        private void InitializeListView()
        {
            // Thêm cột cho MaterialListView
            materialListView1.Columns.Add("Tên", 150);
            materialListView1.Columns.Add("Tuổi", 100);
            materialListView1.Columns.Add("Địa chỉ", 200);

            // Đăng ký sự kiện cho MouseDoubleClick
            //  materialListView1.MouseDoubleClick += materialListView1_MouseDoubleClick;
        }

        private void materialListView1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void materialListView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void materialListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTestInfo = materialListView1.HitTest(e.Location);
            ListViewItem clickedItem = hitTestInfo.Item;
            if (clickedItem != null)
            {
                // Lấy chỉ số của hàng được double-click
                int rowIndex = clickedItem.Index;

                // Lấy chỉ số của cột được double-click
                int colIndex = hitTestInfo.SubItem != null ? clickedItem.SubItems.IndexOf(hitTestInfo.SubItem) : 0;

                string cellValue = clickedItem.SubItems[colIndex].Text;
                // Lấy giá trị hiện tại của ô
                string currentValue = clickedItem.SubItems[colIndex].Text;

                // Hiển thị hộp thoại nhập liệu
                string newValue = Interaction.InputBox(
                    $"Nhập giá trị mới cho hàng {rowIndex}, cột {colIndex}:",
                    "Cập nhật thông tin",
                    currentValue
                );

                // Cập nhật giá trị mới nếu người dùng nhập liệu và nhấn OK
                if (!string.IsNullOrEmpty(newValue))
                {
                    // Cập nhật giá trị trong ListView
                    clickedItem.SubItems[colIndex].Text = newValue;

                    // Cập nhật giá trị trong BindingList<FileImport>
                    var FileImport = people[rowIndex];
                    switch (colIndex)
                    {
                        case 0:
                            FileImport.Name = newValue;
                            break;
                        case 1:
                            if (int.TryParse(newValue, out int age))
                            {
                                FileImport.Age = age;
                            }
                            else
                            {
                                MessageBox.Show("Tuổi phải là một số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case 2:
                            FileImport.Address = newValue;
                            break;
                    }

                    // Kích hoạt sự kiện ListChanged để cập nhật BindingList
                    people.ResetItem(rowIndex);
                }
            }
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Chọn thư mục bạn muốn lưu.";
                // folderBrowserDialog.rootFolder = Environment.SpecialFolder.MyComputer; // Thay đổi thư mục gốc nếu cần

                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;

                    // Lưu đường dẫn thư mục vào App.config
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["LastFilePath"].Value = selectedPath;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");

                    savedPath = selectedPath;
                    txtPath.Text = savedPath;
                }
                else if (result == DialogResult.Cancel)
                {
                    // MessageBox.Show("Không có thư mục nào được chọn.");
                }
            }
        }
    }
}