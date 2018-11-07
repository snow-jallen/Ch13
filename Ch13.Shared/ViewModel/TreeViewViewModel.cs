using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch13.Shared.ViewModel
{
    public class TreeViewViewModel : INotifyPropertyChanged
    {
        public TreeViewViewModel(ObservableCollection<Person> people, IPlatformServices platformServices)
        {
            People = people;
            this.platformServices = platformServices ?? throw new ArgumentNullException(nameof(platformServices));
            SelectedPerson = People?.FirstOrDefault();
        }

        public ObservableCollection<Person> People { get; }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { SetField(ref selectedPerson, value); }
        }
        int childNumber = 1;
        private ICommand addSinglePerson;
        private readonly IPlatformServices platformServices;

        public ICommand AddSinglePerson => addSinglePerson ?? (addSinglePerson = platformServices.CreateCommand(
            () => SelectedPerson.Children.Add(new Person(platformServices) { FirstName = $"Child {childNumber++}" })));

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
