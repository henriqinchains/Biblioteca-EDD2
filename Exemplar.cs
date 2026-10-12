using System.Collections.Generic;

namespace Biblioteca
{
    public class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos;

        public Exemplar(int tombo)
        {
            this.tombo = tombo;
            this.emprestimos = new List<Emprestimo>();
        }

        public int Tombo { get { return tombo; } set { tombo = value; } }
        public List<Emprestimo> Emprestimos { get { return emprestimos; } }

        // Só empresta se estiver disponível.
        public bool emprestar()
        {
            if (!disponivel()) return false;
            emprestimos.Add(new Emprestimo());
            return true;
        }

        // Só devolve se houver empréstimo em aberto.
        public bool devolver()
        {
            if (disponivel()) return false;
            emprestimos[emprestimos.Count - 1].DtDevolucao = System.DateTime.Now;
            return true;
        }

        // Disponível: nunca emprestado ou último empréstimo já devolvido.
        public bool disponivel()
        {
            return emprestimos.Count == 0 || emprestimos[emprestimos.Count - 1].Devolvido;
        }

        public int qtdeEmprestimos()
        {
            return emprestimos.Count;
        }
    }
}
