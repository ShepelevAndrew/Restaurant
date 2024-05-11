using Mapster;
using Restaurant.Controllers.Product.Response;
using Restaurant.Domain.Products;

namespace Restaurant.Common.Mapping;

public class ProductsMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductResponse>()
            .Map(dest => dest.AverageRating, src => src.AverageRating.Value)
            .Map(dest => dest.CountOfVoting, src => src.AverageRating.NumRatings);
    }
}