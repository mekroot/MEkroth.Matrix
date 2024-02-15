using FluentAssertions;
using FluentAssertions.Execution;
using MEkroth.Matrix.Api.StatusMatrices;
using MEkroth.Matrix.Api.StatusMatrices.Infrastructure;
using NSubstitute;
using Xunit;

namespace MEkroth.Matrix.Api.UnitTests.StatusMatrices
{
    public sealed class SaveStatusMatrixTests
    {
        SaveStatusMatrix.CommandValidator _validator;
        public SaveStatusMatrixTests()
        {
            _validator = new SaveStatusMatrix.CommandValidator();
        }

        [Fact]
        public void CommandValidator_ShouldBeInvalid_WhenNameIsMissing()
        {
            //Act
            var result = _validator.Validate(new SaveStatusMatrix.Command(Guid.Empty, string.Empty, new Status[] { Status.None }));

            //Asserts
            using (var assertScope = new AssertionScope())
            {
                result.Errors.Should().NotBeEmpty();
                result.Errors.Length().Should().Be(1);
                result.Errors.Should().Contain(error => error.ErrorMessage == "Please enter a name for the status matrix.");
            }
        }

        [Fact]
        public void CommandValidator_ShouldBeInvalid_WhenStatusesIsEmpty()
        {
            //Act
            var result = _validator.Validate(new SaveStatusMatrix.Command(Guid.Empty, "Test", []));

            //Asserts
            using (var assertScope = new AssertionScope())
            {
                result.Errors.Should().NotBeEmpty();
                result.Errors.Length().Should().Be(1);
                result.Errors.Should().Contain(error => error.ErrorMessage == "Please specify atleast 1 status.");
            }
        }

        [Fact]
        public void CommandValidator_ShouldBeInvalid_WhenBothNameAndStatusesIsEmpty()
        {
            //Act
            var result = _validator.Validate(new SaveStatusMatrix.Command(Guid.Empty, string.Empty, []));

            //Asserts
            using (var assertScope = new AssertionScope())
            {
                result.Errors.Should().NotBeEmpty();
                result.Errors.Length().Should().Be(2);
            }
        }

        [Fact]
        public async Task CommandHandler_ShouldReturnFailure_WhenCommandMissingName()
        {
            //Arrange
            SaveStatusMatrix.CommandHandler handler = new SaveStatusMatrix.CommandHandler(Substitute.For<IStatusMatrixRepository>(), _validator);

            //Act
            var result = await handler.Handle(new SaveStatusMatrix.Command(Guid.Empty, string.Empty, new[] { Status.None }), Arg.Any<CancellationToken>());

            //Asserts
            result.IsFaulted.Should().BeTrue();
        }

        [Fact]
        public async Task CommandHandler_ShouldReturnFailure_WhenCommandMissingStatuses()
        {
            SaveStatusMatrix.CommandHandler handler = new SaveStatusMatrix.CommandHandler(Substitute.For<IStatusMatrixRepository>(), _validator);

            //Act
            var result = await handler.Handle(new SaveStatusMatrix.Command(Guid.Empty, "Status missing", []), Arg.Any<CancellationToken>());

            //Asserts
            result.IsFaulted.Should().BeTrue();
        }

        [Fact]
        public async Task CommandHandler_ShouldReturnSuccessful_WhenSavingNewStatusMatrix()
        {
            //Arrange
            var command = new SaveStatusMatrix.Command(Guid.Empty, "test", new[] { Status.None });
            var repository = Substitute.For<IStatusMatrixRepository>();

            repository.SaveAsync(Arg.Any<StatusMatrix>(), Arg.Any<bool>(), Arg.Any<CancellationToken>())
                      .Returns(true);

            SaveStatusMatrix.CommandHandler handler = new SaveStatusMatrix.CommandHandler(repository, _validator);

            //Act
            var result = await handler.Handle(command, Arg.Any<CancellationToken>());

            //Asserts
            result.IsSuccess.Should().BeTrue();
        }
    }
}
