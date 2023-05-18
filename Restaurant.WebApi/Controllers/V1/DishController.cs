using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Features.Dish.Commands.UpdateDish;
using Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById;
using Restaurant.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Restaurant.WebApi.Controllers.V1
{
    [Authorize(Roles = "Admin")]
    [SwaggerTag("Dish Maintenance")]
    public class DishController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<DishResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Dish List",
            Description = "Return all available dishes."
            )]
        public async Task<IActionResult> GetAll()
        {

           Response<IList<DishResponse>> response = await Mediator.Send(new GetAllDishQuery());
           return response != null ? Ok(response) : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Dish By Id",
            Description = "Return a specified dish"
            )]
        public async Task<IActionResult> GetById(int id)
        {

           Response<DishResponse> response = await Mediator.Send(new GetDishByIdQuery() { Id = id });
           return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Create Dish",
            Description = "Receives the necessary parameters to create a new dish."
            )]
        public async Task<IActionResult> Post([FromBody]CreateDishCommand command)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);
                return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(UpdateDishResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Update Dish",
            Description = "Receives the necessary parameters to update a specified dish."
            )]
        public async Task<IActionResult> Put([FromBody]UpdateDishCommand command, int Id)
        {
            try
            {
                if (!ModelState.IsValid || command.Id != Id)
                {
                    return BadRequest();
                }

               var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
