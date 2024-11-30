﻿using BuildingBlocks.CQRS;
using CatalogApi.Models;
using Marten;

namespace CatalogApi.Products.GetProductByCategory
{

    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    //*******************

    internal class GetProductByCategoryQueryHandler( IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            //logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query) ; // add to loggingBehavior

            var products = await session.Query<Product>()
                .Where(p=> p.Category.Contains(query.Category))
                .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}