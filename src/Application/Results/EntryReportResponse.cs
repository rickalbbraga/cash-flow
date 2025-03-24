using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces.CQS;

namespace Application.Results
{
    [ExcludeFromCodeCoverage]
    public class EntryReportResponse : IResult
    {
        /// <summary>
        /// Data do relatório.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Soma das entradas.
        /// </summary>
        public decimal Incomings { get; set; }

        /// <summary>
        /// Soma das saídas
        /// </summary>
        public decimal Outcomings { get; set; }

        /// <summary>
        /// Saldo do dia.
        /// </summary>
        public decimal Balance { get; set; }
    }
}