using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;
using Domain.Interfaces.CQS;
using Domain.Utils;

namespace Application.Commands
{
    [ExcludeFromCodeCoverage]
    public record CreateEntryCommand : ICommand
    {
        /// <summary>
        /// A data do lançamento.
        /// </summary>
        [Required(ErrorMessage = DomainErrorMessage.RequiredDateErrorMessage)]
        public DateTime Date { get; set; }

        /// <summary>
        /// A descrição do lançamento.
        /// </summary>
        [Required(ErrorMessage = DomainErrorMessage.RequiredDescriptionErrorMessage)]
        public string? Description { get; set; }
        
        /// <summary>
        /// O valor do lançamento.
        /// </summary>
        [EntryValueCustomValidation]
        public decimal Value { get; set; }
        
        /// <summary>
        /// O tipo do lançamento (1 - Debit, 2 - Credit).
        /// </summary>
        [EnumDataType(typeof(EntryType), ErrorMessage = DomainErrorMessage.TypeErrorMessage)]
        [Required(ErrorMessage = DomainErrorMessage.TypeErrorMessage)]
        public EntryType Type { get; set; }
    }
}