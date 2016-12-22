﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Utils
{
    public class EmailSender
    {
        SmtpClient client;
        string emailFrom;
        string nameFrom;

        public EmailSender(string login, string password, string nameFrom = "DIS Task Manager", string emailFrom = "dis-task-manager@yandex.ru")
        {
            client = new SmtpClient("smtp.yandex.ru", 25);
            client.Credentials = new NetworkCredential(login, password);
            client.EnableSsl = true;
            this.emailFrom = emailFrom;
            this.nameFrom = nameFrom;
        }

        public bool SendAsync(string mailTo, string title, string htmlBody)
        {
            MailAddress from = new MailAddress(emailFrom, nameFrom);
            MailAddress to = new MailAddress(mailTo);
            MailMessage msg = new MailMessage(from, to)
            {
                Subject = title,
                Body = htmlBody,
                IsBodyHtml = true
            };
            try
            {
                client.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
