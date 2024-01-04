namespace MVM.CabanasDream.Core.Application;

public class CommandResult<TViewModel>
{
    public bool Sucess { get; private set; }
    public TViewModel ViewModel { get; private set; }
    public List<string> MensagensErro { get; private set; }

    private CommandResult()
    {
        MensagensErro = new List<string>();
    }

    public static CommandResult<TViewModel> Sucesso(TViewModel viewModel)
    {
        return new CommandResult<TViewModel>
        {
            Sucess = true,
            ViewModel = viewModel
        };
    }

    public static CommandResult<TViewModel> Falha(params string[]? mensagensErro)
    {
        var result = new CommandResult<TViewModel>
        {
            Sucess = false
        };

        if (mensagensErro != null)
        {
            result.MensagensErro.AddRange(mensagensErro);
        }

        return result;
    }
}