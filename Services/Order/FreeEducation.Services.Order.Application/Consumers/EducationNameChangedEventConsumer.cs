using FreeEducation.Services.Order.Infrastructure;
using FreeEducation.Shared.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FreeEducation.Services.Order.Application.Consumers;

public class EducationNameChangedEventConsumer : IConsumer<EducationNameChangedEvent>
{
    private readonly OrderDbContext _orderDbContext;

    public EducationNameChangedEventConsumer(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
    }

    public async Task Consume(ConsumeContext<EducationNameChangedEvent> context)
    {
        var orderItems = await _orderDbContext.OrderItems
            .Where(x=>x.ProductId == context.Message.EducationId).ToListAsync();
        
        orderItems.ForEach(x =>
        {
            x.UpdateOrderItem(context.Message.UpdatedName,x.Price,x.PictureUrl);
        });
        await _orderDbContext.SaveChangesAsync();
    }
}