namespace Demo.Business.ChainHandler.Shared
{
    internal class ChainHandlerBase : IChainHandler
    {
        public IChainHandler Next { get; }

        public ChainHandlerBase(IChainHandler next)
        {
            Next = next;
        }

        public async Task HandleAsync(int id)
        {
            if (Next is not null)
            {
                await Next.HandleAsync(id);
            }
        }
    }
}
