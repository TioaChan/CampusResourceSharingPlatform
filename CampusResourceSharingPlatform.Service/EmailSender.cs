﻿using CampusResourceSharingPlatform.Model.Application;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace CampusResourceSharingPlatform.Service
{
	public class EmailSender : IEmailSender
	{
		private readonly EmailSettings _emailSettings;

		public EmailSender(IOptions<EmailSettings> emailSettings)
		{
			_emailSettings = emailSettings.Value;
		}
		public async Task SendEmailAsync(string email, string subject, string message)
		{
			try
			{
				var mimeMessage = new MimeMessage();

				mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

				mimeMessage.To.Add(new MailboxAddress(email.Split("@")[0],email));

				mimeMessage.Subject = subject;

				mimeMessage.Body = new TextPart("html")
				{
					Text = message
				};

				using (var client = new SmtpClient())
				{
					// For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
					client.ServerCertificateValidationCallback = (s, c, h, e) => true;

					await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, true);


					// Note: only needed if the SMTP server requires authentication
					await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

					await client.SendAsync(mimeMessage);

					await client.DisconnectAsync(true);
				}

			}
			catch (Exception ex)
			{
				// TODO: handle exception
				throw new InvalidOperationException(ex.Message);
			}
		}
	}
}
