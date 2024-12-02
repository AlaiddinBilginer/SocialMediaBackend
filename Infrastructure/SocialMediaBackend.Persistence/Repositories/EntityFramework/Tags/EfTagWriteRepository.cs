﻿using SocialMediaBackend.Application.Repositories.Tags;
using SocialMediaBackend.Domain.Entities;
using SocialMediaBackend.Persistence.Contexts;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework.Tags
{
    public class EfTagWriteRepository : EfWriteRepository<Tag, SocialMediaDbContext>, ITagWriteRepository
    {
        public EfTagWriteRepository(SocialMediaDbContext context) : base(context)
        {
        }
    }
}