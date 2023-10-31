namespace RenterDemocracy.Models
{
    public class Owner
    {
        public Guid Id { get; set; }
        public string? CompanyName { private get; set; }
        public User PrimaryContact { get; set; } = new User();
        public User PropertyManager { get; set;} = new User();
        public string Address { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string? PhoneNumber { private get; set; }
        public string? EMail { private get; set; }

        public string getOwnerName()
        {
            return CompanyName ?? PrimaryContact.getFullName();
        }

        public string getPhoneNumber()
        {
            return PhoneNumber ?? PrimaryContact.PhoneNumber;
        }

        public string getEMail()
        {
            return EMail ?? PrimaryContact.Email;
        }
    }
}
