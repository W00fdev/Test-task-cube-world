using Assets.CodeBase.Infrastructure.Pool;
using Assets.CodeBase.Logic;
using Assets.CodeBase.Data;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class GameCoreLoop
    {
        private readonly SimulationSettings _simulationSettings;

        private readonly PoolObjects<Movable> _movablePool;
        private readonly ICoroutinable _coroutinable;

        private readonly DataListenerUI _dataListenerUI;

        private bool _isGameLooped;

        public GameCoreLoop(SimulationSettings simulationSettings, PoolObjects<Movable> movablePool,
            DataListenerUI dataListenerUI, ICoroutinable coroutinable)
        {
            _simulationSettings = simulationSettings;
            _movablePool = movablePool;
            _dataListenerUI = dataListenerUI;
            _coroutinable = coroutinable;

            InitializeGame();
        }

        public void EndGame()
        {
            _dataListenerUI.UnsubscribeListeners();
            _isGameLooped = false;
        }

        private void InitializeGame()
        {
            _isGameLooped = true;
            _dataListenerUI.InitializeListener(_simulationSettings);
            _coroutinable.StartCoroutine(SpawnerCoroutine());
        }

        private void ExtractMovable()
        {
            Movable spawnedObject = _movablePool.GetFreeObject();
            spawnedObject.Distance = _simulationSettings.Distance;
            spawnedObject.Speed = _simulationSettings.Speed;
            spawnedObject.Activate();
        }

        IEnumerator SpawnerCoroutine()
        {
            while (_isGameLooped)
            {
                yield return new WaitForSeconds(_simulationSettings.Time);
                ExtractMovable();
            }
        }

    }
}
