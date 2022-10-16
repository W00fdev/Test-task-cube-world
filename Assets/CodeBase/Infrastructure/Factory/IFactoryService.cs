using Assets.CodeBase.Infrastructure.Pool;

namespace Assets.CodeBase.Infrastructure.Factory
{
    public interface IFactoryService<TCreatable> where TCreatable : ICreatable
    {
        TCreatable CreateObject();
    }
}
