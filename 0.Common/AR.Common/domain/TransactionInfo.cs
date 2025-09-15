using System;
using AR.Common.Dto;

namespace AR.Common.domain;

public class TransactionInfo
{
    public OperationDto Operation { get; set; } = new OperationDto();
    public DocumentTypeByOperationDto DocumentType { get; set; } = new DocumentTypeByOperationDto();
}

public sealed class OperationDto
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public int key_transaction { get; set; }
}