using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application;
using Shop.Application.Categories;
using Shop.Application.Products;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.SellerAgg.Service;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure;
using Shop.Query;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependency(this IServiceCollection service, string connectionString)
        {
            //inject Repositories on InfrastructureBootstrapper
            InfrastructureBootstrapper.Init(service, connectionString);

            //inject MediatR with Assembly           
            service.AddMediatR(x=>x.RegisterServicesFromAssembly(typeof(QueryAssembly).Assembly));            

            service.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly));

            //inject FluentValidation with Assembly           
            service.AddValidatorsFromAssembly(typeof(ApplicationAssembly).Assembly);

            //inject DomainService
            service.AddTransient<ICategoryDomainService, CategoryDomainService>();
            service.AddTransient<IProductDomainService, ProductDomainService>();
            service.AddTransient<ISellerDomainService, SellerDomainService>();
            service.AddTransient<IUserDomainService, UserDomainService>();

        }
    }
}
