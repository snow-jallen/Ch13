using Ch13;
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
    public class PersonTests
    {
        [Test]
        public void CannotWorkFromHomeIfPartTime()
        {
            var vm = new Person();
            vm.Schedule = ScheduleType.PartTime;
            Assert.IsFalse(vm.CanWorkFromHome);
        }

        [Test]
        public void IfFullTimeTrueThenOthersFalse()
        {
            var vm = new Person();
            vm.IsFullTime = true;
            Assert.IsFalse(vm.IsPartTime);
            Assert.IsFalse(vm.IsAsNeeded);
        }

        [Test]
        public void IfPartTimeTrueThenOthersFalse()
        {
            var vm = new Person();
            vm.IsPartTime = true;
            Assert.IsFalse(vm.IsFullTime);
            Assert.IsFalse(vm.IsAsNeeded);
        }

        [Test]
        public void ErrorMessageWhenWorkingFromHomeAndPartTime()
        {
            var vm = new Person();
            vm.IsPartTime = true;
            vm.DoesWorkFromHome = true;
            Assert.AreEqual(vm[nameof(vm.DoesWorkFromHome)], "Work from home is only available for full-time employees.");
        }

        [Test]
        public void CannotSaveChangesIfErrorExists()
        {
            var vm = new Person();
            vm.IsPartTime = true;
            vm.DoesWorkFromHome = true;
            Assert.IsFalse(vm.SaveChanges.CanExecute(this));
        }

        [Test]
        public void CanSaveChangesIfErrorDoesNotExist()
        {
            var vm = new Person();
            vm.IsFullTime = true;
            vm.DoesWorkFromHome = true;
            Assert.IsTrue(vm.SaveChanges.CanExecute(this));
        }
    }
}
