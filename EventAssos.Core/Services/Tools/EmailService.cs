using EventAssos.Core.Interfaces.Services.Tools;
using EventAssos.Core.Settings;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace EventAssos.Core.Services.Tools
{
    public class EmailService(EmailSettings _settings) : IEmailService
    {
            //Subject à la place de password, pour rendre la méthode réutilisable
            public async Task SendEmailAsync(string email, string subject, string body)
            {
            var message = new MimeMessage();//crée le message email
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));//expéditeur
            message.To.Add(new MailboxAddress("", email));// destinataire

            
            message.Subject = subject;

            // Utilisation de html
            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var client = new SmtpClient();
            // Utilisation de SecureSocketOptions pour plus de sécurité selon le port (587 ou 465)
            await client.ConnectAsync(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);//connexion au serveur SMTP
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.Password);//authentification
            await client.SendAsync(message);//envoi
            await client.DisconnectAsync(true);//deconnexion
            }
    }
    
}
