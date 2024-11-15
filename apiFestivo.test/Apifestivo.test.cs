using Xunit;
using Moq;
using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Core.Interfaces.Servicios;
using apiFestivos.Dominio.Entidades;
using apiFestivos.Dominio.DTOs;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class Apifestivo
{
    private readonly Mock<IFestivoRepositorio> repositorioMock;
    private readonly IFestivoServicio servicio;

    public Apifestivo()
    {
        repositorioMock = new Mock<IFestivoRepositorio>();
        servicio = new FestivoServicio(repositorioMock.Object);
    }

    // 1.método EsFestivo()
    [Fact]
    public async Task ValidarSiLaFechaCoincideConFestivo()
    {
        //Arrange
        var fechaPrueba = new DateTime(2024, 12, 25);
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 1,
                Nombre = "Navidad",
                Dia = 25,
                Mes = 12,
                IdTipo = 1,
                DiasPascua = 0
            }
        };

        repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        //Act
        var resultado = await servicio.EsFestivo(fechaPrueba);

        //Assert
        Assert.True(resultado);
    }

    [Fact]
    public async Task FechaNoEstaEnListaDeFestivos()
    {
        // Arrange
        var fechaPrueba = new DateTime(2024, 12, 26);
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 1,
                Nombre = "Navidad",
                Dia = 25,
                Mes = 12,
                IdTipo = 1,
                DiasPascua = 0
            }
        };

        repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // Act
        var resultado = await servicio.EsFestivo(fechaPrueba);

        // Assert
        Assert.False(resultado);
    }

    // 2.1 "Verificar que, al dar un festivo con tipo 1, se retorne la fecha esperada"

    [Fact]
    public async Task VerificarFestivoDeTipiUnoRetorneFechaEsperada()
    {
        // Arrange
        var año = 2024;
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 1,
                Nombre = "Navidad",
                Dia = 25,
                Mes = 12,
                IdTipo = 1,
				DiasPascua = 0
            }
        };

        repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // Act
        var resultado = await servicio.ObtenerAño(año);

        // Assert
        var festivo = resultado.First();
        Assert.Equal(new DateTime(2024, 12, 25), festivo.Fecha);
        Assert.Equal("Navidad", festivo.Nombre);
    }

    // 2.2 "Probar que un festivo movible (tipo 2) caiga en el lunes siguiente a la fecha inicial"
    [Fact]
    public async Task ProbarFestivoMovibeTipoDosCaigaLunesEnSiguienteFechaInicial()
    {
        // Arrange
        var año = 2024;
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 2,
                Nombre = "Todos los Santos",
                Dia = 1,
                Mes = 11,
                IdTipo = 2,
				DiasPascua = 0
            }
        };

        repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // Act
        var resultado = await servicio.ObtenerAño(año);

        // Assert
        var festivo = resultado.First();
        Assert.Equal(new DateTime(2024, 11, 4), festivo.Fecha); // En este caso el 1 de noviembre cae viernes, entonces se traslada al lunes 4
        Assert.Equal("Todos los Santos", festivo.Nombre);
    }

    //  2.3 "Verificar un festivo que se desplaza a lunes basado en una fecha relativa a Semana Santa (tipo 4)"
    [Fact]
    public async Task FestivoQueSeDesplazaAlunesBasadoEnFechaSemanaSantaTipo4()
    {
        // Arrange
        var año = 2024;
        var festivosDB = new List<Festivo>
        {
            new Festivo
            {
                Id = 4,
                Nombre = "Ascensión del Señor",
                Dia = 0,
                Mes = 0,
                IdTipo = 4,
				DiasPascua = 43
            }
        };

        repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(festivosDB);

        // Act
        var resultado = await servicio.ObtenerAño(año);

        // Assert
        var festivo = resultado.First();
        // En 2024:
        // 1. Pascua: 31 de marzo
        // 2. +43 días = 13 de mayo (que cae en lunes)
        // 3. Como es tipo 4, se aplica el traslado al lunes anterior
        Assert.Equal(new DateTime(2024, 5, 6), festivo.Fecha);
        Assert.Equal("Ascensión del Señor", festivo.Nombre);
    }
}