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
using System.Text.RegularExpressions;
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
        public static int Id = 1;
        #endregion
        public class FileImportDetail
        {
            public string Ten { get; set; }
            public int ParentId { get; set; }
            public string SoHieu { get; set; }
            public int Soluong { get; set; }
            public double Dongia { get; set; }
            public string DVT { get; set; }

            public FileImportDetail(string ten, int parentId, string soHieu, int soluong, double dongia, string dVT)
            {
                Ten = ten;
                ParentId = parentId;
                SoHieu = soHieu;
                Soluong = soluong;
                Dongia = dongia;
                DVT = dVT;
            }
        }

        public class FileImport
        {
            public int ID { get; set; }
            public string SHDon { get; set; }
            public string KHHDon { get; set; }
            public DateTime NLap { get; set; }
            public string Ten { get; set; }
            public string Noidung { get; set; }
            public int TKCo { get; set; }
            public int TKNo { get; set; }
            public int TkThue { get; set; }
            public string Mst { get; set; }
            public double TongTien { get; set; }
            public int Vat { get; set; }
            public List<FileImportDetail> fileImportDetails;
            public FileImport(string shdon, string khhdon, DateTime nlap, string ten, string noidung, int tkno, int tkco, int tkthue, string mst, double tongTien, int vat)
            {
                ID = Id;
                SHDon = shdon;
                KHHDon = khhdon;
                NLap = nlap;
                Ten = ten;
                Noidung = noidung;
                TKCo = tkco;
                TKNo = tkno;
                TkThue = tkthue;
                Mst = mst;
                TongTien = tongTien;
                Vat = vat;
                Id += 1;
                fileImportDetails = new List<FileImportDetail>();
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
        private int ExecuteQueryResult(string query, params OleDbParameter[] parameters)
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

                    int rowsAffected = command.ExecuteNonQuery(); // Thực thi câu lệnh
                    return rowsAffected;
                }
            }

            return -1;
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
                    XmlNode nTTThanToan = root.SelectSingleNode("//LTSuat");
                    string SHDon = "";
                    string KHHDon = "";
                    string ten = "";
                    string mst = "";
                    string SoHD = "";
                    int TkCo = 0;
                    int TkNo = 0;
                    int TkThue = 0;
                    int Vat = 0;
                    double Thanhtien = 0;
                    string diengiai = "";
                    DateTime NLap = new DateTime();
                    if (nTTChungNode != null)
                    {
                        SHDon = nTTChungNode.SelectSingleNode("SHDon")?.InnerText;
                        KHHDon = nTTChungNode.SelectSingleNode("KHHDon")?.InnerText;
                        NLap = DateTime.Parse(nTTChungNode.SelectSingleNode("NLap")?.InnerText);
                    }

                    XmlNode nBanNode = ndhDonNode.SelectSingleNode("NBan");
                    if (nBanNode != null)
                    {
                        ten = nBanNode.SelectSingleNode("Ten")?.InnerText;
                        mst = nBanNode.SelectSingleNode("MST")?.InnerText;
                    }
                    Vat = int.Parse(nTTThanToan.SelectSingleNode("TSuat").InnerText.Replace("%", ""));
                    Thanhtien = double.Parse(nTTThanToan.SelectSingleNode("ThTien").InnerText);
                    string query = @" SELECT TOP 1 *  FROM KhachHang AS kh  
INNER JOIN HoaDon AS hd ON kh.Maso = hd.MaKhachHang    
WHERE kh.MST = ?  
ORDER BY hd.MaSo DESC"; // Sử dụng ? thay cho @mst trong OleDb
                    DataTable result = ExecuteQuery(query, new OleDbParameter("?", mst));
                    if (result.Rows.Count < 0)
                    {
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
                                    TkThue = int.Parse(row["MaTKNo"].ToString());  // Giả sử có cột "MaSo"; 
                                }
                                if (index == 1)
                                {
                                    TkNo = int.Parse(row["MaTKNo"].ToString());  // Giả sử có cột "MaSo"; 
                                    TkCo = int.Parse(row["MaTKCo"].ToString());  // Giả sử có cột "MaSo"; 
                                    diengiai = Helper.ConvertVniToUnicode(row["DienGiai"].ToString());
                                }
                                // Lấy giá trị từ cột cụ thể trong hàng hiện tại

                                index += 1;
                            }
                        }
                        // Tra cứu từ bảng HeThongTK
                        query = @"Select   * from HeThongTK where MaTC = ?";
                        result = ExecuteQuery(query, new OleDbParameter("?", TkNo));
                        TkNo = int.Parse(result.Rows[0]["SoHieu"].ToString());

                        query = @"Select   * from HeThongTK where MaTC = ?";
                        result = ExecuteQuery(query, new OleDbParameter("?", TkCo));
                        TkCo = int.Parse(result.Rows[0]["SoHieu"].ToString());

                        query = @"Select   * from HeThongTK where MaTC = ?";
                        result = ExecuteQuery(query, new OleDbParameter("?", TkThue));
                        TkThue = int.Parse(result.Rows[0]["SoHieu"].ToString());
                    }
                    if (TkThue == 0)
                        TkThue = 1331;
                    //Add detail
                    var hhdVuList = xmlDoc.SelectNodes("//HHDVu");

                    people.Add(new FileImport(SHDon, KHHDon, NLap, ten, diengiai, TkNo, TkCo, TkThue, mst, Thanhtien, Vat));
                    for (int i = 0; i < hhdVuList.Count; i++)
                    {
                        try
                        {
                            var THHDVu = hhdVuList[i].SelectSingleNode("THHDVu").InnerText;
                            var DVTinh = hhdVuList[i].SelectSingleNode("DVTinh").InnerText;
                            var SLuong = hhdVuList[i].SelectSingleNode("SLuong").InnerText;
                            var DGia = hhdVuList[i].SelectSingleNode("DGia").InnerText;
                            string newName = Helper.ConvertUnicodeToVni(THHDVu);
                            //Kiểm tra trong database xem có sản phẩm chưa, nếu chưa có thì thêm mới
                            query = @"Select * from Vattu 
where TenVattu = ? ";
                            //int rs = (int)ExecuteQuery(query, new OleDbParameter("?", "SAdsd")).Rows[0][0];
                            var getdata = ExecuteQuery(query, new OleDbParameter("?", newName));

                            string sohieu = "";
                            if (getdata.Rows.Count == 0)
                                sohieu = GenerateResultString(THHDVu.Trim());
                            else
                                sohieu = getdata.Rows[0]["SoHieu"].ToString();
                            FileImportDetail fileImportDetail = new FileImportDetail(newName, people.LastOrDefault().ID, sohieu, int.Parse(SLuong), double.Parse(DGia), Helper.ConvertUnicodeToVni(DVTinh));
                            people.LastOrDefault().fileImportDetails.Add(fileImportDetail);
                            if (getdata.Rows.Count == 0)
                            {
                                //Insert thêm vô database
                                query = @"
        INSERT INTO Vattu (MaPhanLoai,SoHieu,TenVattu,DonVi)
        VALUES (?,?,?,?)";
                                OleDbParameter[] parameters = new OleDbParameter[]
                    {
        new OleDbParameter("?","1"),
          new OleDbParameter("?",sohieu),
           new OleDbParameter("?",newName),
            new OleDbParameter("?",Helper.ConvertUnicodeToVni(DVTinh))
                    };

                                // Thực thi truy vấn và lấy kết quả
                                int a = ExecuteQueryResult(query, parameters);
                            }
                        }
                        catch(Exception ex)
                        {

                        }
                      
                    }
                }
            }
            else
            {
                MessageBox.Show("Thư mục không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static string GenerateResultString(string input)
        {
            // Tìm từ đầu tiên (không cần loại bỏ dấu toàn bộ)
            string firstWord = input.Split(' ')[0];

            // Loại bỏ dấu tiếng Việt cho từ đầu tiên
            string normalizedFirstWord = RemoveVietnameseDiacritics(firstWord);

            // Tạo 4 số ngẫu nhiên từ 1 đến 9
            string randomNumbers = GenerateRandomNumbers(4);

            // Kết hợp từ đầu tiên với 4 số ngẫu nhiên
            return normalizedFirstWord + randomNumbers;
        }

        private static string RemoveVietnameseDiacritics(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            Regex regex = new Regex(@"[\u0300-\u036F]+");
            return regex.Replace(normalizedString, "");
        }

        private static string GenerateRandomNumbers(int length)
        {
            Random random = new Random();
            string randomNumbers = "";
            for (int i = 0; i < length; i++)
            {
                // Sinh số ngẫu nhiên từ 1 đến 9
                randomNumbers += random.Next(1, 10).ToString();
            }
            return randomNumbers;
        }
        private void UpdateListView()
        {
            materialListView1.Items.Clear();
            if (people.Count > 0)
            {
                foreach (var FileImport in people)
                {
                    var item = new ListViewItem(FileImport.ID.ToString());
                    item.SubItems.Add(FileImport.SHDon.ToString());
                    item.SubItems.Add(FileImport.NLap.ToShortDateString());
                    item.SubItems.Add(FileImport.Ten + " | " + FileImport.Mst);
                    item.SubItems.Add(FileImport.TongTien.ToString());
                    item.SubItems.Add(FileImport.Noidung);
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
            // connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database";
            //connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\S.T.E 25\S.T.E 25\DATA\importData.accdb;Persist Security Info=False";
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
            materialListView1.Columns.Add("ID", -2);
            materialListView1.Columns.Add("Số hóa đơn", -2);
            materialListView1.Columns.Add("Ngày lập", -2);
            materialListView1.Columns.Add("Tên công ty", -2);
            materialListView1.Columns.Add("Tổng tiền", -2);
            materialListView1.Columns.Add("Nội dung", -2);
            materialListView1.Columns.Add("TK nợ", -2);
            materialListView1.Columns.Add("TK có", -2);
            // Đăng ký sự kiện cho MouseDoubleClick
            //  materialListView1.MouseDoubleClick += materialListView1_MouseDoubleClick;

            // Đăng ký sự kiện DrawColumnHeader
            int totalWidth = materialListView1.ClientSize.Width;
            materialListView1.Columns[0].Width = (int)(totalWidth * 0.05); // 40% chiều rộng
            materialListView1.Columns[1].Width = (int)(totalWidth * 0.05); // 40% chiều rộng
            materialListView1.Columns[2].Width = (int)(totalWidth * 0.1); // 30% chiều rộng
            materialListView1.Columns[3].Width = (int)(totalWidth * 0.4); // 30% chiều rộng
            materialListView1.Columns[4].Width = (int)(totalWidth * 0.1); // 30% chiều rộng
            materialListView1.Columns[5].Width = (int)(totalWidth * 0.1); // 30% chiều rộng
            materialListView1.Columns[6].Width = (int)(totalWidth * 0.1); // 30% chiều rộng
            materialListView1.Columns[7].Width = (int)(totalWidth * 0.1); // 30% chiều rộng
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
                        case 5:
                            FileImport.Noidung = newValue;
                            break;
                        case 6:
                            FileImport.TKNo = int.Parse(newValue);
                            break;
                        case 7:
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            foreach (var item in people)
            {
                // Câu truy vấn SQL với các tham số được đặt tên rõ ràng
                string query = @"
        INSERT INTO tbImport (SHDon,KHHDon, NLap, Ten, Noidung, TKCo, TKNo, TkThue, Mst, Status, Ngaytao,TongTien,Vat)
        VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)";

                // Chuyển đổi Unicode sang VNI (nếu cần)
                string newTen = Helper.ConvertUnicodeToVni(item.Ten);
                string newNoidung = Helper.ConvertUnicodeToVni(item.Noidung);

                // Khai báo mảng tham số với đủ 10 tham số
                OleDbParameter[] parameters = new OleDbParameter[]
                {
        new OleDbParameter("?", item.SHDon),
          new OleDbParameter("?", item.KHHDon),
        new OleDbParameter("?", item.NLap),
        new OleDbParameter("?", newTen),
        new OleDbParameter("?", newNoidung),
        new OleDbParameter("?", item.TKCo),
        new OleDbParameter("?", item.TKNo),
        new OleDbParameter("?", item.TkThue),
        new OleDbParameter("?", item.Mst),
        new OleDbParameter("?","0"),
        new OleDbParameter("?",DateTime.Now.ToShortDateString()),
         new OleDbParameter("?",item.TongTien.ToString()),
          new OleDbParameter("?",item.Vat.ToString())
                };

                // Thực thi truy vấn và lấy kết quả
                int a = ExecuteQueryResult(query, parameters);

                // Kiểm tra kết quả
                if (a > 0)
                {
                    string tableName = "tbImport";

                    query = $"SELECT MAX(ID) FROM {tableName}";

                    int parentID = (int)ExecuteQuery(query, new OleDbParameter("?", null)).Rows[0][0];
                    foreach(var it in item.fileImportDetails)
                    {
                        query = @"
        INSERT INTO tbimportdetail (ParentId,SoHieu, SoLuong, DonGia,DVT,Ten)
        VALUES (?,?,?,?,?,?)";
                        parameters = new OleDbParameter[]
                    {
        new OleDbParameter("?", parentID),
          new OleDbParameter("?", it.SoHieu),
        new OleDbParameter("?", it.Soluong),
        new OleDbParameter("?", it.Dongia),
        new OleDbParameter("?", it.DVT),
        new OleDbParameter("?", it.Ten)
                    };
                        int resl = ExecuteQueryResult(query, parameters);

                    }

                }
                else
                {
                    Console.WriteLine("Thêm dữ liệu thất bại.");
                }
            }

            MessageBox.Show("Lấy dữ liệu thành công");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}