using Assets.CodeBase.Infrastructure.Factory;
using Assets.CodeBase.Infrastructure.Pool;
using Assets.CodeBase.Logic;
using Assets.CodeBase.Data;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    // Entry point of game (set in Script Execution Order)
    public class Bootstrapper : MonoBehaviour, ICoroutinable
    {
        [SerializeField] private DataListenerUI _dataListenerUI;
        
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private Transform _cubesParent;

        [Header("Only for debug purposes:")]
        [SerializeField] private SimulationSettings _simulationSettings;

        private IFactoryService<Movable> _movableFactory;
        private PoolObjects<Movable> _movablePool;

        private GameCoreLoop _gameCoreLoop;

        private void Awake()
        {
            _movableFactory = new FactoryCubes<Movable>(_cubePrefab, _cubesParent);
            _movablePool = new PoolObjects<Movable>(_movableFactory);

            _gameCoreLoop = new GameCoreLoop(_simulationSettings, _movablePool, _dataListenerUI, this);
        }

        private void OnDestroy()
        {
            _gameCoreLoop.EndGame();
        }

        public void ExitApplication() => Application.Quit();

        Coroutine ICoroutinable.StartCoroutine(IEnumerator routine) => StartCoroutine(routine);
    }
}
