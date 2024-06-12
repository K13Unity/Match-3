using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Process
    {
        private readonly ICoroutineRunner _runner;
        private Coroutine _loopCoroutine;

        public bool isRunning { get; private set; }

        protected Process(ICoroutineRunner runner) => _runner = runner;

        public Process Start()
        {
            isRunning = true;
            OnStart();
            _loopCoroutine = _runner.StartCoroutine(Loop());
            return this;
        }

        public void Stop()
        {
            if (_loopCoroutine != null) _runner.StopCoroutine(_loopCoroutine);
            isRunning = false;
            OnStop();
        }

        private IEnumerator Loop()
        {
            while (true)
            {
                OnUpdate();
                yield return null;
            }
        }

        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnStop() { }
    }
}

