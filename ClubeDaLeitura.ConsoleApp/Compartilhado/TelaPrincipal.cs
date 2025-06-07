using ClubeDaLeitura.ConsoleApp.ModuloAmigo;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.Compartilhado;

public class TelaPrincipal
{
    private char opcaoEscolhida;

    private RepositorioAmigo repositorioAmigo;
    private TelaAmigo telaAmigo;

    private RepositorioCaixa repositorioCaixa;
    private TelaCaixa telaCaixa;

    private RepositorioRevista repositorioRevista;
    private TelaRevista telaRevista;

    private RepositorioEmprestimo repositorioEmprestimo;
    private TelaEmprestimo telaEmprestimo;

    public TelaPrincipal()
    {
        repositorioAmigo = new RepositorioAmigo();
        telaAmigo = new TelaAmigo(repositorioAmigo);

        repositorioCaixa = new RepositorioCaixa();
        telaCaixa = new TelaCaixa(repositorioCaixa);

        repositorioRevista = new RepositorioRevista();
        telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);

        repositorioEmprestimo = new RepositorioEmprestimo();
        telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);

        Amigo amigo = new Amigo("Júnior", "Amanda", "49 99999-3333");
        repositorioAmigo.CadastrarRegistro(amigo);

        Caixa caixa = new Caixa("Ação", "Vermelha");
        repositorioCaixa.CadastrarRegistro(caixa);

        Revista revista = new Revista("Superman", 150, 1995, caixa);
        repositorioRevista.CadastrarRegistro(revista);
    }

    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|           Clube da Leitura           |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Controle de Amigos");
        Console.WriteLine("2 - Controle de Caixas");
        Console.WriteLine("3 - Controle de Revistas");
        Console.WriteLine("4 - Controle de Empréstimos");
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        opcaoEscolhida = Console.ReadLine()[0];
    }

    public TelaBase ObterTela()
    {
        if (opcaoEscolhida == '1')
            return telaAmigo;

        else if (opcaoEscolhida == '2')
            return telaCaixa;

        else if (opcaoEscolhida == '3')
            return telaRevista;

        else if (opcaoEscolhida == '4')
            return telaEmprestimo;

        return null;
    }
}
