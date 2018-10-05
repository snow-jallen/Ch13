using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Ch13.ViewModel
{
    public class AddPersonCommand : ICommand
    {
        private BindingList<Person> people;

        public AddPersonCommand(BindingList<Person> people)
        {
            this.people = people;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            people.Add(new Person()
            {
                FirstName="New First",
                LastName="New Last",
                MugshotPath="/Images/baby1.png",
                Salary=123.45M,
                StartDate=DateTime.Today
            });
        }
    }
}