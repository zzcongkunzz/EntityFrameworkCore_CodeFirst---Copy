using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCore_CodeFirst.Models;

[Table("ProductOder")]
public class ProductOder
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public int Quantity { get; set; } = 1;
    
    [ForeignKey("ProductId")]
    public Product Product { get; set; } 
    
    [ForeignKey("OrderId")]
    public Order Order { get; set; } 
}