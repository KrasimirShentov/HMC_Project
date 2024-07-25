using HMC_Project.Models.Enums;

namespace HMC_Project.Requests
{
    public class UserRequest
    {
        public Guid ID { get; private set; }
        public string Name{ get; set; }
        public string Surname{ get; set; }
        public string UserName{ get; set; }
        public string Password{ get; set; }
        public string Email{ get; set; }
        public GenderType Gender { get; set; }
    }
}
