namespace Demo.Business.ChainHandler.Shared
{
    public interface IChainHandler
    {
        IChainHandler Next { get; }
        public Task HandleAsync(int id);
    }
}
