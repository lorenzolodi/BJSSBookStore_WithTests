using BookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookService.Extensions
{
    public static class ModelExtensions
    {
        public static IQueryable<T> FilterEnvironment<T>(this IQueryable<T> entities, Guid envId)
            where T : EnvironmentEntity
        {
            return entities.Where(e => e.EnvironmentId == envId);
        }
    }
}