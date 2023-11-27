namespace RenterDemocracy.Models
{
    public class Owner
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string? CompanyName { private get; set; }
        public User PropertyManager { get; set;} = new User();
        public string Address { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string? PhoneNumber { private get; set; }
        public string? EMail { private get; set; }

        public string getOwnerName()
        {
            return CompanyName ?? PropertyManager.getFullName();
        }

        public string getPhoneNumber()
        {
            return PhoneNumber ?? PropertyManager.PhoneNumber;
        }

        public string getEMail()
        {
            return EMail ?? PropertyManager.Email;
        }
    }
}
