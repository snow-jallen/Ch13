using Ch13.ViewModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch13Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void CannotWorkFromHomeIfPartTime()
        {
            var vm = new EmployeeManagementViewModel();
            vm.Schedule = ScheduleType.PartTime;
            Assert.IsFalse(vm.CanWorkFromHome);
        }

        [Test]
        public void IfFullTimeTrueThenOthersFalse()
        {
            var vm = new EmployeeManagementViewModel();
            vm.IsFullTime = true;
            Assert.IsFalse(vm.IsPartTime);
            Assert.IsFalse(vm.IsAsNeeded);
        }

        [Test]
        public void IfPartTimeTrueThenOthersFalse()
        {
            var vm = new EmployeeManagementViewModel();
            vm.IsPartTime = true;
            Assert.IsFalse(vm.IsFullTime);
            Assert.IsFalse(vm.IsAsNeeded);
        }

        [Test]
        public void ErrorMessageWhenWorkingFromHomeAndPartTime()
        {
            var vm = new EmployeeManagementViewModel();
            vm.IsPartTime = true;
            vm.DoesWorkFromHome = true;
            Assert.AreEqual(vm[nameof(DoesWorkFromHome)], "Work from home is only available for full-time employees.");
        }

        private object DoesWorkFromHome()
        {
            throw new NotImplementedException();
        }
    }
}
