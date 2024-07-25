using HMC_Project.Models;
using HMC_Project.Models.Enums;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HMC_Project.Requests
{
    public class EmployeeRequest
    {
        public Guid ID { get; private set; }
        public string Name{ get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email {  get; set; }
        public string Position { get; set; }
        public GenderType Gender { get; set; }
        public Training Training { get; set; }
        public Department Department { get; set; }
    }
}
