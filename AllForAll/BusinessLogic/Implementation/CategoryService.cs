﻿using AllForAll.Models;
using AutoMapper;
using BusinessLogic.Dto.Category;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly AllForAllDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(AllForAllDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateCategoryAsync(CategoryRequestDto category, CancellationToken cancellation = default)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            var createdCategory = await _dbContext.Categories.AddAsync(mappedCategory, cancellation);
            await _dbContext.SaveChangesAsync(cancellation);
            return createdCategory.Entity.CategoryId;
        }

        public async Task DeleteCategoryAsync(int id, CancellationToken cancellation = default)
        {
            var categoryToDelete = await _dbContext.Categories.FindAsync(id, cancellation);
            if (categoryToDelete != null)
            {
                _dbContext.Categories.Remove(categoryToDelete);
                await _dbContext.SaveChangesAsync(cancellation);
            }
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync(CancellationToken cancellation = default)
        {
            return await _dbContext.Categories.ToListAsync(cancellation);
        }

        public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id, cancellation);
        }

        public async Task<bool> IsCategoryExistAsync(int id, CancellationToken cancellation = default)
        {
            return await _dbContext.Categories.AnyAsync(c => c.CategoryId == id, cancellation);
        }

        public async Task UpdateCategoryAsync(int id, CategoryRequestDto category, CancellationToken cancellation = default)
        {
            var categoryToUpdate = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id, cancellation);
            if (categoryToUpdate != null)
            {
                _mapper.Map(category, categoryToUpdate);
                _dbContext.Update(categoryToUpdate);
                await _dbContext.SaveChangesAsync(cancellation);
            }
        }

        public async Task UpdateCategoryPhotoLinkAsync(int id, string categoryPhotoLink, CancellationToken cancellationToken = default)
        {
            var categoryToUpdate = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id, cancellationToken);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.CategoryPhotoLink = categoryPhotoLink;
                _dbContext.Update(categoryToUpdate);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
