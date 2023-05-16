using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Dish;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Features.Dish.Commands.UpdateDish;
using Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById;

namespace Restaurant.WebApi.Controllers.V1
{
    [Authorize(Roles = "Admin")]
    public class DishController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<DishResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IList<DishResponse> response = await Mediator.Send(new GetAllDishQuery());
                return response != null || response.Count == 0 ? Ok(response) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                DishResponse response = await Mediator.Send(new GetDishByIdQuery() { Id = id });
                return response != null ? Ok(response) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateDishCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(UpdateDishResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateDishCommand command, int Id)
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
