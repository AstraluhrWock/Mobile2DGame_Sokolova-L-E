using UnityEngine;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Game
{
    internal class BaseContext : IDisposable
    {
        private List<GameObject> _gameObjects;
        private List<IDisposable> _disposable;
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            DisposeDisposable();
            DisposeGameObjects();

            OnDispose();
        }

        private void DisposeDisposable()
        {
            if (_disposable == null)
                return;

            foreach (IDisposable disposable in _disposable)
                disposable.Dispose();

            _disposable.Clear();
        }

        private void DisposeGameObjects()
        {
            if (_gameObjects == null)
                return;

            foreach (GameObject gameObject in _gameObjects)
                Object.Destroy(gameObject);

            _gameObjects.Clear();
        }

        protected virtual void OnDispose() { }

        protected void AddController(BaseController baseController) =>
        AddDisposable(baseController);

        protected void AddRepositories(IRepository repository) =>
        AddDisposable(repository);

        protected void AddGameObject(GameObject gameObject)
        {
            _gameObjects ??= new List<GameObject>();
            _gameObjects.Add(gameObject);
        }

        private void AddDisposable(IDisposable disposable)
        {
            _disposable ??= new List<IDisposable>();
            _disposable.Add(disposable);
        }
    }

}

