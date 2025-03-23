using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SaovietTool
{
    public partial class Form1 : Form
    {
        string dbPath, password, connectionString;
        public Form1()
        {
            InitializeComponent();
        }
        // Bảng ánh xạ Unicode sang VNI Windows
        // Mảng quy đổi từ Unicode sang mã VNI-Windows
        static string ConvertUnicodeToVni(string input)
        {
            // Bảng ánh xạ Unicode sang VNI-Windows
            Dictionary<char, string> unicodeToVniMap = new Dictionary<char, string>
        {
          {'à', "aø"}, {'á', "aù"}, {'ả', "aû"}, {'ã', "aõ"}, {'ạ', "aï"},
    {'À', "AØ"}, {'Á', "AÙ"}, {'Ả', "AÛ"}, {'Ã', "AÕ"}, {'Ạ', "AÏ"},
    {'â', "aâ"}, {'ấ', "aá"}, {'ầ', "aà"}, {'ẩ', "aû"}, {'ẫ', "aõ"}, {'ậ', "aä"},
    {'Â', "AÂ"}, {'Ấ', "AÁ"}, {'Ầ', "AÀ"}, {'Ẩ', "AÛ"}, {'Ẫ', "AÕ"}, {'Ậ', "AÄ"},
    {'è', "eø"}, {'é', "eù"}, {'ẻ', "eû"}, {'ẽ', "eõ"}, {'ẹ', "eï"},
    {'È', "EØ"}, {'É', "EÙ"}, {'Ẻ', "EÛ"}, {'Ẽ', "EÕ"}, {'Ẹ', "EÏ"},
    {'ê', "eâ"}, {'ế', "eá"}, {'ề', "eà"}, {'ể', "eû"}, {'ễ', "eõ"}, {'ệ', "eä"},
    {'Ê', "EÂ"}, {'Ế', "EÁ"}, {'Ề', "EÀ"}, {'Ể', "EÛ"}, {'Ễ', "EÕ"}, {'Ệ', "EÄ"},
    {'ì', "iø"}, {'í', "iù"}, {'ỉ', "iû"}, {'ĩ', "iõ"}, {'ị', "iï"},
    {'Ì', "IØ"}, {'Í', "IÙ"}, {'Ỉ', "IÛ"}, {'Ĩ', "IÕ"}, {'Ị', "IÏ"},
    {'ò', "oø"}, {'ó', "où"}, {'ỏ', "oû"}, {'õ', "oõ"}, {'ọ', "oï"},
    {'Ò', "OØ"}, {'Ó', "OÙ"}, {'Ỏ', "OÛ"}, {'Õ', "OÕ"}, {'Ọ', "OÏ"},
    {'ô', "oâ"}, {'ố', "oá"}, {'ồ', "oà"}, {'ổ', "oû"}, {'ỗ', "oõ"}, {'ộ', "oä"},
    {'Ô', "OÂ"}, // Chỉ thêm một lần
    {'Ố', "OÁ"}, {'Ồ', "OÀ"}, {'Ổ', "OÛ"}, {'Ỗ', "OÕ"}, {'Ộ', "OÄ"},
    {'ù', "uø"}, {'ú', "uù"}, {'ủ', "uû"}, {'ũ', "uõ"}, {'ụ', "uï"},
    {'Ù', "UØ"}, {'Ú', "UÙ"}, {'Ủ', "UÛ"}, {'Ũ', "UÕ"}, {'Ụ', "UÏ"},
    {'ư', "ö"}, {'ứ', "öù"}, {'ừ', "öø"}, {'ử', "öû"}, {'ữ', "öõ"}, {'ự', "öï"},
    {'Ư', "Ö"}, {'Ứ', "ÖÙ"}, {'Ừ', "ÖØ"}, {'Ử', "ÖÛ"}, {'Ữ', "ÖÕ"}, {'Ự', "ÖÏ"},
    {'ỳ', "yø"}, {'ý', "yù"}, {'ỷ', "yû"}, {'ỹ', "yõ"}, {'ỵ', "yï"},
    {'Ỳ', "YØ"}, {'Ý', "YÙ"}, {'Ỷ', "YÛ"}, {'Ỹ', "YÕ"}, {'Ỵ', "YÏ"},
    {'đ', "ñ"}, {'Đ', "Ñ"}
        };

            // Duyệt qua từng ký tự trong chuỗi đầu vào
            string output = "";
            foreach (char c in input)
            {
                if (unicodeToVniMap.ContainsKey(c))
                {
                    output += unicodeToVniMap[c]; // Thay thế bằng ký tự VNI tương ứng
                }
                else
                {
                    output += c; // Giữ nguyên ký tự nếu không có trong bảng ánh xạ
                }
            }

            return output;
        }
        string ten = "";
        private void Form1_Load(object sender, EventArgs e)
        {

            string xmlFilePath = @"C:\TCP\Saoviet\Hoadonchungtu\HD_PB15010034865_2025_2_1_1496315521_TD.xml";

            // Tạo đối tượng XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath); // Tải file XML

            // Lấy phần tử gốc
            XmlNode root = xmlDoc.DocumentElement;

            // Lấy phần tử <NDHDon>
            XmlNode ndhDonNode = root.SelectSingleNode("//NDHDon");
            if (ndhDonNode != null)
            {
                // Lấy phần tử <NBan> trong <NDHDon>
                XmlNode nBanNode = ndhDonNode.SelectSingleNode("NBan");
                if (nBanNode != null)
                {
                    // Lấy các giá trị từ các phần tử con của <NBan>
                    ten = nBanNode.SelectSingleNode("Ten")?.InnerText;
                    textBox2.Text = ten;
                    string mst = nBanNode.SelectSingleNode("MST")?.InnerText;
                    string dchi = nBanNode.SelectSingleNode("DChi")?.InnerText;
                    string sdthoai = nBanNode.SelectSingleNode("SDThoai")?.InnerText;
                    string stknhang = nBanNode.SelectSingleNode("STKNHang")?.InnerText;
                    string tnhang = nBanNode.SelectSingleNode("TNHang")?.InnerText;

                    // In ra các giá trị
                    Console.WriteLine("Tên: " + ten);
                    Console.WriteLine("MST: " + mst);
                    Console.WriteLine("Địa chỉ: " + dchi);
                    Console.WriteLine("Số điện thoại: " + sdthoai);
                    Console.WriteLine("Số tài khoản ngân hàng: " + stknhang);
                    Console.WriteLine("Tên ngân hàng: " + tnhang);

                    // Lấy thông tin từ phần <TTKhac>
                    XmlNode ttKhacNode = nBanNode.SelectSingleNode("TTKhac");
                    if (ttKhacNode != null)
                    {
                        XmlNode ttinNode = ttKhacNode.SelectSingleNode("TTin");
                        if (ttinNode != null)
                        {
                            string ttruong = ttinNode.SelectSingleNode("TTruong")?.InnerText;
                            string kdlieu = ttinNode.SelectSingleNode("KDLieu")?.InnerText;
                            string dlieu = ttinNode.SelectSingleNode("DLieu")?.InnerText;

                            Console.WriteLine("TTruong: " + ttruong);
                            Console.WriteLine("KDLieu: " + kdlieu);
                            Console.WriteLine("DLieu: " + dlieu);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Không tìm thấy phần tử <NBan> trong <NDHDon>.");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy phần tử <NDHDon> trong XML.");
            }

            // Đường dẫn đến cơ sở dữ liệu Access và mật khẩu
            dbPath = @"C:\S.T.E 25\S.T.E 25\DATA\Moi.mdb"; // Thay đổi đường dẫn này
            password = "1@35^7*9)"; // Thay đổi mật khẩu này
            connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Jet OLEDB:Database Password={password};";
            // Khởi tạo kết nối
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    // Mở kết nối
                    connection.Open();
                    Console.WriteLine("Kết nối đến cơ sở dữ liệu thành công!");

                    // Tạo truy vấn SQL

                    string query = "SELECT *  FROM VatTu"; // Thay đổi tên bảng
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);

                    // Tạo DataTable để lưu trữ dữ liệu
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    dataAdapter.Fill(dataTable);

                    // Gán dữ liệu vào DataGridView
                    dataGridView1.DataSource = dataTable;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //var a = textBox1.Text;
            //using (OleDbConnection connection = new OleDbConnection(connectionString))
            //{
            //    try
            //    {
            //        connection.Open();

            //        // Câu lệnh UPDATE
            //        string updateQuery = "UPDATE VatTu SET TenVattu = ? WHERE MaSo = ?";

            //        using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
            //        {
            //            // Thay thế giá trị với tham số
            //            textBox1.Text = textBox2.Text;
            //            var sa = ConvertUnicodeToVniWindows("Cáp thép 12mm. 35x7").ToLower();
            //            var sb = "Coâng Ty Ñieän Löïc Baø Ròa - Vuõng taøu".ToLower();
            //            if (sa == sb)
            //                MessageBox.Show("sdasd");
            //            command.Parameters.AddWithValue("@TenVattu", textBox1.Text); // Giá trị mới
            //            command.Parameters.AddWithValue("@MaSo", 35); // Điều kiện

            //            int rowsAffected = command.ExecuteNonQuery(); // Thực thi câu lệnh

            //            Console.WriteLine($"{rowsAffected} rows updated.");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Lỗi: " + ex.Message);
            //    }
            //    finally
            //    {
            //        if (connection.State == ConnectionState.Open)
            //        {
            //            connection.Close();
            //        }
            //        Console.WriteLine("Kết nối đã được đóng.");
            //    }
            //}

            //Console.ReadLine();   
        }
        public static string VniToUni(string str)
        {
            // Bảng ánh xạ VNI sang Unicode
            string[] VNI = {
    "aù", "aø", "aû", "aõ", "aï", "aâ", "aê", "aá", "aà", "aå", "aã", "aä", "aé", "aè", "aú", "aü", "aë",
    "AÙ", "AØ", "AÛ", "AÕ", "AÏ", "AÂ", "AÊ", "AÁ", "AÀ", "AÅ", "AÃ", "AÄ", "AÉ", "AÈ", "AÚ", "AÜ", "AË",
    "eù", "eø", "eû", "eõ", "eï", "eâ", "eá", "eà", "eå", "eã", "eä",
    "EÙ", "EØ", "EÛ", "EÕ", "EÏ", "EÂ", "EÁ", "EÀ", "EÅ", "EÃ", "EÄ",
    "í ", "ì ", "æ ", "ó ", "ò ",
    "Í ", "Ì ", "Æ ", "Ó ", "Ò ",
    "où", "oø", "oû", "oõ", "oï", "oâ", "ô", "oá", "oà", "oå", "oã", "oä", "ôù", "ôø", "ôû", "ôõ", "ôï",
    "OÙ", "OØ", "OÛ", "OÕ", "OÏ", "OÂ", "Ô ", "OÁ", "OÀ", "OÅ", "OÃ", "OÄ", "ÔÙ", "ÔØ", "ÔÛ", "ÔÕ", "ÔÏ",
    "uù", "uø", "uû", "uõ", "uï", "ö ", "öù", "öø", "öû", "öõ", "öï",
    "UÙ", "UØ", "UÛ", "UÕ", "UÏ", "Ö ", "ÖÙ", "ÖØ", "ÖÛ", "ÖÕ", "ÖÏ",
    "yù", "yø", "yû", "yõ", "î ",
    "YÙ", "YØ", "YÛ", "YÕ", "Î ",
    "ñ ", "Ñ "
};

            string[] UNI = {
    "E1", "E0", "1EA3", "E3", "1EA1", "E2", "103", "1EA5", "1EA7", "1EA9", "1EAB", "1EAD", "1EAF", "1EB1", "1EB3", "1EB5", "1EB7",
    "C1", "C0", "1EA2", "C3", "1EA0", "C2", "102", "1EA4", "1EA6", "1EA8", "1EAA", "1EAC", "1EAE", "1EB0", "1EB2", "1EB4", "1EB6",
    "E9", "E8", "1EBB", "1EBD", "1EB9", "EA", "1EBF", "1EC1", "1EC3", "1EC5", "1EC7",
    "C9", "C8", "1EBA", "1EBC", "1EB8", "CA", "1EBE", "1EC0", "1EC2", "1EC4", "1EC6",
    "ED", "EC", "1EC9", "129", "1ECB",
    "CD", "CC", "1EC8", "128", "1ECA",
    "F3", "F2", "1ECF", "F5", "1ECD", "F4", "1A1", "1ED1", "1ED3", "1ED5", "1ED7", "1ED9", "1EDB", "1EDD", "1EDF", "1EE1", "1EE3",
    "D3", "D2", "1ECE", "D5", "1ECC", "D4", "1A0", "1ED0", "1ED2", "1ED4", "1ED6", "1ED8", "1EDA", "1EDC", "1EDE", "1EE0", "1EE2",
    "FA", "F9", "1EE7", "169", "1EE5", "1B0", "1EE9", "1EEB", "1EED", "1EEF", "1EF1",
    "DA", "D9", "1EE6", "168", "1EE4", "1AF", "1EE8", "1EEA", "1EEC", "1EEE", "1EF0",
    "FD", "1EF3", "1EF7", "1EF9", "1EF5",
    "DD", "1EF2", "1EF6", "1EF8", "1EF4",
    "111", "110"
};
            StringBuilder sUni = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                // Kiểm tra cặp ký tự (2 ký tự)
                if (i + 1 < str.Length)
                {
                    string pair = str.Substring(i, 2);
                    int index = Array.IndexOf(VNI, pair);
                    if (index >= 0)
                    {
                        sUni.Append(ConvertToUnicodeChar(UNI[index]));
                        i++; // Bỏ qua ký tự thứ hai trong cặp
                        continue;
                    }
                }

                // Kiểm tra ký tự đơn (1 ký tự)
                string single = str[i].ToString() + " ";
                int singleIndex = Array.IndexOf(VNI, single);
                if (singleIndex >= 0)
                {
                    sUni.Append(ConvertToUnicodeChar(UNI[singleIndex]));
                }
                else
                {
                    // Giữ nguyên ký tự nếu không có trong bảng ánh xạ
                    sUni.Append(str[i]);
                }
            }

            return sUni.ToString();
        }

        private static char ConvertToUnicodeChar(string hexCode)
        {
            int code = int.Parse(hexCode, System.Globalization.NumberStyles.HexNumber);
            return (char)code;
        }
        private static readonly Dictionary<char, string> UnicodeToVniMap = new Dictionary<char, string>
{
    // Chữ thường
    {'à', "aø"}, {'á', "aù"},  {'ắ', "aé"},{'ả', "aû"}, {'ã', "aõ"}, {'ạ', "aï"},
    {'â', "aâ"}, {'ấ', "aá"}, {'ầ', "aà"}, {'ẩ', "aû"}, {'ẫ', "aõ"}, {'ậ', "aä"},
    {'è', "eø"}, {'é', "eù"}, {'ẻ', "eû"}, {'ẽ', "eõ"}, {'ẹ', "eï"},
    {'ê', "eâ"}, {'ế', "eá"}, {'ề', "eà"}, {'ể', "eû"}, {'ễ', "eõ"}, {'ệ', "eä"},
    {'ì', "iø"}, {'í', "iù"}, {'ỉ', "iû"}, {'ĩ', "iõ"}, {'ị', "iï"},
    {'ò', "oø"}, {'ó', "où"}, {'ỏ', "oû"}, {'õ', "oõ"}, {'ọ', "oï"},
    {'ô', "oâ"}, {'ố', "oá"}, {'ồ', "oà"}, {'ổ', "oû"}, {'ỗ', "oõ"}, {'ộ', "oä"},
    {'ơ', "ô"}, {'ớ', "ôù"}, {'ờ', "ôø"}, {'ở', "ôû"}, {'ỡ', "ôõ"}, {'ợ', "ôï"},
    {'ù', "uø"}, {'ú', "uù"}, {'ủ', "uû"}, {'ũ', "uõ"}, {'ụ', "uï"},
    {'ư', "ö"}, {'ứ', "öù"}, {'ừ', "öø"}, {'ử', "öû"}, {'ữ', "öõ"}, {'ự', "öï"},
    {'ỳ', "yø"}, {'ý', "yù"}, {'ỷ', "yû"}, {'ỹ', "yõ"}, {'ỵ', "yï"}, {'đ', "ñ"},
   

    // Chữ hoa
    {'À', "AØ"}, {'Á', "AÙ"}, {'Ắ', "AÉ"},{'Ả', "AÛ"}, {'Ã', "AÕ"}, {'Ạ', "AÏ"},
    {'Â', "AÂ"}, {'Ấ', "AÁ"}, {'Ầ', "AÀ"}, {'Ẩ', "AÛ"}, {'Ẫ', "AÕ"}, {'Ậ', "AÄ"},
    {'È', "EØ"}, {'É', "EÙ"}, {'Ẻ', "EÛ"}, {'Ẽ', "EÕ"}, {'Ẹ', "EÏ"},
    {'Ê', "EÂ"}, {'Ế', "EÁ"}, {'Ề', "EÀ"}, {'Ể', "EÛ"}, {'Ễ', "EÕ"}, {'Ệ', "EÄ"},
    {'Ì', "IØ"}, {'Í', "IÙ"}, {'Ỉ', "IÛ"}, {'Ĩ', "IÕ"}, {'Ị', "IÏ"},
    {'Ò', "OØ"}, {'Ó', "OÙ"}, {'Ỏ', "OÛ"}, {'Õ', "OÕ"}, {'Ọ', "OÏ"},
    {'Ô', "OÂ"}, {'Ố', "OÁ"}, {'Ồ', "OÀ"}, {'Ổ', "OÛ"}, {'Ỗ', "OÕ"}, {'Ộ', "OÄ"},
    {'Ơ', "Ô"}, {'Ớ', "ÔÙ"}, {'Ờ', "ÔØ"}, {'Ở', "ÔÛ"}, {'Ỡ', "ÔÕ"}, {'Ợ', "ÔÏ"},
    {'Ù', "UØ"}, {'Ú', "UÙ"}, {'Ủ', "UÛ"}, {'Ũ', "UÕ"}, {'Ụ', "UÏ"},
    {'Ư', "Ö"}, {'Ứ', "ÖÙ"}, {'Ừ', "ÖØ"}, {'Ử', "ÖÛ"}, {'Ữ', "ÖÕ"}, {'Ự', "ÖÏ"},
    {'Ỳ', "YØ"}, {'Ý', "YÙ"}, {'Ỷ', "YÛ"}, {'Ỹ', "YÕ"}, {'Ỵ', "YÏ"}, {'Đ', "Ñ"}

};
        public static string ConvertUnicodeToVni2(string input)
        {
            StringBuilder output = new StringBuilder();

            foreach (char c in input)
            {
                string character = c.ToString();
                Console.WriteLine($"Current character: {character}");

                if (UnicodeToVniMap.ContainsKey(c))
                {
                    output.Append(UnicodeToVniMap[c]); // Thay thế bằng ký tự VNI tương ứng
                }
                else
                {
                    output.Append(c); // Giữ nguyên ký tự nếu không có trong bảng ánh xạ
                }
            }

            return output.ToString().Replace("đ","ñ").Replace("Ð","Ñ");  
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    //OÁng daây trong suoát
                    var origin = "Ắc quy SEBANG SMF 54316";
                    var change = ConvertUnicodeToVni2(origin.ToLower()).Replace("đ", "ñ").Replace("Ð", "Ñ");
                    string query = "SELECT * FROM Vattu WHERE LCase(TenVattu) LIKE '%" + change.ToLower() + "%'"; // Thay đổi tên bảng
                    connection.Open();
                    // Khởi tạo đối tượng OleDbCommand
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            // Đọc dữ liệu
                            while (reader.Read())
                            {
                                textBox1.Text = reader["TenVattu"].ToString();
                                // Giả sử bạn có một cột tên "FieldName" trong bảng

                            }
                        }
                    }
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
    }
}