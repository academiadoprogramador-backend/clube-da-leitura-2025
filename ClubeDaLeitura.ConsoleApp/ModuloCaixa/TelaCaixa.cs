using ClubeDaLeitura.ConsoleApp.Compartilhado;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa;

public class TelaCaixa : TelaBase
{
    public TelaCaixa(RepositorioCaixa repositorio) : base("Caixa", repositorio)
    {
    }

    public override void VisualizarRegistros(bool exibirCabecalho)
    {
        if (exibirCabecalho == true)
            ExibirCabecalho();

        Console.WriteLine("Visualização de Caixas");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -30} | {3, -30}",
            "Id", "Etiqueta", "Cor", "Dias de Empréstimo"
        );

        EntidadeBase[] caixas = repositorio.SelecionarRegistros();

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

        Console.ReadLine();
    }

    protected override EntidadeBase ObterDados()
    {
        Console.Write("Digite a etiqueta da caixa: ");
        string etiqueta = Console.ReadLine();

        Console.Write("Digite a cor da caixa: ");
        string cor = Console.ReadLine();

        Console.Write("Dias de Empréstimo (opcional): ");
        bool conseguiuConverter = int.TryParse(Console.ReadLine(), out int diasEmprestimo);

        Caixa caixa;

        if (conseguiuConverter)
            caixa = new Caixa(etiqueta, cor, diasEmprestimo);
        else
            caixa = new Caixa(etiqueta, cor);

        return caixa;
    }
}