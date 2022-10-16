using Assets.CodeBase.Infrastructure.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Pool
{
    public class PoolObjects<TPoolable> where TPoolable : class, IPoolable 
    {
        private readonly IFactoryService<TPoolable> _factoryService;
        
        private readonly List<TPoolable> _poolables = new();

        public PoolObjects(IFactoryService<TPoolable> factoryService)
        {
            _factoryService = factoryService;
        }

        public TPoolable CreateObject(bool active)
        {
            TPoolable poolable = _factoryService.CreateObject();
            _poolables.Add(poolable);

            if (active == true)
            {
                poolable.GameObject.SetActive(true);
                //poolable.Activate();
            }
            else
                poolable.Deactivate();

            return poolable;
        }

        public bool HasFreeObject(out TPoolable poolable)
        {
            foreach (TPoolable element in _poolables)
            {
                GameObject goPoolable = element.GameObject;

                if (goPoolable.activeInHierarchy == false)
                {
                    element.GameObject.SetActive(true);

                    // Activate() must be invoked after params initialization
                    //element.Activate();

                    poolable = element;
                    return true;
                }
            }

            poolable = null;
            return false;
        }

        public TPoolable GetFreeObject()
        {
            if (HasFreeObject(out TPoolable poolable))
                return poolable;

            return CreateObject(active: true);
        }
    }
}
