using System.ComponentModel.DataAnnotations;

namespace SimpleApi.DTOs.Product;

public record CreateProductRequest(
    [Required, MaxLength(200)] string Name,
    string? Description,
    [Range(0, double.MaxValue)] decimal Price,
    [Range(0, int.MaxValue)] int Stock
);

public record UpdateProductRequest(
    [Required, MaxLength(200)] string Name,
    string? Description,
    [Range(0, double.MaxValue)] decimal Price,
    [Range(0, int.MaxValue)] int Stock
);

public record ProductResponse(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
