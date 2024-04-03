namespace Demo.Business.ChainHandler.Shared
{
    public interface IChainHandler
    {
        public Task HandleAsync(int id);
    }
}
