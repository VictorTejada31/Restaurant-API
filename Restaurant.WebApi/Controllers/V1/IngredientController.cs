using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Ingredient;
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
    public class IngredientController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IngredientResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Ingredient List",
            Description = "Return all available dishes."
            )]
        public async Task<IActionResult> GetAll()
        {
           Response<IList<IngredientResponse>> response = await Mediator.Send(new GetAllIngredientQuery());
           return response != null  ? Ok(response) : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Ingredient By Id",
            Description = "Return a specified ingredient."
            )]
        public async Task<IActionResult> GetById(int id)
        {
            Response<IngredientResponse> response = await Mediator.Send(new GetIngredientByIdQuery() { Id = id });
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Create Ingredient",
            Description = "Receives the necessary parameters to create a new ingredient."
            )]

        public async Task<IActionResult> Post([FromBody]CreateIngredientCommand command)
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
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Update Ingredient",
            Description = "Receives the necessary parameters to update a specified ingredient."
            )]
        public async Task<IActionResult> Put([FromBody]UpdateIngredientCommand command, int Id)
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
