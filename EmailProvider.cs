using ..............DAL.Interfaces;
using System;
using Cm = System.Configuration.ConfigurationManager;
using System.Net.Mail;
using System.Diagnostics;
using ....DAL.Helpers;

namespace ....DAL.Providers {
    public class EmailProvider : IEmailProvider {

        public void SendEmail(MailMessage mailMessage) {
            mailMessage.From = new MailAddress(Cm.AppSettings["EmailAdmin"], ". . .");
            try {
                using (var smtpClient = new SmtpClient(Cm.AppSettings["SmtpHost"], int.Parse(Cm.AppSettings["SmtpPort"]))) {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Send(mailMessage);
                }
            } catch (Exception ex)
            {
                GlobalVars.loggerBlock.LogWriter.Write(". .: ReminderActionDue:  " + ex.Message, "General", 5, 2000, TraceEventType.Error);
            }
        }      
    }
}
