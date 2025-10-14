using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace dotnet_api.Services
{
    public interface IEmailService
    {
        Task<bool> SendLoginCredentialsAsync(string email, string fullName, string temporaryPassword);
        Task<bool> SendPasswordChangedNotificationAsync(string email, string fullName);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendLoginCredentialsAsync(string email, string fullName, string temporaryPassword)
        {
            try
            {
                var subject = "Thông tin đăng nhập hệ thống quản lý xây dựng";
                var body = GenerateLoginCredentialsEmail(fullName, email, temporaryPassword);

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi email thông tin đăng nhập cho {Email}", email);
                return false;
            }
        }

        public async Task<bool> SendPasswordChangedNotificationAsync(string email, string fullName)
        {
            try
            {
                var subject = "Thông báo đổi mật khẩu thành công";
                var body = GeneratePasswordChangedEmail(fullName);

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi email thông báo đổi mật khẩu cho {Email}", email);
                return false;
            }
        }

        private async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var smtpHost = _configuration["EmailSettings:SmtpHost"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
                var smtpUser = _configuration["EmailSettings:SmtpUser"];
                var smtpPass = _configuration["EmailSettings:SmtpPassword"];
                var fromEmail = _configuration["EmailSettings:FromEmail"];
                var fromName = _configuration["EmailSettings:FromName"] ?? "Hệ thống quản lý xây dựng";

                using var client = new SmtpClient(smtpHost, smtpPort);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
                _logger.LogInformation("Email đã được gửi thành công đến {Email}", to);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi email đến {Email}: {Message}", to, ex.Message);
                return false;
            }
        }

        private string GenerateLoginCredentialsEmail(string fullName, string email, string temporaryPassword)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #2c3e50, #3498db); color: white; padding: 20px; text-align: center; border-radius: 8px 8px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 8px 8px; }}
        .credentials {{ background: #fff; padding: 20px; border-radius: 8px; border-left: 4px solid #3498db; margin: 20px 0; }}
        .warning {{ background: #fff3cd; border: 1px solid #ffeaa7; padding: 15px; border-radius: 8px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
        .btn {{ display: inline-block; background: #3498db; color: white; padding: 12px 24px; text-decoration: none; border-radius: 6px; margin: 10px 0; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🏗️ Hệ thống Quản lý Xây dựng</h1>
            <p>Chào mừng bạn đến với hệ thống!</p>
        </div>
        
        <div class='content'>
            <h2>Xin chào {fullName}!</h2>
            
            <p>Tài khoản của bạn đã được tạo thành công trong hệ thống quản lý thi công xây dựng.</p>
            
            <div class='credentials'>
                <h3>📧 Thông tin đăng nhập:</h3>
                <p><strong>Email:</strong> {email}</p>
                <p><strong>Mật khẩu tạm thời:</strong> <code style='background: #f0f0f0; padding: 4px 8px; border-radius: 4px; font-family: monospace;'>{temporaryPassword}</code></p>
            </div>
            
            <div class='warning'>
                <h4>⚠️ Lưu ý quan trọng:</h4>
                <ul>
                    <li><strong>Bạn sẽ được yêu cầu đổi mật khẩu ngay khi đăng nhập lần đầu</strong></li>
                    <li>Vui lòng đổi mật khẩu thành một mật khẩu mạnh và dễ nhớ</li>
                    <li>Không chia sẻ thông tin đăng nhập với người khác</li>
                </ul>
            </div>
            
            <h3>🔐 Yêu cầu mật khẩu mới:</h3>
            <ul>
                <li>Ít nhất 8 ký tự</li>
                <li>Có chữ hoa và chữ thường</li>
                <li>Có ít nhất 1 số</li>
                <li>Có ít nhất 1 ký tự đặc biệt (@$!%*?&)</li>
            </ul>
            
            <p>Nếu bạn có bất kỳ câu hỏi nào, vui lòng liên hệ với quản trị viên hệ thống.</p>
            
            <p>Trân trọng,<br>
            <strong>Đội ngũ phát triển hệ thống</strong></p>
        </div>
        
        <div class='footer'>
            <p>Email này được gửi tự động từ hệ thống quản lý xây dựng</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GeneratePasswordChangedEmail(string fullName)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #28a745, #20c997); color: white; padding: 20px; text-align: center; border-radius: 8px 8px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 8px 8px; }}
        .success {{ background: #d4edda; border: 1px solid #c3e6cb; padding: 15px; border-radius: 8px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>✅ Đổi mật khẩu thành công</h1>
        </div>
        
        <div class='content'>
            <h2>Xin chào {fullName}!</h2>
            
            <div class='success'>
                <h4>🎉 Thông báo:</h4>
                <p>Mật khẩu của bạn đã được đổi thành công vào lúc <strong>{DateTime.Now:dd/MM/yyyy HH:mm}</strong></p>
            </div>
            
            <p>Nếu bạn không thực hiện thay đổi này, vui lòng liên hệ ngay với quản trị viên hệ thống.</p>
            
            <p>Trân trọng,<br>
            <strong>Đội ngũ phát triển hệ thống</strong></p>
        </div>
        
        <div class='footer'>
            <p>Email này được gửi tự động từ hệ thống quản lý xây dựng</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}
