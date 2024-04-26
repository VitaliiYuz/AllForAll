
using AllForAll.Models;
using AutoMapper;
using BusinessLogic.Dto.Product;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Implementation
{
    public class ProductService : IProductService
    {
        private readonly AllForAllDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductService(AllForAllDbContext dbContext, IMapper mapper) { 
            _dbContext = dbContext;
            _mapper = mapper;
        }
        

        public async Task<int> CreateProductAsync(ProductRequestDto product, CancellationToken cancellation = default)
        {
            var mappedProduct = _mapper.Map<Product>(product);

            var createdProduct = await _dbContext.Products.AddAsync(mappedProduct, cancellation);

            await _dbContext.SaveChangesAsync(cancellation);

            return createdProduct.Entity.ProductId;
        }

        public async Task DeleteProductAsync(int id, CancellationToken cancellation = default)
        {
            var productToDelete = await _dbContext.Products.FindAsync(id, cancellation);
            if (productToDelete != null)
            {
                _dbContext.Products.Remove(productToDelete);
                await _dbContext.SaveChangesAsync(cancellation);
            }
            
        }

        public async Task<ICollection<Product>> GetAllProductsAsync(CancellationToken cancellation = default)
        {
            return await _dbContext.Products.ToListAsync(cancellation);
        }

        public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id , cancellation);
        }

        public async Task<bool> IsProductExistAsync(int id, CancellationToken cancellation = default)
        {
            return await _dbContext.Products.AnyAsync(p => p.ProductId == id , cancellation);
        }

        public async Task UpdateProductAsync(int id, ProductRequestDto product, CancellationToken cancellation = default)
        {
            var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id , cancellation);
            if (productToUpdate != null)
            {
                _mapper.Map(product, productToUpdate);
                _dbContext.Update(productToUpdate);
                await _dbContext.SaveChangesAsync(cancellation);
            }
            
        }
        public async Task UpdateProductPhotoLinkAsync(int id, string productPhotoLink, CancellationToken cancellationToken = default)
        {
            var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);
            if (productToUpdate != null)
            {
                productToUpdate.ProductPhotoLink = productPhotoLink;
                _dbContext.Update(productToUpdate);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
