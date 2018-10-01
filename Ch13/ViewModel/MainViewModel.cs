using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ch13.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            People = new BindingList<Person>(new[]
            {
                new Person{FirstName="Jim",LastName="Bob", Salary=123.45M},
                new Person{FirstName="Joe",LastName="Bob", Salary=223.45M},
                new Person{FirstName="Jack",LastName="Bob", Salary=323.45M},
                new Person{FirstName="Jill",LastName="Bob", Salary=423.45M},
            }.ToList());

        }

        public BindingList<Person> People { get; set; }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { SetField(ref selectedPerson, value); }
        }

        private bool isScreen1;
        public bool IsScreen1
        {
            get { return isScreen1; }
            set
            {
                isScreen1 = value;
                OnPropertyChanged(nameof(IsScreen1));
                OnPropertyChanged(nameof(IsScreen2));
            }
        }

        public bool IsScreen2
        {
            get { return !isScreen1; }
            set
            {
                isScreen1 = !value;
                OnPropertyChanged(nameof(IsScreen1));
                OnPropertyChanged(nameof(IsScreen2));
            }
        }



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