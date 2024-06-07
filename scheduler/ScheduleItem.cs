using System;

public abstract class ScheduleItem
{
    public string Name { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }
    public bool IsComplete { get; set; }

    protected ScheduleItem(string name, DateTime dateTime, string description)
    {
        Name = name;
        DateTime = dateTime;
        Description = description;
        IsComplete = false;
    }

    public abstract string ItemType { get; }

    public override string ToString()
    {
        string status = ItemType == "Task" ? (IsComplete ? "[X]" : "[ ]") : "";
        return $"{Name} - {DateTime} - {Description} - {ItemType} {status}";
    }
}

public class Meeting : ScheduleItem
{
    public Meeting(string name, DateTime dateTime, string description)
        : base(name, dateTime, description) { }

    public override string ItemType => "Meeting";
}

public class TaskItem : ScheduleItem
{
    public TaskItem(string name, DateTime dateTime, string description)
        : base(name, dateTime, description) { }

    public override string ItemType => "Task";
}

public class Event : ScheduleItem
{
    public Event(string name, DateTime dateTime, string description)
        : base(name, dateTime, description) { }

    public override string ItemType => "Event";
}