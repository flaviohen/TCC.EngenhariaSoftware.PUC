using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.Business
{
	public class EmailBusiness
	{
        private readonly IEmailEnvio _email;

        public EmailBusiness(IEmailEnvio email) 
        {
            _email = email;
        }

        public async Task EnviarEmailAsync(EmailModel email) 
        {
			try
			{
                await _email.EnviarEmailAsync(email.ParaEmail, email.DoEmail, email.EmailsCopia, email.EmailsCopiaOculta, email.Assunto, email.CorpoEmail);
			}
			catch (Exception ex)
			{
                throw new Exception(ex.Message);
			}
        }
	}

    public class EnvioEmail : IEmailEnvio
    {
        public EmailConfiguracao _emailConfiguracao { get; }
        public EnvioEmail()
        {
            _emailConfiguracao = new EmailConfiguracao();
            _emailConfiguracao.PrimaryDomain = "smtp.live.com";
            _emailConfiguracao.PrimaryPort = 587;
            _emailConfiguracao.UsernameEmail = "flavio.henriq@live.com";
            _emailConfiguracao.UsernamePassword = "";
        }
        public Task EnviarEmailAsync(string paraEmail, string doEmail, List<string> emailsCopia, List<string> emailsCopiaOculto, string assunto, string corpo)
        {
            _emailConfiguracao.ToEmail = paraEmail;
            _emailConfiguracao.FromEmail = doEmail;
            _emailConfiguracao.CcEmail = emailsCopia;
            _emailConfiguracao.BccEmail = emailsCopiaOculto;

            Execute(_emailConfiguracao, assunto, corpo).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(EmailConfiguracao emailConfig, string assunto, string corpoEmail)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(emailConfig.UsernameEmail) ? emailConfig.ToEmail : emailConfig.UsernameEmail;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(emailConfig.FromEmail, "Gestao de Saúde")
                };

                mail.To.Add(new MailAddress(toEmail));

                if (emailConfig.CcEmail != null && emailConfig.CcEmail.Count > 0)
                {
                    foreach (var emailCopia in emailConfig.CcEmail)
                    {
                        mail.CC.Add(new MailAddress(emailCopia));
                    }
                }

                if (emailConfig.BccEmail != null && emailConfig.BccEmail.Count > 0)
                {
                    foreach (var emailCopiaOculta in emailConfig.BccEmail)
                    {
                        mail.CC.Add(new MailAddress(emailCopiaOculta));
                    }
                }

                mail.Subject = assunto;
                mail.Body = corpoEmail;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient(emailConfig.PrimaryDomain, emailConfig.PrimaryPort))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailConfig.UsernameEmail, emailConfig.UsernamePassword);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class EmailConfiguracao
	{
		public String PrimaryDomain { get; set; }
		public int PrimaryPort { get; set; }
		public String UsernameEmail { get; set; }
		public String UsernamePassword { get; set; }
		public String FromEmail { get; set; }
		public String ToEmail { get; set; }
		public List<String> CcEmail { get; set; }
		public List<String> BccEmail { get; set; }
	}
	public interface IEmailEnvio
	{
		Task EnviarEmailAsync(string paraEmail, string doEmail, List<string> emailsCopia, List<string> emailsCopiaOculto, string assunto, string corpo);
	}
}
