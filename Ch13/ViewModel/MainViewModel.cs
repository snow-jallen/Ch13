using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Ch13.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            ChildViewModels = new ObservableCollection<ChildControl>();
        }

        private ObservableCollection<ChildControl> childViewModels;
        public ObservableCollection<ChildControl> ChildViewModels
        {
            get { return childViewModels; }
            set { SetField(ref childViewModels, value); }
        }

        private RelayCommand addEmployeeManagement;
        public RelayCommand AddEmployeeManagement => addEmployeeManagement ?? (addEmployeeManagement = new RelayCommand(
            () =>ChildViewModels.Add(new ChildControl("Emp Mgmt", new EmployeeManagementViewModel()))
            ));

        private RelayCommand addTreeView;
        public RelayCommand AddTreeView => addTreeView ?? (addTreeView = new RelayCommand(
            () =>
            {
                var mostRecentEmployeeManagementViewModel = (EmployeeManagementViewModel)(ChildViewModels.FirstOrDefault(v => v.ViewModel.GetType() == typeof(EmployeeManagementViewModel))?.ViewModel);
                var people = mostRecentEmployeeManagementViewModel?.People ?? new BindingList<Person>(new[] { new Person() { FirstName = "Bogus", LastName = "Person" } });
                    
                childViewModels.Add(new ChildControl("Tree View", new TreeViewViewModel(new ObservableCollection<Person>(people))));
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