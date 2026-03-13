using AutoMapper;
using RestaurantServiceAPI.Application.DTOs;
using RestaurantServiceAPI.Domain.Entities;
using RestaurantServiceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Order mapping

        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.Table.Number))
            .ForMember(dest => dest.WaiterName, opt => opt.MapFrom(src => src.Waiter.FirstName + " " + src.Waiter.LastName))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

        CreateMap<CreateOrderRequestDto, Order>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => OrderStatus.Created))
            .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(_ => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.Table, opt => opt.Ignore())
            .ForMember(dest => dest.Waiter, opt => opt.Ignore());

        CreateMap<UpdateOrderStatusRequestDto, Order>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)));

        // OrderItem mapping

        CreateMap<OrderItem, OrderItemResponseDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

        CreateMap<CreateOrderItemRequestDto, OrderItem>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => OrderItemStatus.Pending))
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        CreateMap<UpdateOrderItemRequestDto, OrderItem>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<OrderItemStatus>(src.Status, true)));

        // Product mapping

        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));

        CreateMap<CreateProductRequestDto, Product>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<MenuCategory>(src.Category, true)));

        CreateMap<UpdateProductRequestDto, Product>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<MenuCategory>(src.Category, true)));

        // Table mapping

        CreateMap<Table, TableResponseDto>();

        CreateMap<CreateTableRequestDto, Table>();

        CreateMap<UpdateTableRequestDto, Table>();

        // Waiter mapping

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>> TODO <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    }
}
