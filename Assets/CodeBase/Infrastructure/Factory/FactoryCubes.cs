using Assets.CodeBase.Infrastructure.Pool;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Factory
{
    public class FactoryCubes<TCreatable> : IFactoryService<TCreatable>
        where TCreatable : ICreatable
    {
        private readonly GameObject _cubePrefab;
        private readonly Transform _cubesParent;

        public FactoryCubes(GameObject prefab, Transform transformParent)
        {
            _cubePrefab = prefab;
            _cubesParent = transformParent;
        }

        public TCreatable CreateObject() => GameObject.Instantiate(_cubePrefab, _cubesParent).GetComponent<TCreatable>();
    }
}
