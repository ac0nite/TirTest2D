using UnityEngine;

namespace Application.UI.Common
{
    public abstract class BaseGameplayScreen : MonoBehaviour, IGameplayScreen
    {
        protected Canvas _canvas;
        protected virtual void GetUIComponents()
        {
            _canvas = GetComponent<Canvas>();
        }
        public abstract void Show();
        public abstract void Hide();
    }
}