using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using EntityFrameworkCore_CodeFirst.Models;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace EntityFrameworkCore_CodeFirst.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly MyDbContext MyDbContext;

    public HomeController(MyDbContext context ,ILogger<HomeController> logger)
    {
        _logger = logger;
        MyDbContext = context;

        AddDataa();
    }

    public IActionResult Index()
    {
        List<Order> list = MyDbContext.Orders.Include(order => order.Customer).ToList();
        // list.ForEach(order =>
        // {
        //     order.Customer = MyDbContext.Customers.Find(order.CustomerId);
        // });
        return View(list);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Search(String CustomerName)
    {
        if (CustomerName == null || CustomerName.Equals("") || CustomerName.Equals(" "))
        {
            return RedirectToAction(nameof(Index));
        }
        
        List<Order> ls = MyDbContext.Orders.Where(order => (order.Customer.FirstName + order.Customer.LastName).ToLower().Contains(CustomerName.ToLower())
                                                                    ).ToList();
        return View("Index", ls);
    }
    
    [HttpGet]
    [Route("Default/OrderDetail/{orderId:guid}")]
    public IActionResult OrderDetail(Guid orderId)
    {
        List<ProductOder> ls = MyDbContext.ProductOders.Where(productOder => productOder.Order.Id == orderId).ToList();
        return View("OrderDetail", ls);
    }
    
    [HttpGet]
    [Route("Default/EditOrder/{orderId:guid}")]
    public IActionResult EditOrder(Guid orderId)
    {
        Models.EditOrder editOrder = new EditOrder
        {
            Order = MyDbContext.Orders.Find(orderId),
            ProductOders = MyDbContext.ProductOders.Where(productOder => productOder.Order.Id == orderId).ToList()
        };
        return View("EditOrder", editOrder);
    }
    
    [HttpPost]
    [Route("Default/EditOrder/")]
    public IActionResult EditOrder(EditOrder editOrder)
    {
        MyDbContext.Orders.Update(editOrder.Order);
        MyDbContext.ProductOders.UpdateRange(editOrder.ProductOders);
        MyDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    [Route("Default/DeleteOrder/{orderId:guid}")]
    public IActionResult DeleteOrder(Guid orderId)
    {
        //xóa tất cả các ProductOders trước 
        List<ProductOder> lsDelete = MyDbContext.ProductOders.Where(productOder => productOder.Order.Id == orderId).ToList();
        MyDbContext.ProductOders.RemoveRange(lsDelete);
        MyDbContext.Orders.Remove(new Order { Id = orderId });
        MyDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public void AddDataa()
    {
        var customers = new List<Customer>
        {
            new Customer { FirstName = "Nguyen", LastName = "A", Phone = "0123456789", Email = "nguyenA@gmail.com", },
            new Customer { FirstName = "Tran", LastName = "B", Phone = "0123456780", Email = "tranB@gmail.com", },
            new Customer { FirstName = "Le", LastName = "C", Phone = "0123456708", Email = "leC@gmail.com", },
            new Customer { FirstName = "Pham", LastName = "D", Phone = "0123456078", Email = "phamD@gmail.com", },
            new Customer { FirstName = "Hoang", LastName = "E", Phone = "0123465789", Email = "hoangE@gmail.com", }
        };
        MyDbContext.Customers.AddRange(customers);
        
        var products = new List<Product>
        {
            new Product { Name = "Product A", Price = 100.98 },
            new Product { Name = "Product B", Price = 20.59 },
            new Product { Name = "Product C", Price = 50.95 },
            new Product { Name = "Product D", Price = 40.97 },
            new Product { Name = "Product E", Price = 50.6 }
        };
        MyDbContext.Products.AddRange(products);
        
        var oders = new List<Order>
        {
            new Order { Customer = customers[0], OderDate = DateTime.Now},
            new Order { Customer = customers[1], OderDate = DateTime.Now },
            new Order { Customer = customers[1], OderDate = DateTime.Now },
            new Order { Customer = customers[3], OderDate = DateTime.Now },
            new Order { Customer = customers[2], OderDate = DateTime.Now }
        };
        MyDbContext.Orders.AddRange(oders);
        
        MyDbContext.SaveChanges();
    }
}