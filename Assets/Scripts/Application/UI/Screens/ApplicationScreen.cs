using Application.UI.Common;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.UI.Screens
{
    public class ApplicationScreen : BaseGameplayScreen
    {
        [SerializeField] private Image _loadingImage;

        private Canvas _canvas;
        private TweenerCore<Quaternion,Vector3,QuaternionOptions> _rotateTween;

        [Inject]
        public void Construct(IScreenController screenController)
        {
            _canvas = GetComponent<Canvas>();
            screenController.Add(GameplayScreenType.APPLICATION, this);
            screenController.Show(GameplayScreenType.APPLICATION);
        }
        
        public override void Show()
        {
            _canvas.enabled = true;
            LoadingAnimation(true);
        }

        public override void Hide()
        {
            _canvas.enabled = false;
            LoadingAnimation(false);
        }

        private void LoadingAnimation(bool play)
        {
            if(play)
            {
                _rotateTween = _loadingImage.transform
                    .DOLocalRotate(new Vector3(0, 0, 360), 10, RotateMode.FastBeyond360)
                    .SetRelative(true)
                    .SetLoops(-1, LoopType.Incremental)
                    .SetEase(Ease.InOutSine);
            }
            else
            {
                if(_rotateTween.IsActive())
                    _rotateTween.Kill();
            }
        }
    }
}