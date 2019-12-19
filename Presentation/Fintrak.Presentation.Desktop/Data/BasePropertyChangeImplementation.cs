using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Fintrak.Presentation.Desktop
{
    public abstract class BasePropertyChangeImplementation : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
            if (propertyChangedEventHandler != null)
            {
                propertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.OnPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
