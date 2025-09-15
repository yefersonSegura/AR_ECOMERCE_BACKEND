using System;

namespace AR.Common;

public class FilterNameAttribute : Attribute
{
    public FilterNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }

}