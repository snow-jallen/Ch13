using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch13.ViewModel
{
    public class TreeViewViewModel : INotifyPropertyChanged
    {
        public TreeViewViewModel(ObservableCollection<Person> people)
        {
            People = people;
            SelectedPerson = People?.FirstOrDefault();
        }

        public ObservableCollection<Person> People { get; }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { SetField(ref selectedPerson, value); }
        }

        private RelayCommand addSinglePerson;
        public RelayCommand AddSinglePerson => addSinglePerson ?? (addSinglePerson = new RelayCommand(
            () => SelectedPerson.Children.Add(new Person() { FirstName = $"Added {DateTime.Now}" })));

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
