﻿using SocialMediaBackend.Application.Repositories.Categories;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Categories
{
    public class EfCategoryWriteRepository : EfWriteRepository<PostCategory, SocialMediaDbContext>, ICategoryWriteRepository
    {
        public EfCategoryWriteRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}
