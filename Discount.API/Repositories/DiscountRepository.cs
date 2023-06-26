using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using NpgsqlConnection connection = GetConnectionPostgreSQL();

        var affected = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName, Description, Amount) " +
            "VALUES (@ProductName,@Description,@Amount)",
            new {ProductName = coupon.ProductName,
                 Description = coupon.Description,
                 Amount = coupon.Amount});

        return !(affected == 0);
    }

    public async Task<bool> DeleteDiscount(string productName)
    {

        using NpgsqlConnection connection = GetConnectionPostgreSQL();

        var affected = await connection.ExecuteAsync(
            "DELETE FROM Coupon " +
            "WHERE ProductName = @ProductName",
            new
            {
                ProductName = productName,
            });

        return !(affected == 0);
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        using NpgsqlConnection connection = GetConnectionPostgreSQL();

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName});

        return coupon == null
            ? new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" }
            : coupon;
    }
   
    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using NpgsqlConnection connection = GetConnectionPostgreSQL();

        var affected = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName = @ProductName," +
            "           Description = @Description," +
            "           Amount = @Amount " +
            " WHERE Id = @Id",
            new
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
                Id = coupon.Id
            });

        return !(affected == 0);
    }

    private NpgsqlConnection GetConnectionPostgreSQL()
    {
        return new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
    }
}
