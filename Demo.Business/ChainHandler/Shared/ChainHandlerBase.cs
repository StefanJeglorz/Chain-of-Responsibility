namespace Demo.Business.ChainHandler.Shared
{
    internal class ChainHandlerBase(IChainHandler next) : IChainHandler
    {
        public virtual async Task HandleAsync(int id)
        {
            if (next is not null)
            {
                await next.HandleAsync(id);
            }
        }
    }
}
