using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Ingredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById;
using Restaurant.Core.Application.Wrappers;

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
           Response<IList<IngredientResponse>> response = await Mediator.Send(new GetAllIngredientQuery());
           return response != null  ? Ok(response) : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            Response<IngredientResponse> response = await Mediator.Send(new GetIngredientByIdQuery() { Id = id });
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateIngredientCommand command)
        {
          if (!ModelState.IsValid)
          {
             return BadRequest();
          }

          await Mediator.Send(command);
          return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(UpdateIngredientResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateIngredientCommand command, int Id)
        {
          if (!ModelState.IsValid || command.Id != Id)
          {
            return BadRequest();
          }

          var result = await Mediator.Send(command);
          return Ok(result);
        }
    }
}
