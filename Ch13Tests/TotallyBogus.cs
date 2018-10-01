using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ch13.ViewModel;

namespace Ch13Tests
{
    [TestFixture]
    public class FirstTest
    {
        
        [Test]
        public void TestOppositeBools()
        {
            var vm = new MainViewModel();
            vm.IsScreen1 = true;
            Assert.IsFalse(vm.IsScreen2);
        }

        public void NotATest()
        {

        }
    }
}
