using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch13.ViewModel
{
    public enum ScheduleType { FullTime,PartTime,AsNeeded}

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

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                SetField(ref fileName, value);
                SaveEmployeeList.RaiseCanExecuteChanged();
                LoadEmployeeList.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand saveEmployeeList;
        public RelayCommand SaveEmployeeList => saveEmployeeList ?? (saveEmployeeList = new RelayCommand(
            () =>
            {
                var json = JsonConvert.SerializeObject(People, Formatting.Indented);

                //For a _real_ MVVM implementation you wouldn't write straight to the filesystem here
                //you'd have something like a IStorageService with Save() and Read() methods.
                //That way you can swap in a different implementation of IStorageService when you're testing
                //that doesn't actually hit the file system.
                //But...for now, we'll just write straight to disk.
                File.WriteAllText(FileName, json);
            },
            ()=>
            {
                var folder = Path.GetDirectoryName(FileName);
                return Directory.Exists(folder);
            }));

        private RelayCommand loadEmployeeList;
        public RelayCommand LoadEmployeeList => loadEmployeeList ?? (loadEmployeeList = new RelayCommand(
            () =>
            {
                var json = File.ReadAllText(FileName);
                var peopleFromJson = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
                People = new BindingList<Person>(peopleFromJson.ToList());
                OnPropertyChanged(nameof(People));
                SelectedPerson = People.First();
            },
            () => File.Exists(FileName)));

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
