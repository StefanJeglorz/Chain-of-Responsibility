using Demo.Business.ChainHandler.Shared;

namespace Demo.Business.ChainHandler.Handlers
{
    internal class Handler2(IChainHandler next) : ChainHandlerBase(next)
    {
    }
}
