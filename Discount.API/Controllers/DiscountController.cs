using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _repository;

    public DiscountController(IDiscountRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{productName}", Name = "GetDiscount")]
    public async Task<IActionResult> Get(string productName)
        => Ok(await _repository.GetDiscount(productName));

    [HttpPost(Name ="CreateDiscount")]
    public async Task<IActionResult> Post([FromBody] Coupon coupon)
    {
        await _repository.CreateDiscount(coupon);
        return CreatedAtAction("GetDiscount", new { productName = coupon.ProductName});
    }

    [HttpPut(Name ="UpdateDiscount")]
    public async Task<IActionResult> Put([FromBody] Coupon coupon)
        => Ok(await _repository.UpdateDiscount(coupon));

    [HttpDelete("{productName}",Name ="DeleteDiscount")]
    public async Task<IActionResult> Delete(string productName)
        => Ok(await _repository.DeleteDiscount(productName));
}
