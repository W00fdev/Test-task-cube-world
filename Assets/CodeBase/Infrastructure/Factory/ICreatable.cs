using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Pool
{
    public interface ICreatable
    {
        GameObject GameObject { get; }
    }
}