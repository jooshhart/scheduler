using System;

public class Scheduler
{
    private readonly ScheduleManager scheduleManager;

    public Scheduler()
    {
        scheduleManager = new ScheduleManager();
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("Task Scheduler");
            Console.WriteLine("1. Schedule a Meeting");
            Console.WriteLine("2. Schedule a Task");
            Console.WriteLine("3. Complete Task");
            Console.WriteLine("4. Schedule an Event");
            Console.WriteLine("5. Check Schedule");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    ScheduleItem("Meeting");
                    break;
                case 2:
                    ScheduleItem("Task");
                    break;
                case 3:
                    CompleteTask();
                    break;
                case 4:
                    ScheduleItem("Event");
                    break;
                case 5:
                    scheduleManager.CheckSchedule();
                    break;
                case 6:
                    scheduleManager.SaveSchedule();
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private void ScheduleItem(string itemType)
    {
        Console.Write($"Enter {itemType} name: ");
        string name = Console.ReadLine();
        Console.Write($"Enter {itemType} date and time (yyyy-mm-dd HH:mm): ");
        DateTime dateTime = DateTime.Parse(Console.ReadLine());
        Console.Write($"Enter {itemType} description: ");
        string description = Console.ReadLine();

        ScheduleItem item = itemType switch
        {
            "Meeting" => new Meeting(name, dateTime, description),
            "Task" => new TaskItem(name, dateTime, description),
            "Event" => new Event(name, dateTime, description),
            _ => throw new InvalidOperationException("Unknown item type")
        };

        scheduleManager.AddItem(item);
    }

    private void CompleteTask()
    {
        Console.Write("Enter the name of the task to complete: ");
        string name = Console.ReadLine();
        scheduleManager.CompleteTask(name);
    }
}