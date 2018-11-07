using Ch13.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ch13Tests
{
    public class TestPlatformServices : IPlatformServices
    {
        public ICommand CreateCommand(Action execute)
        {
            throw new NotImplementedException();
        }

        public ICommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(object message)
        {
            throw new NotImplementedException();
        }
    }
}
