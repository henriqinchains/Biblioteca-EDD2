using System.Collections.Generic;

namespace Biblioteca
{
    public class Livros
    {
        private List<Livro> acervo = new List<Livro>();

        public IReadOnlyList<Livro> Acervo { get { return acervo; } }

        public void adicionar(Livro livro)
        {
            acervo.Add(livro);
        }

        public Livro pesquisar(Livro livro)
        {
            int i = acervo.IndexOf(livro);
            return i >= 0 ? acervo[i] : null;
        }
    }
}
