using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers
{
    public class MailController:BaseController
    {
        #region 邮件发送

        [HttpPost]
        public ActionResult SendMail()
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "邮件发送成功。";

            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Mail mail = JsonHandler.DeserializeJsonToObject<M_Mail>(strReqStream);

            Message validateMsg = mail.validate();
            if(!validateMsg.Success)
            {
                message.Success = false;
                message.Content = validateMsg.Content;
                return Json(message);
            }

            // 设置发送方的邮件信息,例如使用网易的smtp
            string smtpServer = "smtp.qq.com"; //SMTP服务器
            //string mailFrom = "\"厦门智能+ 创新创业公共服务平台\" <2727954462@qq.com>"; //登陆用户名
            string mailFrom = "2727954462@qq.com"; //登陆用户名

            string userPassword = "aptnaagocerwdfhd";//登陆密码

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码
            smtpClient.EnableSsl = true;

            // 发送邮件设置        
            MailMessage mailMessage = new MailMessage(mailFrom, mail.To); // 发送人和收件人

            mailMessage.From = new MailAddress("\"厦门智能+创新创业公共服务平台\" <2727954462@qq.com>");
            mailMessage.Subject = mail.Subject;//主题
            mailMessage.Body = mail.Body;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Normal;//优先级

            try
            {
                smtpClient.Send(mailMessage); // 发送邮件

            }
            catch (SmtpException e)
            {
                message.Success = false;
                message.Content = "邮件发送失败" + e.Message;
            }


            return Json(message);
        }

        #endregion
    }
}