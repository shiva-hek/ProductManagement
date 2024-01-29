using AutoMapper;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Products.Commands.UpdateProduct;
using ProductManagement.Application.Products.Queries.GetProducts;
using ProductManagement.Domain.Aggregates.Products;

namespace ProductManagement.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommandRequest, Product>();
            CreateMap<Product, CreateProductCommandResponse>()
                .ForMember(dest => dest.Name, source => source.MapFrom(customer => customer.Name.Value))
                .ForMember(dest => dest.ProduceDate, source => source.MapFrom(customer => customer.ProduceDate.Value))
                .ForMember(dest => dest.ManufacturePhone, source => source.MapFrom(customer => customer.ManufacturePhone.Value))
                .ForMember(dest => dest.ManufactureEmail, source => source.MapFrom(customer => customer.ManufactureEmail.Value));


            CreateMap<UpdateProductCommandRequest, Product>();
            CreateMap<Product, UpdateProductCommandResponse>()
                .ForMember(dest => dest.Name, source => source.MapFrom(customer => customer.Name.Value))
                .ForMember(dest => dest.ProduceDate, source => source.MapFrom(customer => customer.ProduceDate.Value))
                .ForMember(dest => dest.ManufacturePhone, source => source.MapFrom(customer => customer.ManufacturePhone.Value))
                .ForMember(dest => dest.ManufactureEmail, source => source.MapFrom(customer => customer.ManufactureEmail.Value));


            CreateMap<Product, GetProductsQueryResponse>()
                .ForMember(dest => dest.Name, source => source.MapFrom(customer => customer.Name.Value))
                .ForMember(dest => dest.ProduceDate, source => source.MapFrom(customer => customer.ProduceDate.Value))
                .ForMember(dest => dest.ManufacturePhone, source => source.MapFrom(customer => customer.ManufacturePhone.Value))
                .ForMember(dest => dest.ManufactureEmail, source => source.MapFrom(customer => customer.ManufactureEmail.Value))
                .ForMember(dest => dest.CreatorName, source => source.MapFrom(customer => customer.Creator.FirstName + " " + customer.Creator.LastName));

        }
    }
}