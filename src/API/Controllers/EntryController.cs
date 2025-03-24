using Application.Commands;
using Application.Queries;
using Application.Results;
using Domain.Interfaces.CQS;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/entries")]
    [Produces("application/json")]
    public class EntryController(
        ICommandHandlerWithTResult<CreateEntryCommand, EntryResponse> createHandler,
        ICommandHandlerWithTResult<UpdateEntryCommand, EntryResponse> updateHandler,
        ICommandHandlerWithoutTResult<DeleteEntryCommand> deleteHandler,
        IQueryHandlerWithTResultList<GetAllEntriesQuery, EntryResponse> getAllHandler,
        IQueryHandlerWithTResult<GetEntryByIdQuery, EntryResponse> getByIdHandler,
        IQueryHandlerWithTResult<GetEntriesReportQuery, EntryReportResponse> getReportHandler
    ) : ControllerBase
    {
        private readonly ICommandHandlerWithTResult<CreateEntryCommand, EntryResponse> _createHandler = createHandler;
        private readonly ICommandHandlerWithTResult<UpdateEntryCommand, EntryResponse> _updateHandler = updateHandler;
        private readonly ICommandHandlerWithoutTResult<DeleteEntryCommand> _deleteHandler = deleteHandler;
        private readonly IQueryHandlerWithTResultList<GetAllEntriesQuery, EntryResponse> _getAllHandler = getAllHandler;
        private readonly IQueryHandlerWithTResult<GetEntryByIdQuery, EntryResponse> _getByIdHandler = getByIdHandler;
        private readonly IQueryHandlerWithTResult<GetEntriesReportQuery, EntryReportResponse> _getReportHandler = getReportHandler;

        /// <summary>
        /// Retorna um relatório com saldo consolidado.
        /// </summary>
        /// <param name="query"></param>
        [HttpGet]
        [Route("reports")]
        [ProducesResponseType(typeof(EntryReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEntriesQueryAsync([FromQuery] GetEntriesReportQuery query)
        {
            return Ok(await _getReportHandler.HandleAsync(query));
        }

        /// <summary>
        /// Retorna uma lista de lançamentos cadastrados.
        /// </summary>
        /// <param name="query"></param>
        [HttpGet]
        [ProducesResponseType(typeof(IList<EntryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEntriesQueryAsync([FromQuery] GetAllEntriesQuery query)
        {
            return Ok(await _getAllHandler.HandleAsync(query));
        }

        /// <summary>
        /// Retorna um lançamento cadastrado.
        /// </summary>
        /// <param name="query"></param>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IList<EntryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEntryByIdAsync([FromRoute] GetEntryByIdQuery query)
        {
            return Ok(await _getByIdHandler.HandleAsync(query));
        }

        /// <summary>
        /// Cadastra um novo lançamento.
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        [ProducesResponseType(typeof(EntryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IList<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEntryCommandAsync([FromBody] CreateEntryCommand command)
        {
            return Created(string.Empty, await _createHandler.HandleAsync(command));
        }

        /// <summary>
        /// Atualiza um lançamento.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(typeof(EntryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IList<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEntryCommandAsync([FromRoute] Guid id, [FromBody] UpdateEntryCommand command)
        {
            command.Id = id;
            return Ok(await _updateHandler.HandleAsync(command));
        }

        /// <summary>
        /// Deleta um lançamento.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IList<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEntryCommandAsync([FromRoute] Guid id)
        {
            await _deleteHandler.HandleAsync(new DeleteEntryCommand { Id = id });
            return NoContent();
        }
    }
}