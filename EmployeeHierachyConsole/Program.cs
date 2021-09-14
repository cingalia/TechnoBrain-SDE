using EmployeeHierarchy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeHierachyConsole
{
    class Program
    {
        private static List<Hierarchy> hierarchyList = new List<Hierarchy>();
        static void Main(string[] args)
        {
            Console.WriteLine("Testing");

            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Emplyee4,Employee2,500");
            csvData.AppendLine("Employee3,Employee1,800");
            csvData.AppendLine("Employee6,Employee2,500");
            csvData.AppendLine("Employee1,,1000");
            csvData.AppendLine("Employee5,Employee1,500");
            csvData.AppendLine("Employee2,Employee1,500");

            var _csvData = csvData.ToString();

            

            //Validate Salary
            foreach (var lineData in _csvData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
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

            //Validate employee

            //if ((from e in hierarchyList
            //     group e by e.EmployeeId into g
            //     where g.Count() > 1
            //     select g.Key).Count() > 0)
            //{
            //    throw new InvalidOperationException("Employee contains more than one manager");
            //}

            //Validate CEO
            if (hierarchyList.Where(x => string.IsNullOrEmpty(x.ManagerId)).ToList().Count > 1)
            {
                throw new InvalidOperationException("More than one CEO");
            }

            //ValidateCircularReference
            var successors = hierarchyList.ToDictionary(x => x.EmployeeId, x => x.ManagerId);

            var visited = new HashSet<string>();

            List<List<string>> cycles = new List<List<string>>();

            foreach (var hierarchy in hierarchyList)
            {
                string cycleStart = FindCycle(hierarchy.EmployeeId, successors, visited);

                if (cycleStart != null)
                {
                    // cycle found, get detail information about involved nodes
                    //List<string> cycle = GetCycleMembers(cycleStart, successors);
                    //cycles.Add(cycle);
                }
            }

            //Validate Manager
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

            //Salary Budget
            long allSalary = 0;
            var managerDetails = hierarchyList.FirstOrDefault(x => x.EmployeeId == "Employee3");
            allSalary = managerDetails.Salary;
            if (managerDetails != null)
            {
                var employees = hierarchyList.Where(x => x.ManagerId == managerDetails.EmployeeId).ToList();
                foreach (var employee in employees)
                {
                    allSalary = allSalary + EmployeesSalary(employee.EmployeeId);
                }
            }


            Console.ReadKey();
        }
        static long EmployeesSalary(string managerId)
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
