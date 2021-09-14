using EmployeeHierarchy.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeHierarchy
{
    public class Employees
    {
        private readonly string csv;
        private readonly List<Hierarchy> hierarchyList;
        public Employees(string _csv)
        {
            csv = _csv;
            hierarchyList = new List<Hierarchy>();

            //The salaries in the CSV are valid integer numbers
            ValidateSalaries();

            //Validate Employee
            ValidateEmployee();

            //Validate CEO
            ValidateCEO();
        }

        //Validate Salaries
        private void ValidateSalaries()
        {
            foreach (var lineData in csv.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] splitCsv = lineData.Split(',');
                var hierarchy = new Hierarchy();
                hierarchy.EmployeeId = splitCsv[0];
                hierarchy.ManagerId = splitCsv[1];

                //Check if salary is integer
                int salary = 0;
                bool integerResult = int.TryParse(splitCsv[2], out salary);
                hierarchy.Salary = integerResult ? salary : throw new InvalidOperationException("Salary is not an integer");
                hierarchyList.Add(hierarchy);
            }
        }

        //Validate Employee
        private void ValidateEmployee()
        {
            if ((from e in hierarchyList
                 group e by e.EmployeeId into g
                 where g.Count() > 1
                 select g.Key).Count() > 0)
            {
                throw new InvalidOperationException("Employee contains more than one manager");
            }
        }

        //Validate CEO
        private void ValidateCEO()
        {
            if (hierarchyList.Where(x => string.IsNullOrEmpty(x.ManagerId)).ToList().Count > 1)
            {
                throw new InvalidOperationException("More than one CEO");
            }
        }
    }
}
