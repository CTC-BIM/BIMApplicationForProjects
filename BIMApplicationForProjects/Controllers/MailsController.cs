using System;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace BIMApplicationForProjects.Controllers
{
    public class MailsController : Controller
    {

        // GET: Mails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmail(string UserEmail, string EmailNguoiNhan, string userNameEmail, string pa)
        {
            try
            {
                #region Format Email
                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Cảm ơn bạn đã đăng ký Ứng dụng, chúng tôi sẽ liên lạc lại cho bạn trong thời gian sớm nhất:</p>");
                Body.Append("<table>");
                Body.Append("<tr><td colspan='2'><h4>Thông tin khách hàng</h4></td></tr>");
                Body.Append("<tr><td>Họ và tên:</td><td>ADMIN/td></tr>");
                Body.Append("<tr><td>Số điện thoại:</td><td>0908146430</td></tr>");
                Body.Append("<tr><td>Địa chỉ:</td><td>ADDRESS</td></tr>");
                Body.Append("<tr><td>Email:</td><td>" + EmailNguoiNhan + "</td></tr>");
                Body.Append("<tr><td>Nguồn khách:</td><td>" + UserEmail + "</td></tr>");
                Body.Append("</table>");

                #endregion

                #region Send Email
                MailMessage mail = new MailMessage();
                mail.To.Add(UserEmail);
                mail.From = new MailAddress(EmailNguoiNhan);
                mail.Subject = "Tiêu đề của mail được gửi";
                mail.Body = Body.ToString();// phần thân của mail ở trên
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(userNameEmail, pa);// tài khoản Gmail của bạn
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ViewBag.Mail = Body;
                return View();
                #endregion
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <param name="EmailNguoiNhan"></param>
        /// <param name="userNameEmail">userName Email</param>
        /// <param name="pa">password email</param>
        /// <returns></returns>
        public string SendToUserEmail(string UserEmail, string EmailNguoiNhan, string ThongTinUngDung, string TenDuAn)
        {
            string kq = "NotOK";
            try
            {
                #region Format Email
                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Cảm ơn bạn đã đăng ký Ứng dụng, chúng tôi sẽ liên lạc lại cho bạn trong thời gian sớm nhất:</p>");
                Body.Append("<table>");
                Body.Append("<tr><td colspan='2'><h4>Mã dự án: " + TenDuAn + "</h4></td></tr>");
                Body.Append("<tr><td colspan='2'><h4>Thông tin Ứng dụng: " + ThongTinUngDung + "</h4></td></tr>");
                Body.Append("<tr><td>Cập nhật tình trạng ứng dụng qua trang web: </td><td>http://projects.cbimtech.com</td></tr>");
                Body.Append("<tr><td>Người gửi:</td><td>" + EmailNguoiNhan + "</td></tr>");
                Body.Append("</table>");

                #endregion

                #region Send Email
                MailMessage mail = new MailMessage();
                mail.To.Add(EmailNguoiNhan);
                mail.From = new MailAddress(UserEmail);
                mail.Subject = "BIMAPP - ĐĂNG KÝ THÀNH CÔNG ỨNG DỤNG BIM CHO DỰ ÁN";
                mail.Body = Body.ToString();// phần thân của mail ở trên
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("teamcbimtech@gmail.com", "Ctc@2018");// tài khoản Gmail của bạn
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return kq = "OK";
                #endregion

            }
            catch (Exception ex)
            {
                return kq + ex.Message;
            }

        }

        public string SendToAdminEmail(string UserEmail, string EmailNguoiNhan, string ThongTinUngDung, string TenDuAn)
        {
            string kq = "NotOK";
            try
            {
                #region Format Email
                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Đã có dự án mới được đăng ký</p>");
                Body.Append("<table>");
                Body.Append("<tr><td colspan='2'><h4>Mã dự án: " + TenDuAn + "</h4></td></tr>");
                Body.Append("<tr><td colspan='2'><h4>Thông tin Ứng dụng: " + ThongTinUngDung + "</h4></td></tr>");
                Body.Append("<tr><td>Email người yêu cầu:</td><td>" + UserEmail + "</td></tr>");

                Body.Append("</table>");

                #endregion

                #region Send Email
                MailMessage mail = new MailMessage();
                mail.To.Add(EmailNguoiNhan);
                mail.From = new MailAddress("teamcbimtech@gmail.com");
                mail.Subject = "BIMAPP - ĐĂNG KÝ ỨNG DỤNG BIM CHO DỰ ÁN";
                mail.Body = Body.ToString();// phần thân của mail ở trên
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("teamcbimtech@gmail.com", "Ctc@2018");// tài khoản Gmail của bạn
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return kq = "OK";
                #endregion

            }
            catch (Exception ex)
            {
                return kq + ex.Message;
            }

        }


        public string SendConfirmEmail(string userName, string pwd, string EmailNguoiNhan, string LinkActive)
        {
            string kq = "NotOK";
            try
            {
                #region Format Email
                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Cảm ơn bạn đã đăng ký Tài khoản mới, bạn cần Click vào đường link này để Active Tài khoản của bạn</p>");
                Body.Append("<table>");
                Body.Append("<tr><td colspan='2'><h4>Thông tin UserName: <i>" + userName + "</i></h4></td></tr>");
                Body.Append("<tr><td colspan='2'><h4>Thông tin Mật khẩu: <i>" + pwd + "</i></h4></td></tr>");
                Body.Append("<tr><td colspan='2'><h4>Link Active tài khoản:</h4></td></tr>");
                Body.Append("<tr><td colspan='2'>" + LinkActive + "</td></tr>");
                Body.Append("<tr><td colspan='2'></td></tr>");
                Body.Append("<tr><td colspan='2'></td></tr>");
                Body.Append("<tr><td colspan='2'>---------------------------------------------------------------------------------</td></tr>");
                Body.Append("<tr><td colspan='2'></td></tr>");
                Body.Append("<tr><td colspan='2'>Cập nhật tình trạng ứng dụng qua trang web: http://projects.cbimtech.com</td></tr>");
                Body.Append("</table>");

                #endregion

                #region Send Email
                MailMessage mail = new MailMessage();
                mail.To.Add(EmailNguoiNhan);
                mail.From = new MailAddress("teamcbimtech@gmail.com");
                mail.Subject = "BIMAPP - ACTIVE TÀI KHOẢN";
                mail.Body = Body.ToString();// phần thân của mail ở trên
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("teamcbimtech@gmail.com", "Ctc@2018");// tài khoản Gmail của bạn
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return kq = "OK";
                #endregion

            }
            catch (Exception ex)
            {
                return kq + ex.Message;
            }

        }


    }
}