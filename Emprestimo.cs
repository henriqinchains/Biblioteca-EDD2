using System;

namespace Biblioteca
{
    public class Emprestimo
    {
        private DateTime dtEmprestimo;
        private DateTime dtDevolucao;   // DateTime.MinValue = ainda não devolvido

        public Emprestimo()
        {
            dtEmprestimo = DateTime.Now;
            dtDevolucao = DateTime.MinValue;
        }

        public DateTime DtEmprestimo { get { return dtEmprestimo; } set { dtEmprestimo = value; } }
        public DateTime DtDevolucao { get { return dtDevolucao; } set { dtDevolucao = value; } }

        public bool Devolvido { get { return dtDevolucao != DateTime.MinValue; } }

        public override string ToString()
        {
            string dev = Devolvido ? dtDevolucao.ToString("dd/MM/yyyy HH:mm") : "(em aberto)";
            return string.Format("Empréstimo: {0:dd/MM/yyyy HH:mm} | Devolução: {1}", dtEmprestimo, dev);
        }
    }
}
