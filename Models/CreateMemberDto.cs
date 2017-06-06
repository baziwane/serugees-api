namespace Serugees.Api.Models
{
    public class CreateMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set;}
        public string LastName {get; set;}    
        public string TelephoneNumber { get; set;}
        public string DateRegistered { get;set; }     
        public bool Active { get;set; }
    }
}