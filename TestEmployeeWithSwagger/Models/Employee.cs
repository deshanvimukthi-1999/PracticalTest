
namespace TestEmployeeWithSwagger.Models
{
    public class Employee
    {
        public string empNo { get; set; }
        public string empName { get; set; }
        public string empAddressLine1 { get; set; }
        public string empAddressLine2 { get; set; }
        public string empAddressLine3 { get; set; }
        public string departmentCode { get; set; }
        public string dateOfJoin { get; set; }
        public string dateOfBirth { get; set; }
        public decimal basicSalary { get; set; }
        public bool isActive { get; set; }
    }

}
