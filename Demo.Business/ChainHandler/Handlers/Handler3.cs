using Demo.Business.ChainHandler.Shared;

namespace Demo.Business.ChainHandler.Handlers
{
    internal class Handler3(IChainHandler next) : ChainHandlerBase(next)
    {
    }
}
