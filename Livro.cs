using System.Collections.Generic;

namespace Biblioteca
{
    public class Livro
    {
        private int isbn;
        private string titulo;
        private string autor;
        private string editora;
        private List<Exemplar> exemplares;

        // Usado nas pesquisas: o ISBN identifica o livro.
        public Livro(int isbn) : this(isbn, "", "", "") { }

        public Livro(int isbn, string titulo, string autor, string editora)
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.autor = autor;
            this.editora = editora;
            this.exemplares = new List<Exemplar>();
        }

        public int Isbn { get { return isbn; } set { isbn = value; } }
        public string Titulo { get { return titulo; } set { titulo = value; } }
        public string Autor { get { return autor; } set { autor = value; } }
        public string Editora { get { return editora; } set { editora = value; } }
        public List<Exemplar> Exemplares { get { return exemplares; } }

        public void adicionarExemplar(Exemplar exemplar)
        {
            exemplares.Add(exemplar);
        }

        public int qtdeExemplares()
        {
            return exemplares.Count;
        }

        public int qtdeDisponiveis()
        {
            int n = 0;
            foreach (Exemplar e in exemplares)
                if (e.disponivel()) n++;
            return n;
        }

        // Total de empréstimos já feitos (histórico de todos os exemplares).
        public int qtdeEmprestimos()
        {
            int n = 0;
            foreach (Exemplar e in exemplares)
                n += e.qtdeEmprestimos();
            return n;
        }

        public double percDisponibilidade()
        {
            if (exemplares.Count == 0) return 0;
            return (double)qtdeDisponiveis() / qtdeExemplares() * 100;
        }

        // Extra (não está no diagrama): localiza um exemplar pelo tombo.
        public Exemplar pesquisarExemplar(int tombo)
        {
            foreach (Exemplar e in exemplares)
                if (e.Tombo == tombo) return e;
            return null;
        }

        public override bool Equals(object obj)
        {
            Livro outro = obj as Livro;
            return outro != null && isbn == outro.isbn;
        }

        public override int GetHashCode()
        {
            return isbn.GetHashCode();
        }
    }
}
