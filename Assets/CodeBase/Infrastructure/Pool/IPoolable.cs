namespace Assets.CodeBase.Infrastructure.Pool
{
    public interface IPoolable : ICreatable
    {
        void Activate();
        void Deactivate();
    }
}