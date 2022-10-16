using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public interface ICoroutinable
    {
        public Coroutine StartCoroutine(IEnumerator routine);
    }
}
