using Domain.Entities;
using Domain.Enums;
using Domain.Utils;
using FluentAssertions;

namespace Test.Domain.Entities
{
    public class EntryTest
    {
        [Fact(DisplayName = "Create - Erro ao criar um lançamento com a data com valor mínimo.")]
        public void Create_ShouldAddErrorWhenDateIsMinValue()
        {
            var entry = Entry.Create(DateTime.MinValue, string.Empty, 0, 0);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.InvalidDateErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Create - Erro ao criar um lançamento com data maior que a data atual.")]
        public void Create_ShouldAddErrorWhenDateIsNotValid()
        {
            var entry = Entry.Create(DateTime.Now.AddDays(2), string.Empty, 0, 0);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.InvalidDateErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Create - Erro ao criar um lançamento com descrição vazia.")]
        public void Create_ShouldAddErrorWhenDescriptionIsNotValid()
        {
            var entry = Entry.Create(DateTime.Now, string.Empty, 0, 0);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.RequiredDescriptionErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Create - Erro ao criar um lançamento com valor igual a 0.")]
        public void Create_ShouldAddErrorWhenValueIsNotValid()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 0, 0);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.ValueErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Create - Erro ao criar um lançamento com tipo inválido.")]
        public void Create_ShouldAddErrorWhenTypeIsNotValid()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 1, (EntryType)4);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.TypeErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Create - Sucesso.")]
        public void Create_Success()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 1, EntryType.Debit);

            entry.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Update - Erro ao atualizar um lançamento com a data com valor mínimo.")]
        public void Update_ShouldAddErrorWhenDateIsMinValue()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 1, EntryType.Debit);
            entry.Update(DateTime.MinValue, "Teste Unitário", 1, EntryType.Debit);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.InvalidDateErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Update - Erro ao atualizar um lançamento com data maior que a data atual.")]
        public void Update_ShouldAddErrorWhenDateIsNotValid()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 1, EntryType.Debit);
            entry.Update(DateTime.Now.AddDays(3), "Teste Unitário", 1, EntryType.Debit);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.InvalidDateErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Update - Erro ao atualizar um lançamento com descrição vazia.")]
        public void Update_ShouldAddErrorWhenDescriptionIsNotValid()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 1, EntryType.Debit);
            entry.Update(DateTime.Now, string.Empty, 1, EntryType.Debit);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.RequiredDescriptionErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Update - Erro ao atualizar um lançamento com valor igual a 0.")]
        public void Update_ShouldAddErrorWhenValueIsNotValid()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 1, EntryType.Debit);
            entry.Update(DateTime.Now, "Teste Unitário", 0, EntryType.Debit);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.ValueErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Update - Erro ao atualizar um lançamento com tipo inválido.")]
        public void Update_ShouldAddErrorWhenTypeIsNotValid()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 1, EntryType.Debit);
            entry.Update(DateTime.Now, "Teste Unitário", 34, (EntryType)3);

            entry.Errors.Should().HaveCountGreaterThan(0);
            entry.Errors.Any(e => e.Message == DomainErrorMessage.TypeErrorMessage).Should().BeTrue();
        }

        [Fact(DisplayName = "Update - Sucesso.")]
        public void Update_Success()
        {
            var entry = Entry.Create(DateTime.Now, "Teste Unitário", 1, EntryType.Debit);
            entry.Update(DateTime.Now, "Teste Unitário 2", 34, EntryType.Debit);

            entry.Errors.Should().HaveCount(0);
        }
    }
}