using ClubeDaLeitura.ConsoleApp.Compartilhado;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista;

public class Revista : EntidadeBase
{
    public string Titulo { get; set; }
    public int NumeroEdicao { get; set; }
    public int AnoPublicacao { get; set; }
    public Caixa Caixa { get; set; }

    public string Status { get; set; }

    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa)
    {
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        Caixa = caixa;
        Status = "Disponível";
    }

    public override void AtualizarRegistro(EntidadeBase registroAtualizado)
    {
        Revista revistaAtualizada = (Revista)registroAtualizado;

        Titulo = revistaAtualizada.Titulo;
        NumeroEdicao = revistaAtualizada.NumeroEdicao;
        AnoPublicacao = revistaAtualizada.AnoPublicacao;
        Caixa = revistaAtualizada.Caixa;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (Titulo.Length < 2 || Titulo.Length > 100)
            erros += "O campo \"Título\" deve conter entre 2 e 100 caracteres.";

        if (NumeroEdicao < 1)
            erros += "O campo \"Número da Edição\" deve conter um valor maior que 0.";

        if (AnoPublicacao < DateTime.MinValue.Year || AnoPublicacao > DateTime.Now.Year)
            erros += "O campo \"Ano de Publicação\" deve conter um valor válido no passado ou presente.";

        if (Caixa == null)
            erros += "O campo \"Caixa\" é obrigatório.";

        return erros;
    }
}