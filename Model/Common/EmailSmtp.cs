using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Configuration;
namespace ES_HomeCare_API.Model.Common
{
    public class EmailSmtp
    {
        private IConfiguration configuration;
        public readonly string userName = string.Empty;
        public readonly string password = string.Empty;
        public readonly string host = string.Empty;
        public readonly string officailEmail = string.Empty;

        public readonly bool isEnableSsl=false;
        public readonly int port=0;
        public EmailSmtp(IConfiguration _configuration)
        {
            configuration = _configuration;
            userName = configuration.GetConnectionString("SmtpUserName").ToString();
            password = configuration.GetConnectionString("SmtpPassword").ToString();
            host = configuration.GetConnectionString("SmtpHost").ToString();
            officailEmail = configuration.GetConnectionString("OfficailEmail").ToString();
            port =Convert.ToInt32(configuration.GetConnectionString("SmtpPort").ToString());
            isEnableSsl= Convert.ToBoolean(configuration.GetConnectionString("IsEnableSsl").ToString());
        }

        #region SendMail

        public string SendMail(string mailTo, string mailSubject, string mailBody, string mailFrom = "")
        {
            try
            {

                mailFrom = string.IsNullOrEmpty(mailFrom) ? officailEmail : mailFrom;
                mailTo = string.IsNullOrEmpty(mailTo) ? officailEmail : mailTo;
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(mailFrom);

                string[] EmailIds = mailTo.Split(',');
                for (int i = 0; i < EmailIds.Length; i++)
                {
                    msg.To.Add(EmailIds[i]);
                }
                msg.Subject = mailSubject;
                msg.Body = mailBody;
                msg.Priority = MailPriority.Normal;
                msg.IsBodyHtml = true;
                String CONFIGSET = "ConfigSet";
                msg.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(userName, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = host;
                client.EnableSsl = isEnableSsl;
                client.Port = port;
                client.UseDefaultCredentials = true;

                client.Send(msg);
                return msg.ToString();

            }
            catch (Exception ex)
            {

                return "";
            }
        }

        #endregion

        #region MailTemplate
        public  string SupportEmail()
        {
            #region Html
            string html = @"<div style='background:#f2e1e1;margin:0 auto;max-width:640px;padding:0 20px;'><div class='adM'>
                   </div><table cellspacing='0' cellpadding='0' border='0' align='center'>
                            <tbody>
                              <tr>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td>
                                      <div style='background:#fff;color:#5b5b5b;border-radius:4px;font-family:arial;font-size:13px;padding:10px 20px;width:90%;margin:20px auto;line-height:17px;border:1px #ddd solid;border-top:0;clear:both'>
                                        <p>Hello {user}</b></p>
                                        <p>{message}</p>
          </div>
         
                 </td>
              </tr>
              <tr>
                <td height='35'>
                   <table style='color: white;' cellspacing='0' cellpadding='0' border='0' width='100%'>
                    <tbody>
                      <tr>
                        <td style='font-family:'Open Sans',Arial,sans-serif;font-size:15px;' width='82%'>Regard {support}</td>
                        
                      </tr>
                    </tbody>
                  </table>
                    </td>
              </tr>
              <tr>
                <td height='35'></td>
              </tr>
            </tbody>
          </table>
     </div>";
            #endregion

            return html;
        }
        #endregion
    }
}
