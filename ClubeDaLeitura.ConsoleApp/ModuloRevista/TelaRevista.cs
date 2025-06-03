using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class TelaRevista : TelaBase
{
    private RepositorioCaixa repositorioCaixa;

    public TelaRevista(RepositorioRevista repositorio, RepositorioCaixa repositorioCaixa) 
        : base("Revista", repositorio)
    {
        this.repositorioCaixa = repositorioCaixa;
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Revistas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -20} | {3, -20} | {4, -20} | {5, -20}",
            "Id", "Título", "Edição", "Ano de Publicação", "Caixa", "Status"
        );

        EntidadeBase[] revistas = repositorio.SelecionarRegistros();

        for (int i = 0; i < revistas.Length; i++)
        {
            Revista r = (Revista)revistas[i];

            if (r == null)
                continue;

            Console.WriteLine(
             "{0, -10} | {1, -30} | {2, -20} | {3, -20} | {4, -20} | {5, -20}",
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta, r.Status
            );
        }

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        Console.Write("Digite o título da revista: ");
        string titulo = Console.ReadLine();

        Console.Write("Digite o número da edição da revista: ");
        int numeroEdicao = Convert.ToInt32(Console.ReadLine());

        Console.Write("Digite o ano da publicação da revista: ");
        int anoPublicacao = Convert.ToInt32(Console.ReadLine());

        VisualizarCaixas();

        Console.Write("\nDigite o ID da caixa selecionada: ");
        int idCaixa = Convert.ToInt32(Console.ReadLine());

        Caixa caixaSelecionada = (Caixa)repositorioCaixa.SelecionarRegistroPorId(idCaixa);

        Revista revista = new Revista(titulo, numeroEdicao, anoPublicacao, caixaSelecionada);

        return revista;
    }

    public void VisualizarCaixas()
    {
        Console.WriteLine();

        Console.WriteLine("Visualização de Caixas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
            "Id", "Etiqueta", "Cor", "Dias de Empréstimo"
        );

        EntidadeBase[] caixas = repositorioCaixa.SelecionarRegistros();

        for (int i = 0; i < caixas.Length; i++)
        {
            Caixa c = (Caixa)caixas[i];

            if (c == null)
                continue;

            Console.WriteLine(
              "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
                c.Id, c.Etiqueta, c.Cor, c.DiasEmprestimo
            );
        }
    }
}