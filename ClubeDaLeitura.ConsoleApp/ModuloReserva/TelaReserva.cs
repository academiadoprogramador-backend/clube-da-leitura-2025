using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloReserva;

public class TelaReserva : TelaBase
{
    private RepositorioReserva repositorioReserva;
    private RepositorioAmigo repositorioAmigo;
    private RepositorioRevista repositorioRevista;

    public TelaReserva(
        RepositorioReserva repositorio,
        RepositorioAmigo repositorioAmigo,
        RepositorioRevista repositorioRevista
    ) : base("Reserva", repositorio)
    {
        repositorioReserva = repositorio;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
    }

    public override char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
        Console.WriteLine($"2 - Cancelamento de {nomeEntidade}");
        Console.WriteLine($"3 - Visualizar {nomeEntidade}s ativas");
        Console.WriteLine($"S - Sair");

        Console.WriteLine();

        Console.Write("Digite uma opção válida: ");
        char opcaoEscolhida = Console.ReadLine().ToUpper()[0];

        return opcaoEscolhida;
    }

    public void CadastrarReserva()
    {
        ExibirCabecalho();

        Console.WriteLine($"Cadastro de {nomeEntidade}");

        Console.WriteLine();

        Reserva novaReserva = (Reserva)ObterDados();

        string erros = novaReserva.Validar();

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

        Reserva[] reservasAtivas = repositorioReserva.SelecionarReservasAtivas();

        for (int i = 0; i < reservasAtivas.Length; i++)
        {
            Reserva reservaAtiva = reservasAtivas[i];

            if (novaReserva.Amigo.Id == reservaAtiva.Amigo.Id)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("O amigo selecionado já tem uma reserva ativa!");
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                return;
            }
        }

        novaReserva.Iniciar();

        repositorio.CadastrarRegistro(novaReserva);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
    }

    public void CancelarReserva()
    {
        ExibirCabecalho();

        Console.WriteLine($"Cancelamento de {nomeEntidade}");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID da reserva que deseja cancelar: ");
        int idReserva = Convert.ToInt32(Console.ReadLine());

        Reserva reservaSelecionada = (Reserva)repositorio.SelecionarRegistroPorId(idReserva);

        if (reservaSelecionada == null)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A reserva selecionada não existe!");
            Console.ResetColor();

            Console.Write("\nDigite ENTER para continuar...");
            Console.ReadLine();

            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("\nDeseja confirmar o cancelamento de reserva? Esta ação é irreversível. (s/N): ");
        Console.ResetColor();

        string resposta = Console.ReadLine()!;

        if (resposta.ToUpper() != "S")
            return;

        reservaSelecionada.Concluir();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nCancelamento de {nomeEntidade} concluído com sucesso!");
        Console.ResetColor();

        Console.ReadLine();
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Reservas Ativas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25}",
            "Id", "Amigo", "Revista", "Data da Reserva", "Status"
        );

        Reserva[] reservas = repositorioReserva.SelecionarReservasAtivas();

        for (int i = 0; i < reservas.Length; i++)
        {
            Reserva r = (Reserva)reservas[i];

            if (r == null)
                continue;

            string statusReserva = r.EstaAtiva ? "Ativa" : "Concluída";

            Console.WriteLine(
             "{0, -5} | {1, -15} | {2, -15} | {3, -20} | {4, -25}",
                r.Id, r.Amigo.Nome, r.Revista.Titulo, r.DataAbertura.ToShortDateString(), statusReserva
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

        Console.Write("Digite o ID do amigo que irá reservar a revista: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());

        Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(idAmigo);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nAmigo selecionado com sucesso!");
        Console.ResetColor();

        VisualizarRevistasDisponiveis();

        Console.Write("Digite o ID da revista que irá ser reservada: ");
        int idRevista = Convert.ToInt32(Console.ReadLine());

        Revista revistaSelecionada = (Revista)repositorioRevista.SelecionarRegistroPorId(idRevista);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nRevista selecionada com sucesso!");
        Console.ResetColor();

        Reserva reserva = new Reserva(amigoSelecionado, revistaSelecionada);

        return reserva;
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
