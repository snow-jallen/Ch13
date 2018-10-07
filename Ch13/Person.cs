﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ch13
{
    public class Person : INotifyPropertyChanged
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
