using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById;

namespace Restaurant.WebApi.Controllers.V1
{
    [Authorize(Roles = "Admin")]
    public class IngredientController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IngredientResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IList<IngredientResponse> response = await Mediator.Send(new GetAllIngredientQuery());
                return response != null || response.Count == 0 ? Ok(response) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                IngredientResponse response = await Mediator.Send(new GetIngredientByIdQuery() { Id = id });
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
        public async Task<IActionResult> Post(CreateIngredientCommand command)
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
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(UpdateIngredientResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateIngredientCommand command, int Id)
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
