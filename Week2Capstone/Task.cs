using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Week2Capstone
{
    class Task
    {
        #region Data Members
        private string description, teamMember;
        private DateTime date;
        private bool status;
        #endregion

        #region Properties 
        public DateTime Date
        {
            set
            {
                date = value;
            }
            get
            {
                return date;
            }
        }

        public string TeamMember
        {
            set
            {
                teamMember = value;
            }
            get
            {
                return teamMember;
            }
        }

        public string Description
        {
            set
            {
                description = value;
            }
            get
            {
                return description;
            }
        }

        public bool Status
        {
            set
            {
                status = value;
            }
            get
            {
                return status;
            }
        }
        #endregion

        public Task(DateTime dueDate, string name, string describe, bool complete = false)
        {
            date = dueDate;
            teamMember = name;
            description = describe;
            status = complete;
        }

        #region Menu Options
        public static List<Task> SetTask(List<Task> Tasks, Task input)
        {
            Tasks.Add(input);
            return Tasks;
        }
        public static void ListTasks(List<Task> Task)
        {
            Console.Clear();
            int count = 0;
            string myString;
            Console.WriteLine("LISTED TASKS");
            string prompt = string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "Task:", "Done?:", "Due Date:", "Team Member:", "Description:");
            Console.WriteLine(prompt);
            string secString = string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "-----", "------", "---------", "------------", "------------");
            Console.WriteLine(secString);

            foreach (Task t in Task)
            {
                count++;
                myString = string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "  " + count, t.Status, t.Date.ToString("MM/dd/yyyy"), t.TeamMember, t.Description);
                Console.WriteLine(myString);
            }
            Console.WriteLine();
        }
        public static void ListByName(List<Task> tasks)
        {
            Console.Write("Please enter the full-name of the team member: ");
            string member = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("LIST TASK BY NAME");
            string prompt = string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "Task:", "Done?:", "Due Date:", "Team Member:", "Description:");
            Console.WriteLine(prompt);
            string secString = string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "-----", "------", "---------", "------------", "------------");
            Console.WriteLine(secString);

            for (int i = 0; i < tasks.Count;i++)
            {
                if(tasks[i].teamMember == member)
                {
                    Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "  " + i+1, tasks[i].Status, tasks[i].Date.ToString("MM/dd/yyyy"), tasks[i].TeamMember, tasks[i].Description));
                }
            }
        }
        public static void ListByBeforeDate(List<Task> tasks)
        {
            DateTime memberDate,chosenDate;
            int count=1;
            Console.Write("Please enter a date: ");
            string member = Console.ReadLine();
            if (DateTime.TryParse(member, out chosenDate))
            {
                Console.Clear();
                Console.WriteLine($"LIST TASK BEFORE DATE: {member}");
                string prompt = string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "Task:", "Done?:", "Due Date:", "Team Member:", "Description:");
                Console.WriteLine(prompt);
                string secString = string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "-----", "------", "---------", "------------", "------------");
                Console.WriteLine(secString);

                for (int i = 0; i < tasks.Count; i++)
                {
                    memberDate = tasks[i].Date;
                    if (DateTime.Compare(memberDate,chosenDate) < 0)
                    {
                        Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "  " + (i + 1), tasks[i].Status, tasks[i].Date.ToString("MM/dd/yyyy"), tasks[i].TeamMember, tasks[i].Description));
                    }
                    else
                    {
                        count++;
                    }
                }

                if(count == tasks.Count)
                {
                    Console.Clear();
                    throw new Exception("Invalid input! Date chosen is before earliest Task date.\nPlease Try Again :)\n");
                }
            }
            else
            {
                Console.Clear();
                throw new FormatException("Invalid input! (Must be in the following Format: mm/dd/yyyy )\nPlease start over. :)\n ");
            }
        }
        public static List<Task> AddTask(List<Task> Tasks)
        {
            string name, descript;
            DateTime dueDate;

            Console.Clear();
            Console.WriteLine("ADD TASK");
            Console.Write("Team Member Name: ");
            name = Console.ReadLine();
            CheckName(name);

            Console.Write("Task Description: ");
            descript = Console.ReadLine();
            CheckDescription(descript);

            Console.Write("Due Date: ");
            string temp = Console.ReadLine();
            if (DateTime.TryParse(temp, out dueDate))
            {
                CheckDate(dueDate);
            }
            else
            {
                Console.Clear();
                throw new FormatException("Invalid Date! (must be in the Format: mm/dd/yyyy)\nPlease start over. :)\n");
            }

            Task input = new Task(dueDate, name, descript);
            Tasks.Add(input);
            Console.WriteLine("\nTask entered!");

            return Tasks;
        }
        public static void DeleteTask(List<Task> tasks)
        {
            Console.Clear();
            Task removable;
            int input;

            ListTasks(tasks);
            

            Console.WriteLine($"What task number# would you like to delete? (1-{tasks.Count})");
            input = int.Parse(Console.ReadLine());

            if (input < 1 || input > tasks.Count)
            {
                Console.WriteLine("That Task does not exist!\n");
                DeleteTask(tasks);
            }
            for (int i = 1; i<=tasks.Count; i++)
            {
                if(input == i)
                {
                    removable = tasks[i-1];
                    Console.Clear();
                    Console.WriteLine("Is this the correct task you would like to delete? (y/n):");
                    Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "Task:", "Done?:", "Due Date:", "Team Member:", "Description:"));
                    Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "-----", "------", "---------", "------------", "------------"));
                    Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "  " + i, removable.Status, removable.Date.ToString("MM/dd/yyyy"), removable.TeamMember, removable.Description));
                    string choice = Console.ReadLine().ToLower();
                    Console.Clear();

                    if (choice == "y")
                    {
                        tasks.Remove(removable);
                        Console.Clear();
                        Console.WriteLine("TASK DELETED!\n");
                    }
                }
            }
            
        }
        public static void MarkTask(List<Task> tasks)
        {
            Console.Clear();
            ListTasks(tasks);

            Console.WriteLine($"Which Task would you like to mark complete?: (1 - {tasks.Count})");
            int input = int.Parse(Console.ReadLine());

            if (input < 1 || input > tasks.Count)
            {
                Console.WriteLine("That Task does not exist!\n");
                MarkTask(tasks);
            }

            for (int i = 1; i <= tasks.Count; i++)
            {
                if (i == input)
                {
                    Console.Clear();
                    Console.WriteLine("Is this the correct task you would like to mark complete? (y/n):");
                    Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "Task:", "Done?:", "Due Date:", "Team Member:", "Description:"));
                    Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "-----", "------", "---------", "------------", "------------"));
                    Console.WriteLine(string.Format("{0,-10}{1,-10}{2,-20}{3,-20}{4,-25}", "  " + i, tasks[i - 1].Status, tasks[i - 1].Date.ToString("MM/dd/yyyy"), tasks[i - 1].TeamMember, tasks[i - 1].Description));
                    string choice = Console.ReadLine().ToLower();
                    Console.Clear();

                    if (choice == "y")
                    {
                        tasks[i - 1].Status = true;
                        Console.Clear();
                        Console.WriteLine("TASK Marked Complete!\n");
                    }
                    
                }
            }
        }
        public static void EditTaskByNumber(List<Task> tasks)
        {
            ListTasks(tasks);
            Console.WriteLine();
            Console.Write("Which Task would you like to edit?\nTask #: ");
            string taskNumber = Console.ReadLine();

            if(int.TryParse(taskNumber, out int number))
            {
                string name,descript;

                Console.Write("Name: ");
                name = Console.ReadLine();
                CheckName(name);
                tasks[number - 1].TeamMember = name;

                Console.Write("Description: ");
                descript = Console.ReadLine();
                CheckDescription(descript);
                tasks[number - 1].Description = descript;

                Console.Write("Due Date: ");
                string temp = Console.ReadLine();
                if (DateTime.TryParse(temp, out DateTime dueDate))
                {
                    CheckDate(dueDate);
                }
                else
                {
                    Console.Clear();
                    throw new FormatException("Invalid Date! (must be in the Format: mm/dd/yyyy)\nPlease start over. :)\n");
                }
                tasks[number - 1].Date = dueDate;
            }
            else
            {
                Console.Clear();
                throw new Exception($"Invalid input! Number must be: (1 - {tasks.Count})\nPlease Try Again :)\n");
            }
        }
        #endregion

        #region Regex Verifications
        public static void CheckName(string input)
        {
            bool regexName, regexFullName;

            regexFullName = Regex.IsMatch(input, @"(\b[A-Z][a-z]{1,30}\s[A-Z][a-z]{1,30}\b)");
            regexName = Regex.IsMatch(input, @"(\b[A-Z][a-z]{1,30}\b)");


            if (!regexFullName && !regexName)
            {
                Console.Clear();
                throw new Exception("Invalid input! Must be in Title Case!\nPlease do not use any special characters.\nPlease start over. :)\n");
            }
        }
        public static void CheckDescription(string input)
        {
            bool regexName;

            regexName = Regex.IsMatch(input, @"[0-9a-zA-Z\s,&,.,\-,_,\,,]{1,25}");


            if (!regexName)
            {
                Console.Clear();
                throw new Exception("Invalid input! Please do not use special characters. (No more than 25 characters)\nPlease start over. :)\n ");
            }
        }
        public static void CheckDate(DateTime input)
        {
            bool regexName;

            regexName = Regex.IsMatch(input.ToString(), @"\b([0-9]{1,2})(\/)([0-9]{1,2})(\/)([0-9]{4})\b");


            if (!regexName)
            {
                Console.Clear();
                throw new FormatException("Invalid input! (Must be in the following Format: mm/dd/yyyy )\nPlease start over. :)\n ");
            }
            else
            {
                //Returns a signed number less than 0 if input is before current date
                //Returns a signed number greater than 0 if input is after current date
                //Returns exactly 0 if input is same as current date
                if(DateTime.Compare(input,DateTime.Now) < 0)
                {
                    Console.Clear();
                    throw new Exception($"Invalid input! Task date must be atleast the current date: {(DateTime.Now).ToString("MM/dd/yyyy")}\nPlease start over. :)\n");
                }
            }
        }
        #endregion

    }
}
