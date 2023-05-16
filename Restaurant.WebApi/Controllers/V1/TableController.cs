using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Features.Table.Commands.ChangeStatus;
using Restaurant.Core.Application.Features.Table.Commands.CreateTableCommand;
using Restaurant.Core.Application.Features.Table.Commands.UpdateTable;
using Restaurant.Core.Application.Features.Table.Queries.GetAllById;
using Restaurant.Core.Application.Features.Table.Queries.GetAllTables;
using Restaurant.Core.Application.Features.Table.Queries.GetTableOrders;

namespace Restaurant.WebApi.Controllers.V1
{
    public class TableController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TableResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IList<TableResponse> response = await Mediator.Send(new GetAllTablesQuery());
                return response != null || response.Count != 0 ? Ok(response) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/GetTableOrders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableOrdersResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTableOrders(int id)
        {
            try
            {
                TableOrdersResponse response = await Mediator.Send(new GetTableOrdersQuery() { Id = id });
                return response != null ? Ok(response) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                TableResponse response = await Mediator.Send(new GetTableByIdQuery() { Id = id });
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
        public async Task<IActionResult> Post(CreateTableCommand command)
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateTableCommand command, int Id)
        {
            try
            {
                if (!ModelState.IsValid || command.Id != Id)
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

        [HttpPut("{id}/ChangeTableStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeTableStatus(ChangeStatusCommand command, int id)
        {
            try
            {
                if (!ModelState.IsValid || command.Id != id)
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

    }
}
