using HMC_Project.Dtos;
using HMC_Project.Models;
using System.Collections.Generic;
using System.Net;

namespace HMC_Project.Requests
{
    public class CompanyRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AddressDTO> Addresses { get; set; }

    }
}
