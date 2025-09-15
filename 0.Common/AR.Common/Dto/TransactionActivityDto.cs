using System;

namespace AR.Common.Dto;

public class TransactionActivityDto
{
        public int ActivityId { get; set; }
        public int TransactionId { get; set; }
        public string ActivityKey { get; set; }
        public string ActivityName { get; set; }
        public string TransactionName { get; set; }
        public int Sort { get; set; }
}
