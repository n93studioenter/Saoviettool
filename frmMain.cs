using MaterialSkin.Controls;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
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
        #region database
        string dbPath, password, connectionString;
        #endregion
        public class FileImport
        {
            public string SHDon { get; set; }
            public DateTime NLap { get; set; }
            public string Ten { get; set; }
            public string Noidung { get; set; }
            public int TKCo { get; set; }
            public int TKNo { get; set; }
            public int TkThue { get; set; }
            public string Mst { get; set; }
            public FileImport(string shdon, DateTime nlap, string ten, string noidung, int tkno, int tkco,  int tkthue, string mst)
            {
                SHDon = shdon;
                NLap = nlap;
                Ten = ten;
                Noidung = noidung;
                TKCo = tkco;
                TKNo = tkno;
                TkThue = tkthue;
                Mst = mst;
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
        private DataTable ExecuteQuery(string query, params OleDbParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Kết nối đến cơ sở dữ liệu thành công!");

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    // Thêm các tham số vào command
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
            }

            return dataTable; // Trả về DataTable chứa dữ liệu
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
                    XmlNode nTTChungNode = root.SelectSingleNode("//TTChung");
                    string SHDon = "";
                    string ten = "";
                    string mst = "";
                    string SoHD = "";
                    int TkCo = 0;
                    int TkNo = 0;
                    int TkThue = 0;
                    string diengiai = "";
                    DateTime NLap = new DateTime();
                    if (nTTChungNode != null)
                    {
                        SHDon = nTTChungNode.SelectSingleNode("SHDon")?.InnerText;
                        NLap = DateTime.Parse(nTTChungNode.SelectSingleNode("NLap")?.InnerText);
                    }

                    XmlNode nBanNode = ndhDonNode.SelectSingleNode("NBan");
                    if (nBanNode != null)
                    {
                        ten = nBanNode.SelectSingleNode("Ten")?.InnerText;
                        mst = nBanNode.SelectSingleNode("MST")?.InnerText;
                    }
                    string query = @" SELECT TOP 1 *  FROM KhachHang AS kh  
INNER JOIN HoaDon AS hd ON kh.Maso = hd.MaKhachHang    
WHERE kh.MST = ?  
ORDER BY hd.MaSo DESC"; // Sử dụng ? thay cho @mst trong OleDb
                    DataTable result = ExecuteQuery(query, new OleDbParameter("?", mst));
                    SoHD = result.Rows[0]["SoHD"].ToString();

                    query = @"Select top 2 * from ChungTu 
where SoHieu = ?
ORDER BY  MaSo DESC";
                    result = ExecuteQuery(query, new OleDbParameter("?", SoHD));
                    var index = 0;
                    if (result.Rows.Count > 0)
                    {
                        foreach (DataRow row in result.Rows)
                        {
                            if (index == 0)
                            {
                                TkThue  = int.Parse(row["MaTKNo"].ToString());  // Giả sử có cột "MaSo"; 
                            }
                            if (index == 1)
                            {
                                TkNo = int.Parse(row["MaTKNo"].ToString());  // Giả sử có cột "MaSo"; 
                                TkCo = int.Parse(row["MaTKCo"].ToString());  // Giả sử có cột "MaSo"; 
                            }
                            // Lấy giá trị từ cột cụ thể trong hàng hiện tại

                            index += 1;
                        }
                    }
                    // Tra cứu từ bảng HeThongTK
                    query = @"Select   * from HeThongTK where MaTC = ?";
                    result = ExecuteQuery(query, new OleDbParameter("?", TkNo));
                    TkNo= int.Parse(result.Rows[0]["SoHieu"].ToString());

                    query = @"Select   * from HeThongTK where MaTC = ?";
                    result = ExecuteQuery(query, new OleDbParameter("?", TkCo));
                    TkCo = int.Parse(result.Rows[0]["SoHieu"].ToString());

                    people.Add(new FileImport(SHDon, NLap, ten, diengiai, TkNo, TkCo, TkThue, mst));
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
                    var item = new ListViewItem(FileImport.SHDon);
                    item.SubItems.Add(FileImport.NLap.ToShortDateString());
                    item.SubItems.Add(FileImport.Ten + " | " + FileImport.Mst);
                    item.SubItems.Add("Nội dung thanh toán chi phí");
                    item.SubItems.Add(FileImport.TKNo.ToString());
                    item.SubItems.Add(FileImport.TKCo.ToString());
                    materialListView1.Items.Add(item);
                }
            }

        }
        private void InitDB()
        {
            // Đường dẫn đến cơ sở dữ liệu Access và mật khẩu
            dbPath = @"C:\S.T.E 25\S.T.E 25\DATA\Moi.mdb"; // Thay đổi đường dẫn này
            password = "1@35^7*9)"; // Thay đổi mật khẩu này
            connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database Password={password};";
        }
        public void LoadDB()
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    // Mở kết nối
                    connection.Open();
                    Console.WriteLine("Kết nối đến cơ sở dữ liệu thành công!");

                    // Tạo truy vấn SQL

                    string query = "SELECT *  FROM tbImport"; // Thay đổi tên bảng
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);

                    // Tạo DataTable để lưu trữ dữ liệu
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    dataAdapter.Fill(dataTable);

                    // Gán dữ liệu vào DataGridView
                    var data = dataTable;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
                finally
                {
                    // Đóng kết nối
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    Console.WriteLine("Kết nối đã được đóng.");
                }
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            InitDB();
            txtPath.Text = savedPath;
            InitializeListView();

            LoadXmlFiles(savedPath);
            UpdateListView();

        }
        private void InitializeListView()
        {
            // Thêm cột cho MaterialListView
            materialListView1.Columns.Add("Số hóa đơn", -2);
            materialListView1.Columns.Add("Ngày lập", -2);
            materialListView1.Columns.Add("Tên công ty", -2);
            materialListView1.Columns.Add("Nội dung", -2);
            materialListView1.Columns.Add("TK nợ", -2);
            materialListView1.Columns.Add("TK có", -2);
            // Đăng ký sự kiện cho MouseDoubleClick
            //  materialListView1.MouseDoubleClick += materialListView1_MouseDoubleClick;

            // Đăng ký sự kiện DrawColumnHeader
            int totalWidth = materialListView1.ClientSize.Width;
            materialListView1.Columns[0].Width = (int)(totalWidth * 0.1); // 40% chiều rộng
            materialListView1.Columns[1].Width = (int)(totalWidth * 0.1); // 30% chiều rộng
            materialListView1.Columns[2].Width = (int)(totalWidth * 0.4); // 30% chiều rộng
            materialListView1.Columns[3].Width = (int)(totalWidth * 0.2); // 30% chiều rộng
            materialListView1.Columns[4].Width = (int)(totalWidth * 0.1); // 30% chiều rộng
            materialListView1.Columns[5].Width = (int)(totalWidth * 0.1); // 30% chiều rộng
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
                        case 3:
                            FileImport.Noidung = newValue;
                            break;
                        case 4:
                            FileImport.TKNo = int.Parse(newValue);
                            break;
                        case 5:
                            FileImport.TKCo = int.Parse(newValue);
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