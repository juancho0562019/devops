using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using Bext.Reps.Business.Features.Entidades.Commands.Create.Validators;
using Bext.Reps.Business.Commons.Interfaces.Repository;

using Bext.Reps.Domain.Entities;
using Bext.Reps.Business.Features.Entidades.Commands.Create;
using Bext.Reps.Business.Features.Entidades.Commands.Create.Request;
using Bext.Reps.Domain.Commons.DefaultMessages;
using FluentValidation.TestHelper;
using Bext.Reps.Domain.Commons.Enums;
using System.Runtime.ConstrainedExecution;
using System.Linq.Expressions;
using NSubstitute;

namespace Business.FunctionalTests;


public class SubTipoNaturalezaRepositoryMock : IReadOnlyRepository<SubTipoNaturaleza, string>
{
    private readonly Dictionary<string, List<string>> _validRelations;

    public SubTipoNaturalezaRepositoryMock(Dictionary<string, List<string>> validRelations)
    {
        _validRelations = validRelations;
    }

    public Task<bool> ExistByIdAsync(Func<SubTipoNaturaleza, bool> filter, string subTipoNaturaleza, string tipoNaturaleza)
    {
        return Task.FromResult(_validRelations.ContainsKey(tipoNaturaleza) && _validRelations[tipoNaturaleza].Contains(subTipoNaturaleza));
    }

    public Task<bool> ExistByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistByIdAsync(Func<SubTipoNaturaleza, bool> filter, params object[] args)
    {
        if (args.Length == 2 && args[0] is string subTipoNaturaleza && args[1] is string tipoNaturaleza)
        {
            return ExistByIdAsync(filter, subTipoNaturaleza, tipoNaturaleza);
        }
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SubTipoNaturaleza>?> GetAllAsync(Func<SubTipoNaturaleza, bool> filter, params object[] args)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SubTipoNaturaleza>?> GetAllAsync(Func<SubTipoNaturaleza, bool> filter, object[] args, params Expression<Func<SubTipoNaturaleza, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<SubTipoNaturaleza?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<SubTipoNaturaleza?> GetByIdAsync(string id, params Expression<Func<SubTipoNaturaleza, object>>[] includes)
    {
        throw new NotImplementedException();
    }
}
[TestFixture]
public class CreateEntidadCommandValidatorTests
{
    private CreateEntidadCommandValidator _validator;
    private ITerceroRepository _terceroRepositoryMock;
    private IReadOnlyRepository<TipoPersona, string> _tipoPersonaRepositoryMock;
    private IReadOnlyRepository<TipoNaturaleza, string> _tipoNaturalezaRepositoryMock;
    private IReadOnlyRepository<ClasePrestador, string> _tipoPrestadorRepositoryMock;
    private IReadOnlyRepository<DocumentoConstitucion, string> _documentoConstitucionRepositoryMock;
    private IReadOnlyRepository<CaracterTerritorial, string> _caracterTerritorialRespositoryMock;
    private IReadOnlyRepository<NivelAtencion, int> _nivelAtencionRepositoryMock;
    private IReadOnlyRepository<TipoVinculacion, string> _tipoVinculacionRepositoryMock;
    private IReadOnlyRepository<SubTipoNaturaleza, string> _subTipoNaturalezaRepositoryMock;
    private readonly Dictionary<string, List<string>> _naturalezaSubtipos = new()
    {
        { "01", new List<string> { "21", "22" } },
        { "02", new List<string> { "21","22","23" } }
    };

    [SetUp]
    public void SetUp()
    {
        _terceroRepositoryMock = Substitute.For<ITerceroRepository>();
        _tipoPersonaRepositoryMock = Substitute.For<IReadOnlyRepository<TipoPersona, string>>();
        _tipoNaturalezaRepositoryMock = Substitute.For<IReadOnlyRepository<TipoNaturaleza, string>>();
        _tipoPrestadorRepositoryMock = Substitute.For<IReadOnlyRepository<ClasePrestador, string>>();
        _documentoConstitucionRepositoryMock = Substitute.For<IReadOnlyRepository<DocumentoConstitucion, string>>();
        _caracterTerritorialRespositoryMock = Substitute.For<IReadOnlyRepository<CaracterTerritorial, string>>();
        _nivelAtencionRepositoryMock = Substitute.For<IReadOnlyRepository<NivelAtencion, int>>();
        _tipoVinculacionRepositoryMock = Substitute.For<IReadOnlyRepository<TipoVinculacion, string>>();

        _subTipoNaturalezaRepositoryMock = new SubTipoNaturalezaRepositoryMock(_naturalezaSubtipos);

        _tipoPersonaRepositoryMock.ExistByIdAsync(Arg.Any<string>()).Returns(Task.FromResult(true));
        _tipoPrestadorRepositoryMock.ExistByIdAsync(Arg.Any<string>()).Returns(Task.FromResult(true));
        _documentoConstitucionRepositoryMock.ExistByIdAsync(Arg.Any<string>()).Returns(Task.FromResult(true));

        _validator = new CreateEntidadCommandValidator(
            _terceroRepositoryMock,
            _tipoPersonaRepositoryMock,
            _tipoNaturalezaRepositoryMock,
            _tipoPrestadorRepositoryMock,
            _documentoConstitucionRepositoryMock,
            _caracterTerritorialRespositoryMock,
            _nivelAtencionRepositoryMock,
            _tipoVinculacionRepositoryMock,
            _subTipoNaturalezaRepositoryMock);
    }

    [Test]
    public async Task Should_Have_Error_When_RazonSocial_Is_Null()
    {
        var model = new CreateEntidadCommand(null, "NI", "123456", "Dir", "12345", "correo@test.com", "02", "PN", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.RazonSocial).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_NumeroIdentificacion_Is_Empty()
    {
        var model = new CreateEntidadCommand("Razon", "TI", "", "Dir", "12345", "correo@test.com", "02", "PN", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.NumeroIdentificacion).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_TipoIdentificacion_Is_Empty()
    {
        var model = new CreateEntidadCommand("Razon", "", "123456", "Dir", "12345", "correo@test.com", "02", "PN", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.TipoIdentificacion).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_Direccion_Is_Empty()
    {
        var model = new CreateEntidadCommand("Razon", "NI", "123456", "", "12345", "correo@test.com", "02", "PN", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.Direccion).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_TelefonoFijo_Is_Empty()
    {
        var model = new CreateEntidadCommand("Razon", "NI", "123456", "Dir", "", "correo@test.com", "02", "PN", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.TelefonoFijo).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_Correo_Is_Empty()
    {
        var model = new CreateEntidadCommand("Razon", "NI", "123456", "Dir", "12345", "", "02", "PN", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.Correo).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_ClasePrestador_Is_Empty()
    {
        var model = new CreateEntidadCommand("Razon", "NI", "123456", "Dir", "12345", "correo@test.com", "", "PN", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.ClasePrestador).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_TipoPersona_Is_Empty()
    {
        var model = new CreateEntidadCommand("Razon", "NI", "123456", "Dir", "12345", "correo@test.com", "02", "", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.TipoPersona).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_TerceroPrestadorRequest_Is_Null()
    {
        var model = new CreateEntidadCommand("Razon", "NI", "123456", "Dir", "12345", "correo@test.com", "02", "PN", null, null, null, null, null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.TerceroPrestadorRequest).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_PersonaNatural_IS_PN_ClasePrestador_IsNot_01()
    {
        var model = new CreateEntidadCommand("Razon", "NI", "123456", "Dir", "12345", "correo@test.com", "01", "PN", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.ClasePrestador).WithErrorMessage("Cuando seleccione Persona Natural debe enviar clase prestador como Profesional Independiente");
    }

    [Test]
    public async Task Should_Have_Error_When_PersonaNatural_IS_PJ_TipoNaturaleza_Is_NULL()
    {
        var model = new CreateEntidadCommand("Razon", "NI", "123456", "Dir", "12345", "correo@test.com", "01", "PJ", null, null, null,
            new TerceroPrestadorRequest("PN", "NI", "123456", null, null, null, null, null, "73", "73001", "Dir", "1234567", null, null, null, "test@test.com"), null, null);
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(x => x.TipoNaturaleza).WithErrorMessage(DefaultMessage.IsRequired);
    }

    [Test]
    public async Task Should_Have_Error_When_SubTipoNaturaleza_Is_Invalid_For_Publica()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "02",
            "PJ",
            "01", // TipoNaturaleza Pública
            "99", // SubTipoNaturaleza no válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "01",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.SubTipoNaturaleza).WithErrorMessage("El SubTipoNaturaleza no corresponde a un codigo valido");
    }

    [Test]
    public async Task Should_Not_Have_Error_When_SubTipoNaturaleza_Is_Valid_For_Publica()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "02",
            "PJ",
            "01", // TipoNaturaleza Pública
            "21", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "01",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.SubTipoNaturaleza);
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Ips_Privada_Animo_Lucro()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01", //Clase prestador IPS
            "PJ",
            "02",
            "21", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "01",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.SubTipoNaturaleza);
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Ips_Privada_Sin_Animo_Lucro()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01", //Clase prestador IPS
            "PJ",
            "02",
            "22", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "01",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.SubTipoNaturaleza);
    }

    [Test]
    public async Task Should_Have_Error_When_Ips_Privada_SubTipoNaturaleza_Invalid()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01", //Clase prestador IPS
            "PJ",
            "02",
            "125", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "01",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.SubTipoNaturaleza).WithErrorMessage("El SubTipoNaturaleza no corresponde a un codigo valido");
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Ips_Privada_Sin_Animo_Lucro_ActoAdm()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01", //Clase prestador IPS
            "PJ",
            "02",
            "22", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "03",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ActaConstitucion.ActoConstitucion);
    }

    [Test]
    public async Task Should_Have_Error_When_Ips_Privada_Sin_Animo_Lucro_ActoAdm()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01", //Clase prestador IPS
            "PJ",
            "02",
            "22", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "81",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.ActaConstitucion.ActoConstitucion).WithErrorMessage("Acto de constitucion debe ser Resolucion o Acto Administrativo");
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Ips_Privada_Derecho_Publico_Lucro_ActoAdm()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01",
            "PJ",
            "02",
            "23",
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "03", //Acto administrativo
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ActaConstitucion.ActoConstitucion);
    }

    [Test]
    public async Task Should_Have_Error_When_Ips_Privada_Derecho_Publico_Lucro_Invalid_ActoAdm()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01",
            "PJ",
            "02",
            "23",
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "99", //Acto administrativo invalido
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldHaveValidationErrorFor(x => x.ActaConstitucion.ActoConstitucion).WithErrorMessage("Acto de constitucion debe ser Acto Administrativo");
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Ips_Mixta()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01", //Clase prestador IPS
            "PJ",
            "03",
            "22", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "03",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ActaConstitucion.ActoConstitucion);
    }

    [Test]
    public async Task Should_Not_Have_Error_When_PersonaNatural_ProfesionalIndependiente()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "02", //Clase prestador Prof IND
            "PN",
            "01",
            "22", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "01",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PJ",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.SubTipoNaturaleza);
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Entidad_Objeto_Social_Tercero()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "01", //Clase prestador Prof IND
            "PN",
            "01",
            "22", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "03",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PN",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.SubTipoNaturaleza);
        result.ShouldNotHaveValidationErrorFor(x => x.TipoNaturaleza);
    }

    [Test]
    public async Task Should_Not_Have_Error_When_Entidad_Objeto_Social_Tercero_Matricula()
    {
        var model = new CreateEntidadCommand(
            "PEPITO PEREZ",
            "NI",
            "12312312",
            "Calle 23 - 45 # 23",
            "12312",
            "pepito@perez.com",
            "04", //Clase prestador Prof IND
            "PJ",
            "02",
            "21", // SubTipoNaturaleza válido
            new ActaConstitucionRequest(
                "01",
                1,
                "Empresa Estatal",
                "01",
                "123-ABC",
                DateTime.UtcNow,
                "Gobierno Local",
                "Bogotá"
            ),
            new TerceroPrestadorRequest(
                "PN",
                "NI",
                "12312312",
                null,
                null,
                null,
                null,
                "PEPITO PEREZ",
                "73",
                "73001",
                "Calle 23 - 45 # 23",
                "1234567",
                "1234567",
                "1234567",
                "",
                "pepito@perez.com"
            ),
            new List<RepresentanteRequest>
            {
                new RepresentanteRequest(
                    "NI",
                    "123456",
                    "PEPITO",
                    null,
                    "PEREZ",
                    null,
                    TipoRepresentacion.Principal,
                    DateTime.UtcNow,
                    "01",
                    DateTime.UtcNow
                )
            },
            null
        );

        var result = await _validator.TestValidateAsync(model);

        result.ShouldNotHaveValidationErrorFor(x => x.SubTipoNaturaleza);
    }
}
