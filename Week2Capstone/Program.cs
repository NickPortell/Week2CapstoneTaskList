using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            int menu;
            bool repeat = true;

            List<Task> tasks = new List<Task>();



            Task Nick = new Task(DateTime.Parse("05/06/2019"), "Nick Portell", "Create week 2 Capstone", false);
            Task.SetTask(tasks, Nick);

            Task Stephen = new Task(DateTime.Parse("05/03/2019"), "Stephen Jedlicki", "Oversee Assessment 2", true);
            Task.SetTask(tasks, Stephen);

            Task Cecelia = new Task(DateTime.Parse("05/04/2019"), "Cecelia Plotzke", "Do Homework", false);
            Task.SetTask(tasks, Cecelia);

            Console.WriteLine("Welcome to the Task Manager!");
            while (repeat == true)
            {
                try
                {


                    Console.WriteLine($"\n\t1. List tasks\n\t2. Add task\n\t3. Delete task\n\t4. Mark task complete\n\t5. Quit\n\t6. Edit Task");
                    Console.Write("What would you like to do? ");
                    menu = int.Parse(Console.ReadLine());
                    Console.WriteLine();


                    switch (menu)
                    {
                        case 1://List tasks
                            {
                                Console.Clear();
                                Console.WriteLine("\n\t1. List ALL\n\t2. List by Name\n\t3. List ALL before selected Date");
                                Console.WriteLine("Which would you like to do?");
                                string listChoice = Console.ReadLine();

                                if (int.TryParse(listChoice, out int menu2))
                                {
                                    switch (menu2)
                                    {
                                        case 1://List ALL
                                            {
                                                Task.ListTasks(tasks);
                                                break;
                                            }
                                        case 2://List by Name
                                            {
                                                Task.ListByName(tasks);
                                                break;
                                            }
                                        case 3://List All before selected Date
                                            {
                                                Task.ListByBeforeDate(tasks);
                                                break;
                                            }
                                    }
                                }
                                break;
                            }
                        case 2://Add task
                            {
                                Task.AddTask(tasks);
                                break;
                            }
                        case 3://Delete task
                            {
                                Task.DeleteTask(tasks);
                                break;
                            }
                        case 4://Mark task complete
                            {
                                Task.MarkTask(tasks);
                                break;
                            }
                        case 5://Quit
                            {
                                Console.WriteLine("Are you sure you would like to Quit? (y/n)");
                                string response = Console.ReadLine().ToLower();
                                if (response == "y")
                                {
                                    Console.WriteLine("\nHave a great day!");
                                    repeat = false;
                                }
                                break;
                            }
                        case 6://Edit Task
                            {
                                Task.EditTaskByNumber(tasks);
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                throw new FormatException("Invalid key pressed!\n(Please try again)\n");
                            }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }



        }

    }
}

