using System.Diagnostics.CodeAnalysis;
using Domain.Enums;
using Domain.Interfaces.CQS;

namespace Application.Results
{
    [ExcludeFromCodeCoverage]
    public record EntryResponse : IResult
    {
        /// <summary>
        /// Id do lançamento.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Data do lançamento.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Descrição do lançamento.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Valor do lançamento.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Tipo do lançamento.
        /// </summary>
        public EntryType Type { get; set; }
    }
}