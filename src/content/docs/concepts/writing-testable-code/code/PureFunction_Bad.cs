public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryService _inventoryService;
    private readonly IEmailService _emailService;

    public OrderService(
        IOrderRepository orderRepository, 
        IInventoryService inventoryService, 
        IEmailService emailService)
    {
        _orderRepository = orderRepository;
        _inventoryService = inventoryService;
        _emailService = emailService;
    }

    public async Task<OrderResult> ProcessOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        
        // Validate order
        if (order == null) return new OrderResult { Success = false, Message = "Order not found" };
        if (order.Status != "Pending") return new OrderResult { Success = false, Message = "Order already processed" };
        if (order.Items.Count == 0) return new OrderResult { Success = false, Message = "Order has no items" };
        
        // Check inventory
        foreach (var item in order.Items)
        {
            var stock = await _inventoryService.GetStockAsync(item.ProductId);
            if (stock < item.Quantity)
            {
                return new OrderResult { Success = false, Message = $"Insufficient stock for {item.ProductName}" };
            }
        }
        
        // Calculate totals
        decimal subtotal = 0;
        decimal tax = 0;
        foreach (var item in order.Items)
        {
            subtotal += item.Price * item.Quantity;
        }
        tax = subtotal * 0.08m; // 8% tax
        var total = subtotal + tax;
        
        // Apply discount if applicable
        if (order.CustomerType == "Premium" && subtotal > 100)
        {
            total = total * 0.9m; // 10% discount
        }
        
        // Update order
        order.Subtotal = subtotal;
        order.Tax = tax;
        order.Total = total;
        order.Status = "Processed";
        await _orderRepository.UpdateOrderAsync(order);
        
        // Send email
        await _emailService.SendOrderConfirmationAsync(order.CustomerEmail, order);
        
        return new OrderResult { Success = true, OrderTotal = total };
    }
}