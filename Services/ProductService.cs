using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.DTOs.Product;
using SimpleApi.Models;

namespace SimpleApi.Services;

public class ProductService(AppDbContext db) : IProductService
{
    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        return await db.Products
            .OrderBy(p => p.Id)
            .Select(p => ToResponse(p))
            .ToListAsync();
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await db.Products.FindAsync(id);
        return product is null ? null : ToResponse(product);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();

        return ToResponse(product);
    }

    public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return null;

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();

        return ToResponse(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return false;

        db.Products.Remove(product);
        await db.SaveChangesAsync();

        return true;
    }

    private static ProductResponse ToResponse(Product p) =>
        new(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CreatedAt, p.UpdatedAt);
}
