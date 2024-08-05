
using Bext.Reps.Business.Commons.Exceptions;
using Bext.Reps.Business.Commons.Interfaces;
using Bext.Reps.Business.Features.Entidades.Commands.Create;
using Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
using Bext.Reps.Domain.Commons.DefaultMessages;
using Bext.Reps.Domain.Commons.Enums;
using Bext.Reps.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Business.FunctionalTests;
[TestFixture]
public class CreateEntidadCommandHandlerTests
{
    private IRepsDbContext _contextMock;
    private IValidator<CreateEntidadCommand> _validatorMock;
    private CreateEntidadCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _contextMock = Substitute.For<IRepsDbContext>();
        _validatorMock = Substitute.For<IValidator<CreateEntidadCommand>>();
        _handler = new CreateEntidadCommandHandler(_contextMock, _validatorMock);
    }

    [Test]
    public async Task Handle_Should_Return_Success_Result_When_Command_Is_Valid()
    {
        // Arrange
        var command = new CreateEntidadCommand(
            "RazonSocial",
            "TI",
            "123456",
            "Direccion",
            "12345",
            "correo@test.com",
            "01",
            "PN",
            null,
            null,
            null,
            new TerceroPrestadorRequest(
                "PN", "NI", "123456", "PrimerNombre", "SegundoNombre", "PrimerApellido", "SegundoApellido",
                "RazonSocial", "01", "01", "Direccion", "12345", "123456", "123456", "SitioWeb", "email@test.com"),
            null,
            null);

        var validationResult = new ValidationResult();
        _validatorMock.ValidateAsync(command, Arg.Any<CancellationToken>()).Returns(Task.FromResult(validationResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _contextMock.Received(1).Entidades.AddAsync(Arg.Any<Entidad>());
        await _contextMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }


    [Test]
    public async Task Handle_Should_Create_Entidad_With_ActaConstitucion()
    {
        // Arrange
        var command = new CreateEntidadCommand(
            "RazonSocial",
            "TI",
            "123456",
            "Direccion",
            "12345",
            "correo@test.com",
            "01",
            "PN",
            "TipoNaturaleza",
            "SubTipoNaturaleza",
            new ActaConstitucionRequest(
                "CaracterTerritorial",
                1,
                "EmpresaSocialEstado",
                "ActoConstitucion",
                "NumeroActo",
                DateTime.UtcNow,
                "EntidadExpide",
                "CiudadExpedicion"),
            new TerceroPrestadorRequest(
                "PN", "NI", "123456", "PrimerNombre", "SegundoNombre", "PrimerApellido", "SegundoApellido",
                "RazonSocial", "01", "01", "Direccion", "12345", "123456", "123456", "SitioWeb", "email@test.com"),
            null,
            null);

        var validationResult = new ValidationResult();
        _validatorMock.ValidateAsync(command, Arg.Any<CancellationToken>()).Returns(Task.FromResult(validationResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _contextMock.Received(1).Entidades.AddAsync(Arg.Is<Entidad>(e => e.ActaConstitucion != null));
        await _contextMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_Should_Create_Entidad_With_Sedes()
    {
        // Arrange
        var command = new CreateEntidadCommand(
            "RazonSocial",
            "TI",
            "123456",
            "Direccion",
            "12345",
            "correo@test.com",
            "01",
            "PN",
            null,
            null,
            null,
            new TerceroPrestadorRequest(
                "PN", "NI", "123456", "PrimerNombre", "SegundoNombre", "PrimerApellido", "SegundoApellido",
                "RazonSocial", "01", "01", "Direccion", "12345", "123456", "123456", "SitioWeb", "email@test.com"),
            null,
            new List<SedeRequest>
            {
                new SedeRequest
                {
                    Departamento = "01",
                    Municipio = "01",
                    Direccion = "Direccion",
                    NombreResponsable = "Responsable",
                    NombreSede = "Sede",
                    EsPrincipal = true,
                    TelefonoFijo = "12345",
                    TelefonoMovil = "123456",
                    Email = "email@sede.com"
                }
            });

        var validationResult = new ValidationResult();
        _validatorMock.ValidateAsync(command, Arg.Any<CancellationToken>()).Returns(Task.FromResult(validationResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _contextMock.Received(1).Entidades.AddAsync(Arg.Is<Entidad>(e => e.Sedes.Count == 1));
        await _contextMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_Should_Create_Entidad_With_Representantes()
    {
        // Arrange
        var command = new CreateEntidadCommand(
            "RazonSocial",
            "TI",
            "123456",
            "Direccion",
            "12345",
            "correo@test.com",
            "01",
            "PN",
            null,
            null,
            null,
            new TerceroPrestadorRequest(
                "PN", "NI", "123456", "PrimerNombre", "SegundoNombre", "PrimerApellido", "SegundoApellido",
                "RazonSocial", "01", "01", "Direccion", "12345", "123456", "123456", "SitioWeb", "email@test.com"),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI", "123456", "PrimerNombre", "SegundoNombre", "PrimerApellido", "SegundoApellido",
                    TipoRepresentacion.Principal, DateTime.UtcNow, "TipoVinculacion", DateTime.UtcNow)
            },
            null);

        var validationResult = new ValidationResult();
        _validatorMock.ValidateAsync(command, Arg.Any<CancellationToken>()).Returns(Task.FromResult(validationResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _contextMock.Received(1).Entidades.AddAsync(Arg.Is<Entidad>(e => e.Periodos.Count == 1));
        await _contextMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
