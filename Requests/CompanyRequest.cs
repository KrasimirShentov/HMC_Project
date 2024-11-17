using HMC_Project.Dtos;
using System.Collections.Generic;

namespace HMC_Project.Requests
{
    public class CompanyRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
