using System;
using BibliotecaDominio;
using BibliotecaRepositorio.Contexto;
using BibliotecaRepositorio.Repositorio;
using DominioTest.TestDataBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DominioTest.Integracion
{
    [TestClass]
    public class BibliotecarioTest
    {
        public const string CRONICAUNAMUERTEANUNCIADA = "Cronica de una muerte anunciada";
        public const string ISBN = "AO157F751OA";

        public const string USER = "";
        private BibliotecaContexto _contexto;
        private RepositorioLibroEF _repositorioLibro;
        private RepositorioPrestamoEF _repositorioPrestamo;

        [TestInitialize]
        public void setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContexto>();
            _contexto = new BibliotecaContexto(optionsBuilder.Options);
            _repositorioLibro = new RepositorioLibroEF(_contexto);
            _repositorioPrestamo = new RepositorioPrestamoEF(_contexto, _repositorioLibro);
        }

        [TestMethod]
        [Owner("AlbertoPalencia")]
        public void PrestarLibroTest()
        {
            var libro = new LibroTestDataBuilder().ConTitulo(CRONICAUNAMUERTEANUNCIADA).Build();
            _repositorioLibro.Agregar(libro);
            var bibliotecario = new Bibliotecario(_repositorioLibro, _repositorioPrestamo);
            bibliotecario.Prestar(libro.Isbn, "AlbertoPalencia");
            Assert.AreEqual(bibliotecario.EsPrestado(libro.Isbn), true);
            Assert.IsNotNull(_repositorioPrestamo.ObtenerLibroPrestadoPorIsbn(libro.Isbn));
        }

        [TestMethod]
        [Owner("AlbertoPalencia")]
        public void PrestarLibroNoDisponibleTest()
        {
            var libro = new LibroTestDataBuilder().ConTitulo(CRONICAUNAMUERTEANUNCIADA).Build();
            _repositorioLibro.Agregar(libro);
            var bibliotecario = new Bibliotecario(_repositorioLibro, _repositorioPrestamo);
            bibliotecario.Prestar(libro.Isbn, "AlbertoPalencia");
            try
            {
                bibliotecario.Prestar(libro.Isbn, "AlbertoPalencia");
                Assert.Fail();
            }
            catch (Exception err)
            {
                Assert.AreEqual("El libro no se encuentra disponible", err.Message);
            }
        }

        [TestMethod]
        [Owner("AlbertoPalencia")]
        public void PrestarLibroConISBNPalindromo()
        {
            var libro = new LibroTestDataBuilder().ConIsbn(ISBN).Build();
            _repositorioLibro.Agregar(libro);
            var bibliotecario = new Bibliotecario(_repositorioLibro, _repositorioPrestamo);
            try
            {
                bibliotecario.Prestar(libro.Isbn, "AlbertoPalencia");
                Assert.Fail();
            }
            catch (Exception err)
            {
                Assert.AreEqual("Los libros palíndromos solo se pueden utilizar en la biblioteca", err.Message);
            }
        }

        [TestMethod]
        [Owner("AlbertoPalencia")]
        public void PrestarLibroConISBNSinNombreUsuario()
        {
            var libro = new LibroTestDataBuilder().ConTitulo(CRONICAUNAMUERTEANUNCIADA).Build();
            _repositorioLibro.Agregar(libro);
            var bibliotecario = new Bibliotecario(_repositorioLibro, _repositorioPrestamo);
            try
            {
                bibliotecario.Prestar(libro.Isbn, USER);
                Assert.Fail();
            }
            catch (Exception err)
            {
                Assert.AreEqual("El nombre del usuario no puede ser null", err.Message);
            }
        }
    }
}