using HMC_Project.Models.Enums;

namespace HMC_Project.Dtos
{
    public class UserRegistraionDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
