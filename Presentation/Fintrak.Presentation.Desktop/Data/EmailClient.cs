using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Fintrak.Presentation.Desktop
{
    public class EmailClient
    {
        public EmailClient(string name, IEnumerable<Folder> folders)
        {
            this.Name = name;
            this.Folders = folders;
            this.Emails = new BindingList<Email>(this.Folders.SelectMany(f => f.Emails).ToList());
        }

        public IEnumerable<Folder> Folders { get; private set; }

        public string Name { get; private set; }

        public BindingList<Email> Emails { get; private set; }
    }
}