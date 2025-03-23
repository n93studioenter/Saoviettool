using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaovietTool
{
    public static class Helper
    {
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
    }
}
