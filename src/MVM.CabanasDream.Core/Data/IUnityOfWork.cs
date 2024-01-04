namespace MVM.CabanasDream.Core.Data;

public interface IUnityOfWork
{
    Task<bool> Commit();
}