using System;

namespace SignalRAPI.Dtos.RequestFormDtos
{
    public class RequestFormReadDto
    {
        public int Id { get; set; }
        public string Requester { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public string PaidBy { get; set; }
        public string FundedAccount { get; set; }
        public string ApprovedBy { get; set; }
        public string Status { get; set; }
    }
}