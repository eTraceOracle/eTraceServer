using System;
using System.Collections.Generic; 
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common.Mail
{
    public  class MailHelper
    {

       /// <summary>
       /// 
       /// </summary>
       /// <param name="dictionary"></param>
       /// <returns></returns>
        public static string ExportDictionaryToHtml(Dictionary<string,string > dictionary)
        {
            StringBuilder strHTMLBuilder = new StringBuilder();
            // strHTMLBuilder.Append("<html >")
            // strHTMLBuilder.Append("<head>")
            // strHTMLBuilder.Append("</head>")

            // Add Connection test data here
            strHTMLBuilder.Append("<table border='1' cellpadding='3' cellspacing='0' style='border-collapse: collapse;font-size:90%;font-family:Calibri' bordercolor='#111111' >");
            strHTMLBuilder.Append("<tr ><b>");
             
            foreach (var kv in dictionary)
            {
                strHTMLBuilder.Append("<td align='left' >");
                strHTMLBuilder.Append(kv.Key);
                strHTMLBuilder.Append("</td>");

                strHTMLBuilder.Append("<td align='left' >");
                strHTMLBuilder.Append(kv.Value);
                strHTMLBuilder.Append("</td>");
                strHTMLBuilder.Append("</tr></b>");
            }
            strHTMLBuilder.Append("</table>");
            // strHTMLBuilder.Append("</html>")
            string Htmltext = strHTMLBuilder.ToString();
            return Htmltext;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailfrom"></param>
        /// <param name="mailto"></param>
        /// <param name="mailsubject"></param>
        /// <param name="message"></param>
        /// <param name="attachmentfile"></param>
        /// <returns></returns>
        public static bool SendMail(string mailfrom, string[] mailto, string mailsubject, string message, string attachmentfile = "")
        {
            try
            {
                SmtpClient smtpclient = new SmtpClient();
                MailMessage mail = new MailMessage();
                smtpclient.EnableSsl = false;
                smtpclient.UseDefaultCredentials = true;
                smtpclient.Port = 25;
                // smtpclient.Host = "ECP-GATEWAY.ASTEC-POWER.COM"
                smtpclient.Host = LocalConfiguration.GetMailHost();                                // "10.162.71.13"
                mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(mailfrom);
                foreach (string value in mailto)
                    mail.To.Add(value);
                mail.Subject = mailsubject;
                mail.Body = message;

                //////////图片不会直接在邮件中显示，只是作为附件
                //string htmlBodyContent = message;
                //AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(htmlBodyContent, null/* TODO Change to default(_) if this is not a reference type */, "text/html");               
                /////////////////////////////////////////////

                ////LinkedResource lrImage = new LinkedResource(attachmentfile, "image/gif");
                ////htmlBody.LinkedResources.Add(lrImage);
                ////lrImage.ContentId = filename;
                
                //mail.AlternateViews.Add(htmlBody);

                if (!string.IsNullOrEmpty(attachmentfile) && attachmentfile.Length >= 4)
                {
                    //if (attachmentfile.Substring(attachmentfile.Length - 4, 4).ToLower() == ".jpg")
                    if (".jpg,.png,.gif".Contains(attachmentfile.Substring(attachmentfile.Length - 4, 4).ToLower()))
                    {
                        if (FileHelper.Instance.IsExistedFile(attachmentfile))
                        {
                            Attachment attachment;
                            attachment = new Attachment(attachmentfile);
                            mail.Attachments.Add(attachment);
                            mail.Body += "<br /> <img src=\"cid:" + attachment.ContentId + "\"/>"; //这个写上附件就会变成文章中的图片
                        }
                    }
                }              

                smtpclient.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

    }
}
