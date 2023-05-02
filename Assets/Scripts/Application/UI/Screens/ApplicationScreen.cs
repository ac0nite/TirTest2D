using System;
using Application.UI.Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.UI.Screens
{
    public class ApplicationScreen : MonoBehaviour, IGameplayScreen
    {
        [SerializeField] private Image _loadingImage;
        
        private readonly Canvas _canvas;

        public ApplicationScreen(IScreenController screenController)
        {
            _canvas = GetComponent<Canvas>();
            screenController.Add(GameplayScreenType.APPLICATION, this);
        }
        public void Show()
        {
            _canvas.enabled = true;
            LoadingAnimation(true);
        }

        public void Hide()
        {
            _canvas.enabled = false;
            LoadingAnimation(false);
        }

        private void LoadingAnimation(bool play)
        {
            if (play)
            {
                _loadingImage.rectTransform.DOLocalRotate(new Vector3(0, 0, 360), 5f);
            }
        }
    }
}