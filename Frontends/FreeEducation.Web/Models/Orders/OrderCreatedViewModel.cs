namespace FreeEducation.Web.Models.Orders
{
    public class OrderCreatedViewModel
    {
        public int OrderId { get; set; }
        public string Error { get; set; }
        public bool Successfull { get; set; }
    }
}
