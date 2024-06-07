using System;
using System.Collections.Generic;
using System.IO;

public class ScheduleManager
{
    private const string FileName = "schedule.txt";
    public Dictionary<string, ScheduleItem> Schedule { get; private set; }

    public ScheduleManager()
    {
        Schedule = new Dictionary<string, ScheduleItem>();
        LoadSchedule();
    }

    public void AddItem(ScheduleItem item)
    {
        Schedule[item.Name] = item;
        Console.WriteLine($"{item.ItemType} scheduled successfully.");
    }

    public void CompleteTask(string name)
    {
        if (Schedule.ContainsKey(name) && Schedule[name] is TaskItem)
        {
            Schedule[name].IsComplete = true;
            Console.WriteLine("Task marked as complete.");
        }
        else
        {
            Console.WriteLine("Task not found or is not a task.");
        }
    }

    public void CheckSchedule()
    {
        foreach (var item in Schedule.Values)
        {
            Console.WriteLine(item);
        }
    }

    public void SaveSchedule()
    {
        using (StreamWriter writer = new StreamWriter(FileName))
        {
            foreach (var item in Schedule.Values)
            {
                writer.WriteLine($"{item.Name},{item.DateTime},{item.Description},{item.IsComplete},{item.ItemType}");
            }
        }
        Console.WriteLine("Schedule saved successfully.");
    }

    public void LoadSchedule()
    {
        if (File.Exists(FileName))
        {
            using (StreamReader reader = new StreamReader(FileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    string name = parts[0];
                    DateTime dateTime = DateTime.Parse(parts[1]);
                    string description = parts[2];
                    bool isComplete = bool.Parse(parts[3]);
                    string type = parts[4];

                    ScheduleItem item = type switch
                    {
                        "Meeting" => new Meeting(name, dateTime, description),
                        "Task" => new TaskItem(name, dateTime, description),
                        "Event" => new Event(name, dateTime, description),
                        _ => throw new InvalidOperationException("Unknown schedule item type")
                    };

                    item.IsComplete = isComplete;
                    Schedule[name] = item;
                }
            }
            Console.WriteLine("Schedule loaded successfully.");
        }
    }
}