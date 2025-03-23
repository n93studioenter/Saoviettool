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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private static readonly Dictionary<char, string> UnicodeToVniMap = new Dictionary<char, string>
{
    // Chữ thường
    {'à', "aø"}, {'á', "aù"}, {'ả', "aû"}, {'ã', "aõ"}, {'ạ', "aï"},
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
    {'À', "AØ"}, {'Á', "AÙ"}, {'Ả', "AÛ"}, {'Ã', "AÕ"}, {'Ạ', "AÏ"},
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
        public static string ConvertUnicodeToVni(string input)
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

            return output.ToString();
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
        // Bảng ánh xạ từ Unicode sang VNI-Windows
       
        /// <summary>
        /// Chuyển đổi chuỗi Unicode sang VNI-Windows
        /// </summary>
        /// <param name="input">Chuỗi Unicode cần chuyển đổi</param>
        /// <returns>Chuỗi đã chuyển đổi sang VNI-Windows</returns> 
        private void Form2_Load(object sender, EventArgs e)
        {
            string input = "Baùnh xe chòu löïc ";
            string output = VniToUni(input);
            Console.WriteLine("Input: " + input);
            Console.WriteLine("Output: " + output);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox2.Text= VniToUni(textBox1.Text);
            textBox2.Text = ConvertUnicodeToVni(textBox1.Text).Replace("Ð", "Ñ");
        }
    }
}
