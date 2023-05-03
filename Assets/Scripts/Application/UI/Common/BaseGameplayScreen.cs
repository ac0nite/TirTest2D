using UnityEngine;

namespace Application.UI.Common
{
    public abstract class BaseGameplayScreen : MonoBehaviour, IGameplayScreen
    {
        public abstract void Show();
        public abstract void Hide();
    }
}