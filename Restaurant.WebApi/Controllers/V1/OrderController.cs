using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Order;
using Restaurant.Core.Application.Features.Dish___Copia.Commands.DeleteOrder;
using Restaurant.Core.Application.Features.Ingredient.Commands.CreateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Commands.UpdateIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetAllIngredient;
using Restaurant.Core.Application.Features.Ingredient.Queries.GetIngredientById;
using Restaurant.Core.Application.Features.Order.Commands.UpdateOrder;
using Restaurant.Core.Application.Wrappers;

namespace Restaurant.WebApi.Controllers.V1
{
    [Authorize(Roles = "Waiter")]
    public class OrderController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<OrderResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            Response<IList<OrderResponse>> response = await Mediator.Send(new GetAllOrdersQuery());
            return response != null ? Ok(response) : NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
           Response<OrderResponse> response = await Mediator.Send(new GetOrderByIdQuery() { Id = id });
           return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {

          if (!ModelState.IsValid)
          {
            return BadRequest();
          }

          await Mediator.Send(command);
          return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(UpdateOrderResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateOrderCommand command, int Id)
        {

          if (!ModelState.IsValid || command.Id != Id)
          {
           return BadRequest();
          }

           var result = await Mediator.Send(command);
           return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int Id)
        {
           await Mediator.Send(new DeleteOrderCommand() { Id = Id });
           return NoContent();
        }
    }
}
