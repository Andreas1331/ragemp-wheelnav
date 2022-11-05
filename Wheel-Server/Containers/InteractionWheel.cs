using System.Collections.Generic;

public class InteractionWheel
{
    public int ID { get; }

    public string Title { get; }

    public List<object> Slices { get; set; }

    public InteractionWheel(int id, string title)
    {
        ID = id;
        Title = title;
    }
}
