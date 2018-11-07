using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Ch13.Shared.ViewModel
{
    public class AddPersonCommand : ICommand
    {
        private BindingList<Person> people;
        private readonly IPlatformServices platformServices;

        public AddPersonCommand(BindingList<Person> people, IPlatformServices platformServices)
        {
            this.people = people;
            this.platformServices = platformServices ?? throw new ArgumentNullException(nameof(platformServices));
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            people.Add(new Person(platformServices)
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