using HMC_Project.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Xml.Linq;

namespace HMC_Project.Requests
{
    public class DepartmentRequest
    {
        public Guid ID = Guid.NewGuid();
        public string Name {  get; set; }
        public string Description{ get; set; }
        public string Type { get; set; }
        public string Email {  get; set; }
        public string PhoneNumber { get; set; }

    }
}
