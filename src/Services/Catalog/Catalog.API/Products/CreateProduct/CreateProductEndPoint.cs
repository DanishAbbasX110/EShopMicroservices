

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProdutRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(int Id);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/product",
                async (CreateProdutRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateProductCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<CreateProductResponse>();
                    return Results.Created($"/products/{response.Id}", response);

                })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
            
        }
    }
}
