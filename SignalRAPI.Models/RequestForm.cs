using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRAPI.Models
{
    public class RequestForm
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public string PaidBy { get; set; }
        public string FundedAccount { get; set; }
        public string ApprovedBy { get; set; }
        public int FormStatusId { get; set; }

        // Navigation Properties
        public FormStatus FormStatus { get; set; }
        public AppUser User { get; set; }
    }
}
