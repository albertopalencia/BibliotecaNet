using BibliotecaDominio;
using BibliotecaDominio.IRepositorio;
using DominioTest.TestDataBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DominioTest.Unitarias
{
    [TestClass]
    public class BibliotecarioTest
    {
        public BibliotecarioTest()
        {
        }

        public Mock<IRepositorioLibro> repositorioLibro;
        public Mock<IRepositorioPrestamo> repositorioPrestamo;

        [TestInitialize]
        public void setup()
        {
            repositorioLibro = new Mock<IRepositorioLibro>();
            repositorioPrestamo = new Mock<IRepositorioPrestamo>();
        }

        [TestMethod]
        [Owner("AlbertoPalenciaBenedetti")]
        public void EsPrestado()
        {
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Returns(libro);
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object);
            var esprestado = bibliotecario.EsPrestado(libro.Isbn);
            Assert.AreEqual(esprestado, true);
        }

        [TestMethod]
        [Owner("AlbertoPalenciaBenedetti")]
        public void LibroNoPrestadoTest()
        {
            var libroTestDataBuilder = new LibroTestDataBuilder();
            Libro libro = libroTestDataBuilder.Build();
            Bibliotecario bibliotecario = new Bibliotecario(repositorioLibro.Object, repositorioPrestamo.Object);
            repositorioPrestamo.Setup(r => r.ObtenerLibroPrestadoPorIsbn(libro.Isbn)).Equals(null);
            var esprestado = bibliotecario.EsPrestado(libro.Isbn);
            Assert.IsFalse(esprestado);
        }
    }
}