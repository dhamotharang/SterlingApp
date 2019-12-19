using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Fintrak.Presentation.Desktop
{
    public class Folder : BasePropertyChangeImplementation
    {
        private BindingList<Email> emails;
        private int activeItems;

        public Folder()
        {
            this.emails = new BindingList<Email>();
            this.Folders = new List<Folder>();
        }

        public string Name { get; set; }

        public List<Folder> Folders { get; set; }

        /// <summary>
        /// Gets the number of unread Email objects of the Folder.
        /// </summary>
        public int ActiveItems
        {
            get
            {
                return this.activeItems;
            }
            set
            {
                if (this.activeItems != value)
                {
                    this.activeItems = value;
                    this.OnPropertyChanged(() => this.ActiveItems);
                }
            }
        }

        /// <summary>
        /// Gets or sets Emails and notifies for changes
        /// </summary>
        public BindingList<Email> Emails
        {
            get
            {
                return this.emails;
            }

            set
            {
                if (this.emails != value)
                {
                    this.emails = value;
                    this.UpdateActiveItems();
                    this.OnPropertyChanged(() => this.Emails);
                }
            }
        }

        public void UpdateActiveItems()
        {
            this.ActiveItems = this.GetUnreadEmailsCount();
        }

        private int GetUnreadEmailsCount()
        {
            if (this.Emails != null)
            {
                return this.Emails.Count(i => i.Status == EmailStatus.Unread);
            }
            else
            {
                return 0;
            }
        }
    }
}