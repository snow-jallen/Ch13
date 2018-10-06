using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch13.ViewModel
{
    public class EmployeeManagementViewModel : INotifyPropertyChanged
    {
        public EmployeeManagementViewModel()
        {
            People = new BindingList<Person>(new[]
{
                new Person{FirstName="Jim",LastName="Bob", Salary=123.45M, StartDate= new DateTime(2013, 1,02), MugshotPath="/Images/baby1.png"},
                new Person{FirstName="Joe",LastName="Bob", Salary=223.45M, StartDate= new DateTime(2014, 2,12), MugshotPath="/Images/baby2.jpg"},
                new Person{FirstName="Jack",LastName="Bob", Salary=323.45M, StartDate=new DateTime(2015, 3,22), MugshotPath="/Images/baby1.png"},
                new Person{FirstName="Jill",LastName="Bob", Salary=423.45M, StartDate=new DateTime(2016, 4,03), MugshotPath="/Images/baby2.jpg"},
            }.ToList());
            AddPerson = new AddPersonCommand(People);
            SelectedPerson = People.First();
        }

        public BindingList<Person> People { get; set; }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { SetField(ref selectedPerson, value); }
        }

        public ICommand AddPerson { get; set; }


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
