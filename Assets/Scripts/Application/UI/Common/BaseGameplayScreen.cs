using System;
using UnityEngine;

namespace Application.UI.Common
{
    public abstract class BaseGameplayScreen : MonoBehaviour, IGameplayScreen, IDisposable
    {
        protected Canvas _canvas;
        protected virtual void GetUIComponents()
        {
            _canvas = GetComponent<Canvas>();
        }
        public abstract void Show();
        public abstract void Hide();

        public virtual void Dispose()
        { }
    }
}