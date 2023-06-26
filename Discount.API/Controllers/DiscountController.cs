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
    [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDiscount(string productName)
    {
       return Ok(await _repository.GetDiscount(productName));

    }

    [HttpPost(Name ="CreateDiscount")]
    [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] Coupon coupon)
    {
       var r =  await _repository.CreateDiscount(coupon);
        return CreatedAtAction("GetDiscount", new { productName = coupon.ProductName},coupon);
    }

    [HttpPut(Name ="UpdateDiscount")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Put([FromBody] Coupon coupon)
        => Ok(await _repository.UpdateDiscount(coupon));

    [HttpDelete("{productName}",Name ="DeleteDiscount")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string productName)
        => Ok(await _repository.DeleteDiscount(productName));
}
