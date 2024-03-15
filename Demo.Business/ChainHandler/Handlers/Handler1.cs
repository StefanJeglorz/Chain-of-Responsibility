using Demo.Business.ChainHandler.Shared;

namespace Demo.Business.ChainHandler.Handlers
{
    internal class Handler1(IChainHandler next) : ChainHandlerBase(next)
    {
    }
}
