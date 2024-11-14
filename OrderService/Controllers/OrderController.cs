using System.Security.Claims;
using Customer.Domain.Orders.Contracts;
using Customer.Domain.Orders.Mappings;
using Customer.Domain.Orders.Models.Commands;
using Customer.Domain.Orders.Models.Requests;
using Customer.Domain.Orders.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController(
    IOrderService service,
    IHttpContextAccessor context) : ControllerBase
{
    [HttpPost]
    public async Task<OrderResponse> PostAsync(OrderPostRequest order, CancellationToken token = default)
    {
        var command = order.ToCommand(Guid.Parse(context.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));
        
        var result = await service.CreateAsync(command, token);

        return result.ToResponse();
    }

    [HttpPatch]
    public async Task<OrderResponse> UpdateAsync(OrderPatchRequest order, CancellationToken token = default)
    {
        var command = order.ToCommand(Guid.Parse(context.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));
        
        var result = await service.UpdateAsync(command, token);

        return result.ToResponse();
    }

    [HttpDelete("orderId")]
    public async Task DeleteAsync(Guid orderId, CancellationToken token = default)
    {
        var userId = Guid.Parse(context.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        await service.DeleteAsync(new OrderDeleteCommand(orderId, userId), token);
    }

    [HttpGet("list")]
    public async Task<OrderResponse[]> GetListAsync([FromQuery] OrderRequest request, CancellationToken token = default)
    {
        var query = request.ToQuery(Guid.Parse(context.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));
        
        var results = await service.GetListAsync(query, token);

        return results.ToResponse();
    }
}
