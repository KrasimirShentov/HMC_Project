using HMC_Project.Models;
using HMC_Project.Models.Enums;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HMC_Project.Requests
{
    public class EmployeeRequest
    {
        public Guid ID = Guid.NewGuid();
        public string Name{ get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email {  get; set; }
        public string Position { get; set; }
        public GenderType Gender { get; set; }
        public Training Training { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
