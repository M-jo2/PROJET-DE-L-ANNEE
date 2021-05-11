using MyPopuStore.BU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPopuStore.UI.Pages.Manage.ReportExport
{
    public class ReportManageViewModel : INotifyPropertyChanged
    {
        private DateTime dateStart;
        private DateTime dateEnd;

        public DateTime DateStart
        {
            get
            {
                return dateStart;
            }
            set
            {
                dateStart = value;
                if (dateStart > DateEnd)
                    dateStart = DateEnd;
                OnPropertyChanged();
            }
        }
        public DateTime DateEnd
        {
            get
            {
                return dateEnd;
            }
            set
            {
                dateEnd = value;
                if (dateEnd < DateStart)
                    dateEnd = DateStart;
                OnPropertyChanged();
            }
        }

        public ReportManageViewModel()
        {
            dateStart = InfoServices.getPopupStoreInfo().CreationDate;
            dateEnd = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
