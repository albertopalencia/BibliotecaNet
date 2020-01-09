using BibliotecaDominio;

namespace DominioTest.TestDataBuilders
{
    public class LibroTestDataBuilder
    {
        private const int ANIO = 2012;
        private const string TITULO = "Cien años de soledad";
        private const string ISBN = "1234";

        public int Anio { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }

        public LibroTestDataBuilder()
        {
            Anio = ANIO;
            Titulo = TITULO;
            Isbn = ISBN;
        }

        public LibroTestDataBuilder ConTitulo(string titulo)
        {
            Titulo = titulo;
            return this;
        }

        public LibroTestDataBuilder ConIsbn(string isbn)
        {
            Isbn = isbn;
            return this;
        }

        public LibroTestDataBuilder ConAnio(int anio)
        {
            Anio = anio;
            return this;
        }

        public Libro Build()
        {
            return new Libro(Isbn, Titulo, Anio);
        }
    }
}