using System.Collections.Generic;

namespace Application.UI.Common
{
    public class ScreenController : IScreenController
    {
        private Dictionary<GameplayScreenType, IGameplayScreen> _screens;
        private GameplayScreenType _lastScreen;

        public ScreenController()
        {
            _screens = new Dictionary<GameplayScreenType, IGameplayScreen>();
            _lastScreen = GameplayScreenType.UNDEFINED;
        }
        
        public void Add(GameplayScreenType type, IGameplayScreen screen)
        {
            _screens.Add(type, screen);
            _screens[type].Hide();
        }

        public void ActiveScreen(GameplayScreenType type, bool asGeneralScreen = false)
        {
            if(!asGeneralScreen) 
                HidePreviewScreen();

            if (_screens.TryGetValue(type, out IGameplayScreen screen))
            {
                screen.Show();
                if(!asGeneralScreen) 
                    _lastScreen = type;
            }
        }

        public void HideLastScreen()
        {
            HidePreviewScreen();
        }

        public void HideScreen(GameplayScreenType type)
        {
            if (_lastScreen != GameplayScreenType.UNDEFINED)
            {
                if(_screens.TryGetValue(type, out IGameplayScreen previewScreen))
                    _screens[type].Hide();
            }
        }

        private void HidePreviewScreen()
        {
            if (_lastScreen != GameplayScreenType.UNDEFINED)
            {
                if(_screens.TryGetValue(_lastScreen, out IGameplayScreen previewScreen))
                    _screens[_lastScreen].Hide();
            }
        }
    }
}