using AutoMapper;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient;
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

            #endregion

            #region Ingredient

            CreateMap<Ingredient, CreateIngredientCommand>()
                .ReverseMap()
                .ForMember(member => member.Id, op => op.Ignore());

            CreateMap<Ingredient, UpdateIngredientCommand>()
                .ReverseMap()
                .ForMember(member => member.Id, op => op.Ignore());

            CreateMap<Ingredient, IngredientResponse>()
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

            #endregion

            #region Dish

            CreateMap<Order, CreateOrderCommand>()
                .ReverseMap()
                .ForMember(member => member.Table, op => op.Ignore());
            CreateMap<Order, OrderResponse>()
                .ReverseMap()
                .ForMember(member => member.TableId, op => op.Ignore());
                

            #endregion

        }



    }
}
