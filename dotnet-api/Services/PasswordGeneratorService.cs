using System.Security.Cryptography;
using System.Text;

namespace dotnet_api.Services
{
    public class PasswordGeneratorService
    {
        private static readonly string LowerCase = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string Numbers = "0123456789";
        private static readonly string SpecialChars = "@$!%*?&";
        
        public static string GenerateSecurePassword(int length = 12)
        {
            // Đảm bảo có ít nhất 1 ký tự từ mỗi loại
            var password = new StringBuilder();
            var random = RandomNumberGenerator.Create();
            
            // Thêm ít nhất 1 ký tự từ mỗi loại
            password.Append(GetRandomChar(LowerCase, random));
            password.Append(GetRandomChar(UpperCase, random));
            password.Append(GetRandomChar(Numbers, random));
            password.Append(GetRandomChar(SpecialChars, random));
            
            // Thêm các ký tự ngẫu nhiên để đạt độ dài yêu cầu
            var allChars = LowerCase + UpperCase + Numbers + SpecialChars;
            for (int i = password.Length; i < length; i++)
            {
                password.Append(GetRandomChar(allChars, random));
            }
            
            // Xáo trộn thứ tự các ký tự
            return ShuffleString(password.ToString(), random);
        }
        
        private static char GetRandomChar(string chars, RandomNumberGenerator random)
        {
            var bytes = new byte[1];
            random.GetBytes(bytes);
            return chars[bytes[0] % chars.Length];
        }
        
        private static string ShuffleString(string input, RandomNumberGenerator random)
        {
            var chars = input.ToCharArray();
            for (int i = chars.Length - 1; i > 0; i--)
            {
                var bytes = new byte[1];
                random.GetBytes(bytes);
                int j = bytes[0] % (i + 1);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }
            return new string(chars);
        }
        
        public static bool IsSecurePassword(string password)
        {
            // Kiểm tra regex: ít nhất 8 ký tự, có chữ hoa, thường, số và ký tự đặc biệt
            var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }
    }
}
