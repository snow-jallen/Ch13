using Ch13.Shared;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch13
{
    public class WpfPlatformServices : IPlatformServices
    {
        public ICommand CreateCommand(Action execute)
        {
            return new RelayCommand(execute);
        }

        public ICommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            return new RelayCommand(execute, canExecute);
        }

        public void SendMessage(object message)
        {
            var origOptions = message as TaskDialogOptions;
            if (origOptions == null)
                return;

            var options = new TaskDialogInterop.TaskDialogOptions();
            options.Title = origOptions.Title;
            options.MainInstruction = origOptions.MainInstruction;
            Messenger.Default.Send(options);
        }
    }
}
