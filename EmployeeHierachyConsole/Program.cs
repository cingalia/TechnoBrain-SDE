using EmployeeHierarchy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeHierachyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing");

            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Emplyee4,Employee2,500");
            csvData.AppendLine("Employee3,Employee1,800");
            //csvData.AppendLine("Employee3,Employee2,800");
            csvData.AppendLine("Employee1,,1000");
            csvData.AppendLine("Employee5,Employee1,500");
            csvData.AppendLine("Employee2,Employee1,500");

            var _csvData = csvData.ToString();

            var hierarchyList = new List<Hierarchy>();

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

            if ((from e in hierarchyList
                 group e by e.EmployeeId into g
                 where g.Count() > 1
                 select g.Key).Count() > 0)
            {
                throw new InvalidOperationException("Employee contains more than one manager");
            }

            Console.ReadKey();
        }
    }
}
