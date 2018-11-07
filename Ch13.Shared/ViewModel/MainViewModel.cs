using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Ch13.Shared.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(IPlatformServices platformServices)
        {
            ChildViewModels = new ObservableCollection<ChildControl>();
            this.platformServices = platformServices ?? throw new ArgumentNullException(nameof(platformServices));
        }

        private ObservableCollection<ChildControl> childViewModels;
        public ObservableCollection<ChildControl> ChildViewModels
        {
            get { return childViewModels; }
            set { SetField(ref childViewModels, value); }
        }

        private ChildControl selectedChildViewModel;
        public ChildControl SelectedChildViewModel
        {
            get { return selectedChildViewModel; }
            set { SetField(ref selectedChildViewModel, value); }
        }

        private ICommand addEmployeeManagement;
        public ICommand AddEmployeeManagement => addEmployeeManagement ?? (addEmployeeManagement = platformServices.CreateCommand(
            () =>
            {
                ChildViewModels.Add(new ChildControl("Emp Mgmt", new EmployeeManagementViewModel(platformServices)));
                SelectedChildViewModel = ChildViewModels.Last();
            }));

        private ICommand addTreeView;
        public ICommand AddTreeView => addTreeView ?? (addTreeView = platformServices.CreateCommand(
            () =>
            {
                var mostRecentEmployeeManagementViewModel = (EmployeeManagementViewModel)(ChildViewModels.FirstOrDefault(v => v.ViewModel.GetType() == typeof(EmployeeManagementViewModel))?.ViewModel);
                var people = mostRecentEmployeeManagementViewModel?.People ?? new BindingList<Person>(new[] { new Person(platformServices) { FirstName = "Bogus", LastName = "Person" } });

                ChildViewModels.Add(new ChildControl("Tree View", new TreeViewViewModel(new ObservableCollection<Person>(people), platformServices)));
                SelectedChildViewModel = ChildViewModels.Last();
            }));

        private ICommand addDataGrid;
        private readonly IPlatformServices platformServices;

        public ICommand AddDataGrid => addDataGrid ?? (addDataGrid = platformServices.CreateCommand(
            () =>
            {
                ChildViewModels.Add(new ChildControl("Data Grid", new DataGridViewModel()));
                SelectedChildViewModel = ChildViewModels.Last();
            }));

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}