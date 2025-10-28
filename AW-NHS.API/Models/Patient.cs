namespace AW_NHS.API.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string NHSNumber { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GPPractice { get; set; }
    }
}
