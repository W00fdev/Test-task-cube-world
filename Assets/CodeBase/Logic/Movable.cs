using Assets.CodeBase.Infrastructure.Pool;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public class Movable : MonoBehaviour, IPoolable
    {
        public Vector3 Direction = Vector3.forward;
        public float Distance;
        public float Speed;
        
        public GameObject GameObject => gameObject;

        private Coroutine _deactivatorCoroutine;
        private float _distanceReached;

        private void Update()
        {
            transform.Translate(Speed * Time.deltaTime * Direction);
        }

        public void Activate()
        {
            ResetPosition();
            StartDeactivateCoroutine();
        }

        public void Deactivate()
        {
            if (_deactivatorCoroutine != null)
                StopCoroutine(_deactivatorCoroutine);

            gameObject.SetActive(false);
        }

        private void ResetPosition()
        {
            _distanceReached = 0f;
            transform.localPosition = Vector3.zero;
        }

        private void StartDeactivateCoroutine()
        {
            if (_deactivatorCoroutine != null)
                StopCoroutine(_deactivatorCoroutine);

            _deactivatorCoroutine = StartCoroutine(DeactivateDistanceCoroutine());
        }

        IEnumerator DeactivateDistanceCoroutine()
        {
            while (_distanceReached < Distance)
            {
                yield return null;
                _distanceReached += Speed * Time.deltaTime;
            }

            _deactivatorCoroutine = null;
            Deactivate();
        }
    }
}
