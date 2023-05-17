using Application.UI.Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.UI.Screens
{
    public class SplashScreen : BaseGameplayScreen
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _loaderImage;
        
        private Sequence _animationTween;

        [Inject]
        public void Construct(IScreenController screenController)
        {
            GetUIComponents();
            screenController.Add(GameplayScreenType.SPLASH, this);
            screenController.Show(GameplayScreenType.SPLASH);
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
                var tween1 = _loaderImage.transform
                    .DOLocalRotate(new Vector3(0, 0, 360), 10, RotateMode.FastBeyond360)
                    .SetRelative(true)
                    .SetLoops(-1, LoopType.Incremental)
                    .SetEase(Ease.InOutSine);

                var tween2 = _backgroundImage.transform
                    .DOLocalMoveX(0, 10)
                    .SetEase(Ease.InOutSine);

                _animationTween = DOTween.Sequence()
                    .Append(tween1)
                    .Join(tween2)
                    .Play();
            }
            else
            {
                if(_animationTween.IsActive())
                    _animationTween.Kill();
            }
        }
    }
}