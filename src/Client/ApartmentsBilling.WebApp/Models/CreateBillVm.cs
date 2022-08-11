using ApartmentsBilling.Common.Dtos.BillsDto;
using System.Collections.Generic;

namespace ApartmentsBilling.WebApp.Models
{
    public class CreateBillVm
    {
        public List<CreateBillDto> Dto { get; set; }
    }
}
