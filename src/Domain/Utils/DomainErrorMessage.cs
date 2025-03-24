namespace Domain.Utils
{
    public static class DomainErrorMessage
    {
        public const int BadRequestErrorCode = 40000; 
        public const string TitleErrorMessage = "Requisição ruim";
        public const string InvalidDateErrorMessage = "Data do lançamento é inválida.";
        public const string RequiredDateErrorMessage = "Data do lançamento deve ser informada.";
        public const string RequiredDescriptionErrorMessage = "Descrição do lançamento deve ser informada.";
        public const string ValueErrorMessage = "O valor do lançamento deve ser maior que 0.";
        public const string TypeErrorMessage = "Tipo do lançamento é inválido.";
        public const string InvalidId = "O id do lançamento deve ser informado.";
        public const string EntryNotFound = "Lançamento não encontrado.";
    }
}