namespace ContactosApi.Models.Contact
{
    public class ContactModel
    {
        public int? Id { get; set; }

        public required string Name { get; set; }

        public required string Phone { get; set; }

        public string? Email { get; set; }

        public required int UserId { get; set; }

    }
}
