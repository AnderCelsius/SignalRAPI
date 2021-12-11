using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRAPI.Dtos.RequestFormDtos
{
    public class RequestFormCreateDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public string PaidBy { get; set; }
        public string FundedAccount { get; set; }
        public string ApprovedBy { get; set; }
    }
}
