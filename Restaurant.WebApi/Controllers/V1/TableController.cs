using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Features.Table.Commands.ChangeStatus;
using Restaurant.Core.Application.Features.Table.Commands.CreateTableCommand;
using Restaurant.Core.Application.Features.Table.Commands.UpdateTable;
using Restaurant.Core.Application.Features.Table.Queries.GetAllById;
using Restaurant.Core.Application.Features.Table.Queries.GetAllTables;
using Restaurant.Core.Application.Features.Table.Queries.GetTableOrders;
using Restaurant.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Restaurant.WebApi.Controllers.V1
{
    [SwaggerTag("Table Maintenance")]
    public class TableController : BaseApiController
    {
        [Authorize(Roles = "Admin,Waiter")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TableResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Table List",
            Description = "Return all tables."
            )]
        public async Task<IActionResult> GetAll()
        {
           Response<IList<TableResponse>> response = await Mediator.Send(new GetAllTablesQuery());
           return Ok(response);
        }

        [Authorize(Roles = "Waiter")]
        [HttpGet("{id}/GetTableOrders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableOrdersResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Table Orders By Id",
            Description = "Return all orders from a specified table."
            )]
        public async Task<IActionResult> GetTableOrders(int id)
        {
            Response<TableOrdersResponse> response = await Mediator.Send(new GetTableOrdersQuery() { Id = id });
            return response != null ? Ok(response) : NotFound();

        }

        [Authorize(Roles = "Admin,Waiter")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Table By Id",
            Description = "Return a specified table."
            )]
        public async Task<IActionResult> GetById(int id)
        {

            Response<TableResponse> response = await Mediator.Send(new GetTableByIdQuery() { Id = id });
            return response != null ? Ok(response) : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Create Order",
            Description = "Receives the necessary parameters to create a new table."
            )]
        public async Task<IActionResult> Post([FromBody]CreateTableCommand command)
        {

            if (!ModelState.IsValid)
            {
               return BadRequest();
            }

             await Mediator.Send(command);
             return NoContent();
            
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Update Table",
            Description = "Receives the necessary parameters to update a specified table."
            )]
        public async Task<IActionResult> Put([FromBody]UpdateTableCommand command, int Id)
        {
            if (!ModelState.IsValid || command.Id != Id)
            {
              return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/ChangeTableStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Change Table Status",
            Description = "Receives the necessary parameters to change the status of a specified table."
            )]
        public async Task<IActionResult> ChangeTableStatus([FromBody]ChangeStatusCommand command, int id)
        {
            if (!ModelState.IsValid || command.Id != id)
            {
              return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

    }
}
