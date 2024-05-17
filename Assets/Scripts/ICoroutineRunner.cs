using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator routine);
        public void StopCoroutine(Coroutine coroutine);
    }
}