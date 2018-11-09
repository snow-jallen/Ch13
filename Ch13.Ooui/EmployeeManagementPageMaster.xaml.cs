using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ch13.Ooui
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeManagementPageMaster : ContentPage
    {
        public ListView ListView;

        public EmployeeManagementPageMaster()
        {
            InitializeComponent();

            BindingContext = new EmployeeManagementPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class EmployeeManagementPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<EmployeeManagementPageMenuItem> MenuItems { get; set; }
            
            public EmployeeManagementPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<EmployeeManagementPageMenuItem>(new[]
                {
                    new EmployeeManagementPageMenuItem { Id = 0, Title = "Page 1" },
                    new EmployeeManagementPageMenuItem { Id = 1, Title = "Page 2" },
                    new EmployeeManagementPageMenuItem { Id = 2, Title = "Page 3" },
                    new EmployeeManagementPageMenuItem { Id = 3, Title = "Page 4" },
                    new EmployeeManagementPageMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}