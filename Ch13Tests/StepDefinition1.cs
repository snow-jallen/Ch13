using Ch13;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace Ch13Tests
{
    [Binding]
    public sealed class StepDefinition1
    {
        private const string CalculatorKey = "calc";

        [Given("I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredSomethingIntoTheCalculator(int number)
        {
            var calc = getCalculator();
            calc.EnterNumber(number);
        }

        private static Calculator getCalculator()
        {
            Calculator calc;
            if (ScenarioContext.Current.ContainsKey(CalculatorKey))
            {
                calc = ScenarioContext.Current.Get<Calculator>(CalculatorKey);
            }
            else
            {
                calc = new Calculator();
                ScenarioContext.Current.Set(calc, CalculatorKey);
            }
            return calc;
        }

        [Given(@"I enter (.*), (.*), (.*) and (.*) into the calculator")]
        public void GivenIEnterAndIntoTheCalculator(int p0, int p1, int p2, int p3)
        {
            var calc = getCalculator();
            calc.EnterNumber(p0);
            calc.EnterNumber(p1);
            calc.EnterNumber(p2);
            calc.EnterNumber(p3);
        }

        [When("I press add")]
        public void WhenIPressAdd()
        {
            var calc = getCalculator();
            calc.Add();
        }

        [Then("the result should be (.*) on the screen")]
        public void ThenTheResultShouldBe(int expectedResult)
        {
            var calc = getCalculator();
            expectedResult.Should().Be(calc.Result);
        }
    }
}
