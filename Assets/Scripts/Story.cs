using System.Collections.Generic;
public class Line
{
    public string[] args;
    public Line()
    {
    }
}
public class Cut
{
    public string no;
    public List<Line> contents;
    public Cut()
    {
        contents = new List<Line>();
    }
}
public class Story
{
    public List<Cut> cuts;
    public Story()
    {
        cuts = new List<Cut>();
    }
}