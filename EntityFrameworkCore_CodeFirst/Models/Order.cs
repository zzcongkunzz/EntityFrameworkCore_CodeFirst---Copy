using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore_CodeFirst.Models;

[Table("Order")]
public class Order
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime OderDate { get; set; }
    
    
    // public Guid CustomerId { get; set; }

    [ForeignKey("Customer")]
    public virtual Customer Customer { get; set; } 
    
    
}