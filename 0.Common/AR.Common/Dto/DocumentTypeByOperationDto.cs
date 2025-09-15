using System;

namespace AR.Common.Dto;

public sealed class DocumentTypeByOperationDto
{
    public int Id { get; set; }
    public string Serial { get; set; } = default!;
    public int Number { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentTypeCode { get; set; } = default!;
    public string NameDocument { get; set; } = default!;
}