using NUnit.Framework;
using EmployeeHierarchy;
using System.Text;
using System;

namespace EmployeesHierarchyTest
{
    public class EmployeeTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestValidateSalary()
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Emplyee4,Employee2,500");
            csvData.AppendLine("Employee3,Employee1,800");
            csvData.AppendLine("Employee6,Employee2,500");
            csvData.AppendLine("Employee1,,1000");
            csvData.AppendLine("Employee5,Employee1,500");
            csvData.AppendLine("Employee2,Employee1,500x");

            var _csvData = csvData.ToString();

            try
            {
                Employees employees = new Employees(_csvData);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }       
        }

        [Test]
        public void TestValidateEmployee()
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Emplyee4,Employee2,500");
            csvData.AppendLine("Employee3,Employee1,800");
            csvData.AppendLine("Employee3,Employee2,500");
            csvData.AppendLine("Employee1,,1000");
            csvData.AppendLine("Employee5,Employee1,500");
            csvData.AppendLine("Employee2,Employee1,500");

            var _csvData = csvData.ToString();

            try
            {
                Employees employees = new Employees(_csvData);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Test]
        public void TestValidateCEO()
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Emplyee4,Employee2,500");
            csvData.AppendLine("Employee3,Employee1,800");
            csvData.AppendLine("Employee3,,500");
            csvData.AppendLine("Employee1,,1000");
            csvData.AppendLine("Employee5,Employee1,500");
            csvData.AppendLine("Employee2,Employee1,500");

            var _csvData = csvData.ToString();

            try
            {
                Employees employees = new Employees(_csvData);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Test]
        public void TestValidateCR()
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Emplyee3,Employee2,500");
            csvData.AppendLine("Employee4,Employee3,800");
            csvData.AppendLine("Employee4,Employee1,500");
            csvData.AppendLine("Employee1,,1000");
            csvData.AppendLine("Employee5,Employee1,500");
            csvData.AppendLine("Employee2,Employee1,500");

            var _csvData = csvData.ToString();

            try
            {
                Employees employees = new Employees(_csvData);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Test]
        public void TestValidateManager()
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Emplyee4,Employee2,500");
            csvData.AppendLine("Employee3,Employee1,800");
            csvData.AppendLine("Employee6,Employee7,500");
            csvData.AppendLine("Employee1,,1000");
            csvData.AppendLine("Employee5,Employee1,500");
            csvData.AppendLine("Employee2,Employee1,500");

            var _csvData = csvData.ToString();

            try
            {
                Employees employees = new Employees(_csvData);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Test]
        public void TestValidateSalaryBudget()
        {
            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Emplyee4,Employee2,500");
            csvData.AppendLine("Employee3,Employee1,500");
            csvData.AppendLine("Employee6,Employee2,500");
            csvData.AppendLine("Employee1,,1000");
            csvData.AppendLine("Employee5,Employee1,500");
            csvData.AppendLine("Employee2,Employee1,800");

            var _csvData = csvData.ToString();

            Employees employees = new Employees(_csvData);

            long emp2Budget = employees.SalaryBudget("Employee2");
            Assert.AreEqual(emp2Budget, 1800);

            long emp3Budget = employees.SalaryBudget("Employee3");
            Assert.AreEqual(emp3Budget, 500);

            long emp1Budget = employees.SalaryBudget("Employee1");
            Assert.AreEqual(emp1Budget, 3800);

        }

    }
}