namespace FreeEducation.Services.Basket.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public string EducationId { get; set; }
        public string EducationName { get; set; }
        public decimal Price { get; set; }

    }
}
