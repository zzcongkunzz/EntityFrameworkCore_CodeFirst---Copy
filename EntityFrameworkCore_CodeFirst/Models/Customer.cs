using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore_CodeFirst.Models;

[Table("Customer")]
public class Customer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    // chỉ được nhập số và số đầu tiên phải là số 0
    [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Số Điện thoại không hợp lệ")]
    public string? Phone { get; set; }
    
    /*
     không được ghi số truớc tên
     phải có ít nhất 2 kí tự sau @ xong mới được thêm cụm như(.com, .vn,...) 
     tối đa 3 cụm như(.com, .vn,...) 
     */ 
    [RegularExpression(@"^[a-zA-Z][0-9a-zA-Z]+@[0-9a-zA-Z]{2,}(\.[0-9a-zA-Z]+){1,3}$",
        ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; }
    
    public string? Mobile { get; set; }
    
}