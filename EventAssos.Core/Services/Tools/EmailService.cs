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
        public async Task SendEmailAsync(string email, string password)
        {
            var message = new MimeMessage(); //crée le message email
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));//expéditeur 
            message.To.Add(new MailboxAddress("", email));// destinataire
            message.Subject = "Votre mot de passe provisoire";//objet de l'email
            message.Body = new TextPart("plain") //contenu du message avec le mot de passe
            {
                Text = $"Bonjour, votre mot de passe provisoire est : {password}"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, false); //connexion au serveur SMTP
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.Password); //authentification
            await client.SendAsync(message); //envoi
            await client.DisconnectAsync(true); //deconnexion
        }
    }
}
