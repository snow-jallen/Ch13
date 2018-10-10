using Ch13.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskDialogInterop;

namespace Ch13
{
    public class Person : INotifyPropertyChanged, IDataErrorInfo
    {
        public Person(string firstName=null, string lastName=null)
        {
            FirstName = firstName;
            LastName = lastName;
            Children.CollectionChanged += (s, e) => OnPropertyChanged(nameof(Children));
        }
        public string Name
        {
            get { return $"{FirstName} {LastName}"; }
        }

        private decimal salary;
        public decimal Salary
        {
            get { return salary; }
            set { SetField(ref salary, value); }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                SetField(ref firstName, value);
                OnPropertyChanged(nameof(Name));
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                SetField(ref lastName, value);
                OnPropertyChanged(nameof(Name));
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { SetField(ref startDate, value); }
        }

        private string mugshotPath;
        public string MugshotPath
        {
            get { return mugshotPath; }
            set { SetField(ref mugshotPath, value); }
        }

        private ObservableCollection<Person> children;
        public ObservableCollection<Person> Children => children ?? (children = new ObservableCollection<Person>());

        private ScheduleType schedule;
        public ScheduleType Schedule
        {
            get { return schedule; }
            set
            {
                SetField(ref schedule, value);
                validateWorkFromHome();
            }
        }

        private void validateWorkFromHome()
        {
            if (DoesWorkFromHome && !IsFullTime)
                errors[nameof(DoesWorkFromHome)] = "Work from home is only available for full-time employees.";
            else
                errors[nameof(DoesWorkFromHome)] = null;

            OnPropertyChanged(nameof(CanWorkFromHome));
            OnPropertyChanged(nameof(DoesWorkFromHome));
            OnPropertyChanged(nameof(IsFullTime));
            OnPropertyChanged(nameof(IsPartTime));
            OnPropertyChanged(nameof(IsAsNeeded));
        }

        private bool doesWorkFromHome;
        public bool DoesWorkFromHome
        {
            get { return doesWorkFromHome; }
            set
            {
                SetField(ref doesWorkFromHome, value);
                validateWorkFromHome();
            }
        }

        public bool CanWorkFromHome => Schedule == ScheduleType.FullTime;
        public bool IsFullTime
        {
            get
            {
                return Schedule == ScheduleType.FullTime;
            }
            set
            {
                if (value)
                    Schedule = ScheduleType.FullTime;
                validateWorkFromHome();
            }
        }

        public bool IsPartTime
        {
            get
            {
                return Schedule == ScheduleType.PartTime;
            }
            set
            {
                if (value)
                    Schedule = ScheduleType.PartTime;
                validateWorkFromHome();
            }
        }

        public bool IsAsNeeded
        {
            get
            {
                return Schedule == ScheduleType.AsNeeded;
            }
            set
            {
                if (value)
                    Schedule = ScheduleType.AsNeeded;
                validateWorkFromHome();
            }
        }

        private RelayCommand saveChanges;
        public RelayCommand SaveChanges => saveChanges ?? (saveChanges = new RelayCommand(
            () =>
            {
                Messenger.Default.Send("You saved it!");


                var options = new TaskDialogOptions();
                options.MainInstruction = "You clicked save!";
                Messenger.Default.Send<TaskDialogOptions>(options);
            },
            () => errors.Count == 0 || errors.Values.Count(v => !String.IsNullOrWhiteSpace(v)) == 0));

        private Dictionary<string, string> errors = new Dictionary<string, string>();
        public string Error => throw new NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

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
