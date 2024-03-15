using Demo.Business.ChainHandler.Shared;

namespace Demo.Business.ChainHandler.Handlers
{
    internal class Handler4(IChainHandler next) : ChainHandlerBase(next)
    {
    }
}
