using HMC_Project.Dtos;
using HMC_Project.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HMC_Project.Requests
{
    public class DepartmentRequest
    {
        //public Guid ID = Guid.NewGuid();
        [Required]
        public string Name {  get; set; }
        [Required]
        public string Description{ get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Email {  get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public Guid CompanyID { get; set; }
        [Required]
        public List<AddressDTO> DepartmentAddresses { get; set; }

    }
}
