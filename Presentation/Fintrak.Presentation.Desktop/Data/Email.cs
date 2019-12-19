using System;
using Telerik.WinForms.Documents.Model;


namespace Fintrak.Presentation.Desktop
{
    public class Email : BasePropertyChangeImplementation
    {
        private EmailStatus status;

        public Email(string sender, string recipient, string subject, DateTime received)
        {
            this.Sender = sender;
            this.Recipient = recipient;
            this.Subject = subject;
            this.Received = received;
        }

        public RadDocument Content { get; set; }

        public string Sender { get; private set; }

        public string Recipient { get; private set; }

        public string Subject { get; private set; }

        public DateTime Received { get; private set; }

        /// <summary>
        /// Gets or sets Status and notifies for changes
        /// </summary>
        public EmailStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                if (this.status != value)
                {
                    this.status = value;
                    this.OnPropertyChanged(() => this.Status);
                }
            }
        }
    }
}