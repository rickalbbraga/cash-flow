using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces.CQS;

namespace Application.Results
{
    [ExcludeFromCodeCoverage]
    public record ErrorResponse : IResult
    {
        /// <summary>
        /// Código do erro.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Título do erro.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Detalhe do erro.
        /// </summary>
        public string? Message { get; set; }
    }
}