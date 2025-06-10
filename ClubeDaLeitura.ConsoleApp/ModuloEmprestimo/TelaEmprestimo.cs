using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

public class TelaEmprestimo : TelaBase
{
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioAmigo repositorioAmigo;
    private RepositorioRevista repositorioRevista;

    public TelaEmprestimo(
        RepositorioEmprestimo repositorio,
        RepositorioAmigo repositorioAmigo,
        RepositorioRevista repositorioRevista
    ) : base("Empréstimo", repositorio)
    {
        repositorioEmprestimo = repositorio;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }

    public override char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
        Console.WriteLine($"2 - Devolução de {nomeEntidade}");
        Console.WriteLine($"3 - Visualizar {nomeEntidade}s");
        Console.WriteLine($"4 - Pagar Multas de {nomeEntidade}s");
        Console.WriteLine($"S - Sair");

        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        char opcaoEscolhida = Console.ReadLine().ToUpper()[0];

        return opcaoEscolhida;
    }

    public void CadastrarEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine($"Cadastro de {nomeEntidade}");

        Console.WriteLine();

        Emprestimo novoRegistro = (Emprestimo)ObterDados();

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erros);
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            CadastrarRegistro();

            return;
        }

        Emprestimo[] emprestimosAtivos = repositorioEmprestimo.SelecionarEmprestimosAtivos();

        for (int i = 0; i < emprestimosAtivos.Length; i++)
        {
            Emprestimo emprestimoAtivo = emprestimosAtivos[i];

            if (novoRegistro.Amigo.Id == emprestimoAtivo.Amigo.Id)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("O amigo selecionado já tem um empréstimo ativo!");
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                return;
            }
        }

        novoRegistro.Revista.Status = "Emprestada";

        repositorio.CadastrarRegistro(novoRegistro);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
    }

    public void DevolverEmprestimo()
    {
        ExibirCabecalho();

        Console.WriteLine($"Devolução de {nomeEntidade}");

        Console.WriteLine();

        VisualizarEmprestimosAtivos();

        Console.Write("Digite o ID do emprestimo que deseja concluir: ");
        int idEmprestimo = Convert.ToInt32(Console.ReadLine());

        Emprestimo emprestimoSelecionado = (Emprestimo)repositorio.SelecionarRegistroPorId(idEmprestimo);

        if (emprestimoSelecionado == null)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("O empréstimo selecionado não existe!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("\nDeseja confirmar a conclusão do empréstimo? Esta ação é irreversível. (s/N): ");
        Console.ResetColor();

        string resposta = Console.ReadLine()!;

        if (resposta.ToUpper() != "S")
            return;

        emprestimoSelecionado.Status = "Concluído";
        emprestimoSelecionado.Revista.Status = "Disponível";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{nomeEntidade} concluído com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
    }

    public void PagarMultas()
    {
        ExibirCabecalho();

        Console.WriteLine($"Pagamento de Multas de {nomeEntidade}");

        Console.WriteLine();

        VisualizarEmprestimosComMulta();

        Console.Write("Digite o ID do emprestimo com multas pendentes: ");
        int idEmprestimo = Convert.ToInt32(Console.ReadLine());

        Emprestimo emprestimoSelecionado = (Emprestimo)repositorio.SelecionarRegistroPorId(idEmprestimo);

        if (emprestimoSelecionado == null)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("O empréstimo selecionado não existe!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("\nDeseja confirmar o pagamento da multa? Esta ação é irreversível. (s/N): ");
        Console.ResetColor();

        string resposta = Console.ReadLine()!;

        if (resposta.ToUpper() != "S")
            return;

        emprestimoSelecionado.MultaPaga = true;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nPagamento de Multa de {nomeEntidade} concluído com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Empréstimos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
            "Id", "Amigo", "Revista", "Data do Empréstimo", "Data Prev. Devolução", "Status"
        );

        EntidadeBase[] emprestimos = repositorio.SelecionarRegistros();

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo e = (Emprestimo)emprestimos[i];

            if (e == null)
                continue;

            if (e.Status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(
             "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
                e.Id, e.Amigo.Nome, e.Revista.Titulo, e.DataEmprestimo.ToShortDateString(), e.DataDevolucao.ToShortDateString(), e.Status
            );

            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"\nDigite ENTER para continuar...");
        Console.ResetColor();

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        VisualizarAmigos();

        Console.Write("Digite o ID do amigo que irá receber a revista: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());

        Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nAmigo selecionado com sucesso!");
        Console.ResetColor();

        VisualizarRevistasDisponiveis();

        Console.Write("Digite o ID da revista que irá ser emprestada: ");
        int idRevista = Convert.ToInt32(Console.ReadLine());

        Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nRevista selecionada com sucesso!");
        Console.ResetColor();

        Emprestimo emprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);

        return emprestimo;
    }

    private void VisualizarEmprestimosAtivos()
    {
        Console.WriteLine("Visualização de Empréstimos Ativos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
            "Id", "Amigo", "Revista", "Data do Empréstimo", "Data Prev. Devolução", "Status"
        );

        EntidadeBase[] emprestimosAtivos = repositorioEmprestimo.SelecionarEmprestimosAtivos();

        for (int i = 0; i < emprestimosAtivos.Length; i++)
        {
            Emprestimo e = (Emprestimo)emprestimosAtivos[i];

            if (e == null)
                continue;

            if (e.Status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(
             "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
                e.Id, e.Amigo.Nome, e.Revista.Titulo, e.DataEmprestimo.ToShortDateString(), e.DataDevolucao.ToShortDateString(), e.Status
            );

            Console.ResetColor();
        }

        Console.WriteLine();
    }

    private void VisualizarEmprestimosComMulta()
    {
        Console.WriteLine("Visualização de Empréstimos Ativos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -20}",
            "Id", "Amigo", "Revista", "Data do Empréstimo", "Data Prev. Devolução", "Valor da multa"
        );

        EntidadeBase[] emprestimosComMulta = repositorioEmprestimo.SelecionarEmprestimosComMulta();

        for (int i = 0; i < emprestimosComMulta.Length; i++)
        {
            Emprestimo e = (Emprestimo)emprestimosComMulta[i];

            if (e == null)
                continue;

            if (e.Status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(
             "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25} | {5, -15}",
                e.Id, e.Amigo.Nome, e.Revista.Titulo, e.DataEmprestimo.ToShortDateString(), e.DataDevolucao.ToShortDateString(), e.Multa.Valor.ToString("C2")
            );

            Console.ResetColor();
        }

        Console.WriteLine();
    }

    private void VisualizarAmigos()
    {
        Console.WriteLine("Visualização de Amigos");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -30} | {2, -30} | {3, -20}",
            "Id", "Nome", "Responsável", "Telefone"
        );

        EntidadeBase[] amigos = repositorioAmigo.SelecionarRegistros();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo a = (Amigo)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine(
              "{0, -5} | {1, -30} | {2, -30} | {3, -20}",
                a.Id, a.Nome, a.NomeResponsavel, a.Telefone
            );
        }

        Console.WriteLine();
    }

    private void VisualizarRevistasDisponiveis()
    {
        Console.WriteLine();

        Console.WriteLine("Visualização de Revistas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -20} | {2, -10} | {3, -20} | {4, -15} | {5, -15}",
            "Id", "Título", "Edição", "Ano de Publicação", "Caixa", "Status"
        );

        EntidadeBase[] revistasDisponiveis = repositorioRevista.SelecionarRevistasDisponiveis();

        for (int i = 0; i < revistasDisponiveis.Length; i++)
        {
            Revista r = (Revista)revistasDisponiveis[i];

            if (r == null)
                continue;

            Console.WriteLine(
            "{0, -5} | {1, -20} | {2, -10} | {3, -20} | {4, -15} | {5, -15}",
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta, r.Status
            );
        }

        Console.WriteLine();
    }
}
