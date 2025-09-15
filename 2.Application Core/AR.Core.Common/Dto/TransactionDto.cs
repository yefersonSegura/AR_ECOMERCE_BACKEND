using System;

namespace AR.Core.Common.Dto;

public class TransactionDto
{
    public int TransactionId { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string GroupKey { get; set; }

}
