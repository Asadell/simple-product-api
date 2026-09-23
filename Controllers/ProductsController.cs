using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleApi.DTOs.Common;
using SimpleApi.DTOs.Product;
using SimpleApi.Services;

namespace SimpleApi.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductsController(IProductService productService) : ControllerBase
{
    /// <summary>Get semua produk.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<ProductResponse>>.Ok(products, "Data produk berhasil diambil"));
    }

    /// <summary>Get produk berdasarkan ID.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await productService.GetByIdAsync(id);
        if (product is null)
            return NotFound(ApiResponse<object>.Fail($"Produk dengan id {id} tidak ditemukan."));

        return Ok(ApiResponse<ProductResponse>.Ok(product, "Data produk berhasil diambil"));
    }

    /// <summary>Tambah produk baru.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var product = await productService.CreateAsync(request);
        var wrapped = ApiResponse<ProductResponse>.Ok(product, "Produk berhasil dibuat");
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, wrapped);
    }

    /// <summary>Update produk berdasarkan ID.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var product = await productService.UpdateAsync(id, request);
        if (product is null)
            return NotFound(ApiResponse<object>.Fail($"Produk dengan id {id} tidak ditemukan."));

        return Ok(ApiResponse<ProductResponse>.Ok(product, "Produk berhasil diupdate"));
    }

    /// <summary>Hapus produk berdasarkan ID.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await productService.DeleteAsync(id);
        if (!deleted)
            return NotFound(ApiResponse<object>.Fail($"Produk dengan id {id} tidak ditemukan."));

        return Ok(ApiResponse<object>.Ok(null, $"Produk dengan id {id} berhasil dihapus."));
    }
}
