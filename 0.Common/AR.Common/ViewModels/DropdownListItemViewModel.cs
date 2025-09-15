using System;

namespace AR.Common.ViewModels;

public class DropdownListItemViewModel
{
    public long? Id { get; set; }
    public long? Value { get; set; }
    public string Label { get; set; }
    public string Code { get; set; }
    public bool IsDefault { get; set; }
}
