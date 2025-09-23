using System;

namespace AR.Common.Dto;

public sealed class DocumentTypeByOperationDto
{
    public int Id { get; set; }
    public string Serial { get; set; } = string.Empty;
    public int Number { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentTypeCode { get; set; } = string.Empty;
    public string NameDocument { get; set; } = string.Empty;
}