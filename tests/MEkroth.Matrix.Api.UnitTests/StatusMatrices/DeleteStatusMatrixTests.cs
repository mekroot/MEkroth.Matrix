using FluentAssertions;
using MEkroth.Matrix.Api.StatusMatrices;
using MEkroth.Matrix.Api.StatusMatrices.Infrastructure;
using NSubstitute;
using Xunit;

namespace MEkroth.Matrix.Api.UnitTests.StatusMatrices
{
    public sealed class DeleteStatusMatrixTests
    {
        [Fact]
        public void CommandValidator_ShouldBeValid_WhenIdIsNotEmptyGuid()
        {
            //Arrange
            DeleteStatusMatrix.CommandValidator validator = new DeleteStatusMatrix.CommandValidator();
            var id = Guid.NewGuid();

            //Act
            var result = validator.Validate(new DeleteStatusMatrix.Command(id));

            //Asserts
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void CommandValidator_ShouldBeInValid_WhenIdIsEmptyGuid()
        {
            //Arrange
            DeleteStatusMatrix.CommandValidator validator = new DeleteStatusMatrix.CommandValidator();
            var id = Guid.Empty;

            //Act
            var result = validator.Validate(new DeleteStatusMatrix.Command(id));

            //Asserts
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should()
                .Contain(error => error.ErrorMessage == "Has not a valid id.");
        }

        [Fact]
        public async Task CommandHandler_ShouldReturnFailure_WhenCommandIsInvalid()
        {
            //Arrange
            DeleteStatusMatrix.CommandValidator validator = new DeleteStatusMatrix.CommandValidator();
            DeleteStatusMatrix.CommandHandler handler = new DeleteStatusMatrix.CommandHandler(Substitute.For<IStatusMatrixRepository>(), validator);
            var id = Guid.Empty;

            //Act
            var result = await handler.Handle(new DeleteStatusMatrix.Command(id), CancellationToken.None);

            //Asserts
            result.IsFaulted.Should().BeTrue();
        }

        [Fact]
        public async Task CommandHandler_ShouldReturnFailure_WhenStatusMatrixDoesNotExists()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new DeleteStatusMatrix.Command(id);
            var repository = Substitute.For<IStatusMatrixRepository>();
            repository.GetStatusMatrixAsync(id, CancellationToken.None)
                .Returns((StatusMatrix)null);

            DeleteStatusMatrix.CommandValidator validator = new DeleteStatusMatrix.CommandValidator();
            DeleteStatusMatrix.CommandHandler handler = new DeleteStatusMatrix.CommandHandler(repository, validator);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Asserts
            result.IsFaulted.Should().BeTrue();
        }

        [Fact]
        public async Task CommandHandler_ShouldReturnSuccessful_WhenCommandIsValid()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new DeleteStatusMatrix.Command(id);
            var existingStatusMatrix = new StatusMatrix() { Id = id };
            var repository = Substitute.For<IStatusMatrixRepository>();
            repository.GetStatusMatrixAsync(id, CancellationToken.None)
                .Returns(existingStatusMatrix);
            repository.DeleteAsync(existingStatusMatrix, CancellationToken.None)
                      .Returns(true);

            DeleteStatusMatrix.CommandValidator validator = new DeleteStatusMatrix.CommandValidator();
            DeleteStatusMatrix.CommandHandler handler = new DeleteStatusMatrix.CommandHandler(repository, validator);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Asserts
            result.IsSuccess.Should().BeTrue();
        }
    }
}
