﻿using Ch13;
using Ch13.Shared;
using Ch13.Shared.ViewModel;
using Ch13.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDialogInterop;

namespace Ch13Tests
{
    [TestFixture]
    public class PersonTests
    {
        IPlatformServices platformServices = new TestPlatformServices();

        [Test]
        public void CannotWorkFromHomeIfPartTime()
        {
            var vm = new Person(platformServices);
            vm.Schedule = ScheduleType.PartTime;
            Assert.IsFalse(vm.CanWorkFromHome);
        }

        [Test]
        public void IfFullTimeTrueThenOthersFalse()
        {
            var vm = new Person(platformServices);
            vm.IsFullTime = true;
            Assert.IsFalse(vm.IsPartTime);
            Assert.IsFalse(vm.IsAsNeeded);
        }

        [Test]
        public void IfPartTimeTrueThenOthersFalse()
        {
            var vm = new Person(platformServices);
            vm.IsPartTime = true;
            Assert.IsFalse(vm.IsFullTime);
            Assert.IsFalse(vm.IsAsNeeded);
        }

        [Test]
        public void ErrorMessageWhenWorkingFromHomeAndPartTime()
        {
            var vm = new Person(platformServices);
            vm.IsPartTime = true;
            vm.DoesWorkFromHome = true;
            Assert.AreEqual(vm[nameof(vm.DoesWorkFromHome)], "Work from home is only available for full-time employees.");
        }

        [Test]
        public void CannotSaveChangesIfErrorExists()
        {
            var vm = new Person(platformServices);
            vm.IsPartTime = true;
            vm.DoesWorkFromHome = true;
            Assert.IsFalse(vm.SaveChanges.CanExecute(this));
        }

        [Test]
        public void CanSaveChangesIfErrorDoesNotExist()
        {
            var vm = new Person(platformServices);
            vm.IsFullTime = true;
            vm.DoesWorkFromHome = true;
            Assert.IsTrue(vm.SaveChanges.CanExecute(this));
        }

        [Test]
        public void TestTaskDialog()
        {
            var taskDialogMessageReceived = false;

            Messenger.Default.Register<TaskDialogInterop.TaskDialogOptions>(this, (o) =>
            {
                Assert.AreEqual(o.MainInstruction, "You clicked save!");
                taskDialogMessageReceived = true;
            });

            var vm = new Person(platformServices);
            vm.IsFullTime = true;
            vm.SaveChanges.Execute(this);
            Assert.IsTrue(taskDialogMessageReceived);
        }

        [Test]
        public void FirstNameThrowsExceptionIfLongerThan10Chars()
        {
            var vm = new Person(platformServices);
            try
            {
                vm.FirstName = "12345678901";//11 characters
                Assert.Fail("FirstName should throw an exception if > 10 characters");
            }
            catch(Exception ex)
            {
                Assert.IsNotNull(ex, "FirstName should throw an exception if > 10 characters");
            }
        }
    }
}
