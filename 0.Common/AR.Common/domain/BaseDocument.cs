using System;
using AR.Common.Dto;

namespace AR.Common.domain;

public class BaseDocument
{
    public int DocumentId { get; set; }
    public TransactionInfo transaction { get; set; } = new TransactionInfo();
    public string? Serial { get; set; }
    public int Number { get; set; }
    public bool IsFixedNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime CreateDate { get; set; }
    public string UserName { get; set; } = string.Empty;
}
