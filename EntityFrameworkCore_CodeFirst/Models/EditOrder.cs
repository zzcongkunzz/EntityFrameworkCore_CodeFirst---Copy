namespace EntityFrameworkCore_CodeFirst.Models;

public class EditOrder
{
    public Order Order { get; set; }
    public List<ProductOder> ProductOders { get; set; }
}