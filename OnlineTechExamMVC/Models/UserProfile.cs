using System.ComponentModel.DataAnnotations;

namespace OnlineTechExamMVC.Models
{
    public class UserProfile
    {
        public int? UserID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - Birthday.Year;
                if (Birthday.Date > today.AddYears(-age))
                    age--;
                return age;
            }
        }
    }
}
