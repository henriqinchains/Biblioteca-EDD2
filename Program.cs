using System;

namespace Biblioteca
{
    class Program
    {
        static Livros livros = new Livros();

        static void Main(string[] args)
        {
            int opcao;
            do
            {
                Console.WriteLine();
                Console.WriteLine("===== BIBLIOTECA =====");
                Console.WriteLine("1. Adicionar livro");
                Console.WriteLine("2. Pesquisar livro (sintético)");
                Console.WriteLine("3. Pesquisar livro (analítico)");
                Console.WriteLine("4. Adicionar exemplar");
                Console.WriteLine("5. Registrar empréstimo");
                Console.WriteLine("6. Registrar devolução");
                Console.WriteLine("0. Sair");
                opcao = LerInt("Opção: ");

                switch (opcao)
                {
                    case 0: Console.WriteLine("Encerrando..."); break;
                    case 1: AdicionarLivro(); break;
                    case 2: PesquisarSintetico(); break;
                    case 3: PesquisarAnalitico(); break;
                    case 4: AdicionarExemplar(); break;
                    case 5: RegistrarEmprestimo(); break;
                    case 6: RegistrarDevolucao(); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            } while (opcao != 0);
        }

        static int LerInt(string msg)
        {
            int v;
            Console.Write(msg);
            while (!int.TryParse(Console.ReadLine(), out v))
                Console.Write("Valor inválido. " + msg);
            return v;
        }

        static string LerTexto(string msg)
        {
            Console.Write(msg);
            return Console.ReadLine().Trim();
        }

        static Livro BuscarLivro()
        {
            int isbn = LerInt("ISBN: ");
            Livro l = livros.pesquisar(new Livro(isbn));
            if (l == null) Console.WriteLine("Livro não encontrado.");
            return l;
        }

        static bool TomboExiste(int tombo)
        {
            foreach (Livro l in livros.Acervo)
                if (l.pesquisarExemplar(tombo) != null) return true;
            return false;
        }

        static void AdicionarLivro()
        {
            int isbn = LerInt("ISBN: ");
            if (livros.pesquisar(new Livro(isbn)) != null)
            {
                Console.WriteLine("Já existe um livro com esse ISBN.");
                return;
            }
            string titulo = LerTexto("Título: ");
            string autor = LerTexto("Autor: ");
            string editora = LerTexto("Editora: ");
            livros.adicionar(new Livro(isbn, titulo, autor, editora));
            Console.WriteLine("Livro adicionado.");
        }

        static void MostrarSintetico(Livro l)
        {
            Console.WriteLine("ISBN: " + l.Isbn);
            Console.WriteLine("Título: " + l.Titulo);
            Console.WriteLine("Autor: " + l.Autor);
            Console.WriteLine("Editora: " + l.Editora);
            Console.WriteLine("Exemplares: " + l.qtdeExemplares());
            Console.WriteLine("Disponíveis: " + l.qtdeDisponiveis());
            Console.WriteLine("Empréstimos: " + l.qtdeEmprestimos());
            Console.WriteLine("Disponibilidade: " + l.percDisponibilidade().ToString("F2") + "%");
        }

        static void PesquisarSintetico()
        {
            Livro l = BuscarLivro();
            if (l != null) MostrarSintetico(l);
        }

        static void PesquisarAnalitico()
        {
            Livro l = BuscarLivro();
            if (l == null) return;

            MostrarSintetico(l);
            Console.WriteLine("--- Exemplares ---");
            if (l.Exemplares.Count == 0)
                Console.WriteLine("(nenhum exemplar cadastrado)");

            foreach (Exemplar e in l.Exemplares)
            {
                Console.WriteLine("Tombo " + e.Tombo + " - " + (e.disponivel() ? "disponível" : "emprestado")
                    + " (" + e.qtdeEmprestimos() + " empréstimo(s))");
                foreach (Emprestimo emp in e.Emprestimos)
                    Console.WriteLine("    " + emp);
            }
        }

        static void AdicionarExemplar()
        {
            Livro l = BuscarLivro();
            if (l == null) return;

            int tombo = LerInt("Tombo do exemplar: ");
            if (TomboExiste(tombo))
            {
                Console.WriteLine("Já existe um exemplar com esse tombo.");
                return;
            }
            l.adicionarExemplar(new Exemplar(tombo));
            Console.WriteLine("Exemplar adicionado.");
        }

        static Exemplar BuscarExemplar()
        {
            Livro l = BuscarLivro();
            if (l == null) return null;

            int tombo = LerInt("Tombo do exemplar: ");
            Exemplar e = l.pesquisarExemplar(tombo);
            if (e == null) Console.WriteLine("Exemplar não encontrado neste livro.");
            return e;
        }

        static void RegistrarEmprestimo()
        {
            Exemplar e = BuscarExemplar();
            if (e == null) return;

            Console.WriteLine(e.emprestar()
                ? "Empréstimo registrado."
                : "Exemplar indisponível (já está emprestado).");
        }

        static void RegistrarDevolucao()
        {
            Exemplar e = BuscarExemplar();
            if (e == null) return;

            Console.WriteLine(e.devolver()
                ? "Devolução registrada."
                : "Este exemplar não possui empréstimo em aberto.");
        }
    }
}
