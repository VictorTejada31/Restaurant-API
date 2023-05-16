using AutoMapper;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Features.Dish.Commands.UpdateDish;
using Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient;
using Restaurant.Core.Application.Features.Order.Commands.UpdateOrder;
using Restaurant.Core.Application.Features.Table.Commands.CreateTableCommand;
using Restaurant.Core.Application.Features.Table.Commands.UpdateTable;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() {

            #region Table

            CreateMap<Tables, TableResponse>()
                .ReverseMap();

            CreateMap<Tables, UpdateTableResponse>()
                .ReverseMap()
                .ForMember(member => member.Orders, op => op.Ignore());

            CreateMap<Tables, CreateTableCommand>()
                .ReverseMap()
                .ForMember(member => member.Orders, op => op.Ignore())
                .ForMember(member => member.Status, op => op.Ignore())
                .ForMember(member => member.Id, op => op.Ignore());

            CreateMap<Tables, UpdateTableCommand>()
                .ReverseMap();


            #endregion

            #region Ingredient

            CreateMap<Ingredient, CreateIngredientCommand>()
                .ReverseMap()
                .ForMember(member => member.Id, op => op.Ignore());

            CreateMap<Ingredient, UpdateIngredientCommand>()
                .ReverseMap();

            CreateMap<Ingredient, IngredientResponse>()
                .ReverseMap();

            CreateMap<Ingredient, UpdateIngredientResponse>()
                .ReverseMap();

            #endregion

            #region Dish

            CreateMap<Dish, CreateDishCommand>()
                .ReverseMap()
                .ForMember(member => member.Ingredients, op => op.Ignore());

            CreateMap<Dish, DishResponse>()
                .ForMember(member => member.Ingredients, op => op.Ignore())
                .ReverseMap()
                .ForMember(member => member.Ingredients, op => op.Ignore());

            CreateMap<Dish, UpdateDishResponse>()
                .ForMember(member => member.Ingredients, op => op.Ignore())
                .ReverseMap()
                .ForMember(member => member.Ingredients, op => op.Ignore())
                .ForMember(member => member.DishCategory, op => op.Ignore());

            CreateMap<Dish, UpdateDishCommand>()
                .ReverseMap();


            #endregion

            #region Order

            CreateMap<Order, CreateOrderCommand>()
                .ReverseMap()
                .ForMember(member => member.Table, op => op.Ignore());

            CreateMap<Order, OrderResponse>()
                .ForMember(member => member.Dishes, op => op.Ignore())
                .ReverseMap()
                .ForMember(member => member.Table, op => op.Ignore())
                .ForMember(member => member.Dishes, op => op.Ignore());

            CreateMap<Order, UpdateOrderCommand>()
              .ReverseMap();

            CreateMap<Order, UpdateOrderResponse>()
               .ForMember(member => member.Dishes, op => op.Ignore())
              .ReverseMap()
              .ForMember(member => member.Dishes, op => op.Ignore())
              .ForMember(member => member.Status, op => op.Ignore())
              .ForMember(member => member.TableId, op => op.Ignore());


            #endregion

        }



    }
}
