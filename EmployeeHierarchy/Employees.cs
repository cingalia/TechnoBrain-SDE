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

            //Circular Reference Validation
            ValidateCR();

            //Validate Manager
            ValidateManager();
        }

        public long SalaryBudget(string managerId)
        {
            long allSalary = 0;
            var managerDetails = hierarchyList.FirstOrDefault(x => x.EmployeeId == managerId);
            allSalary = managerDetails.Salary;
            if (managerDetails != null)
            {
                var employees = hierarchyList.Where(x => x.ManagerId == managerDetails.EmployeeId).ToList();
                foreach (var employee in employees)
                {
                    allSalary = allSalary + EmployeesSalary(employee.EmployeeId);
                }
            }
            return allSalary;
        }

        long EmployeesSalary(string managerId)
        {
            long _salary = 0;
            var managerDetails = hierarchyList.FirstOrDefault(x => x.EmployeeId == managerId);
            _salary = managerDetails.Salary;
            if (managerDetails != null)
            {
                _salary = _salary + hierarchyList.Where(x => x.ManagerId == managerDetails.EmployeeId).Sum(x => x.Salary);
            }
            return _salary;
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

        //Circular Reference Validation
        private void ValidateCR()
        {
            var successors = hierarchyList.ToDictionary(x => x.EmployeeId, x => x.ManagerId);

            var visited = new HashSet<string>();

            List<List<string>> cycles = new List<List<string>>();

            foreach (var hierarchy in hierarchyList)
            {
                string cycleStart = FindCycle(hierarchy.EmployeeId, successors, visited);

                if (cycleStart != null)
                {
                    // cycle found
                    throw new InvalidOperationException("Circular Reference Found");
                }
            }
        }

        private void ValidateManager()
        {
            foreach (var hierarchy in hierarchyList)
            {
                if (!string.IsNullOrEmpty(hierarchy.ManagerId))
                {
                    var data = hierarchyList.Where(x => x.EmployeeId == hierarchy.ManagerId);
                    if (data.Count() <= 0)
                    {
                        throw new InvalidOperationException("Manager is not an EMployee");
                    }
                }

            }
        }

        static string FindCycleHelper(string start, Dictionary<string, string> successors, HashSet<string> stackVisited, HashSet<string> previouslyVisited)
        {
            string current = start;
            while (current != null)
            {
                if (previouslyVisited.Contains(current))
                {
                    return null;
                }
                if (stackVisited.Contains(current))
                {
                    // this node is part of a cycle
                    return current;
                }

                stackVisited.Add(current);

                successors.TryGetValue(current, out current);
            }

            return null;
        }
        static string FindCycle(string start, Dictionary<string, string> successors, HashSet<string> globalVisited)
        {
            HashSet<string> stackVisited = new HashSet<string>();
            var result = FindCycleHelper(start, successors, stackVisited, globalVisited);
            // update collection of previously processed nodes
            globalVisited.UnionWith(stackVisited);
            return result;
        }
    }
}
