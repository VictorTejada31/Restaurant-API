﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Table;
using Restaurant.Core.Application.Features.Table.Commands.ChangeStatus;
using Restaurant.Core.Application.Features.Table.Commands.CreateTableCommand;
using Restaurant.Core.Application.Features.Table.Commands.UpdateTable;
using Restaurant.Core.Application.Features.Table.Queries.GetAllById;
using Restaurant.Core.Application.Features.Table.Queries.GetAllTables;
using Restaurant.Core.Application.Features.Table.Queries.GetTableOrders;
using Restaurant.Core.Application.Wrappers;

namespace Restaurant.WebApi.Controllers.V1
{
    public class TableController : BaseApiController
    {
        [Authorize(Roles = "Admin,Waiter")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TableResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        public async Task<IActionResult> Post(CreateTableCommand command)
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
        public async Task<IActionResult> Put(UpdateTableCommand command, int Id)
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
        public async Task<IActionResult> ChangeTableStatus(ChangeStatusCommand command, int id)
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
