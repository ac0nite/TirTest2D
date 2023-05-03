using System.Collections.Generic;

namespace Application.UI.Common
{
    public class ScreenController : IScreenController
    {
        private Dictionary<GameplayScreenType, IGameplayScreen> _screens;
        private GameplayScreenType _currentScreen;
        private GameplayScreenType _previousScreen;
        private List<GameplayScreenType> _generalScreens;

        private const GameplayScreenType UNDEFINED = GameplayScreenType.UNDEFINED;

        public ScreenController()
        {
            _screens = new Dictionary<GameplayScreenType, IGameplayScreen>();
            _generalScreens = new List<GameplayScreenType>();
            _currentScreen = UNDEFINED;
            _previousScreen = UNDEFINED;
        }
        
        public void Add(GameplayScreenType type, IGameplayScreen screen)
        {
            _screens.Add(type, screen);
            _screens[type].Hide();
        }

        public void Show(GameplayScreenType type)
        {
            if (IsCurrentScreen && TryToHideScreen(_currentScreen))
                _previousScreen = _currentScreen;

            if(TryToShowScreen(type))
                _currentScreen = type;    
        }

        public void Hide(GameplayScreenType type, bool showPrevious = false)
        {
            if (IsCurrentScreen && TryToHideScreen(_currentScreen))
                _currentScreen = UNDEFINED;

            if (IsPreviousScreen)
                Show(_previousScreen);
        }

        public void ShowGeneral(GameplayScreenType type)
        {
            if(_generalScreens.Contains(type))
                return;
            
            if (TryToShowScreen(type))
                _generalScreens.Add(type);
        }

        public void HideGeneral(GameplayScreenType type)
        {
            if (_generalScreens.Contains(type))
            {
                TryToHideScreen(type);
                _generalScreens.Remove(type);
            }
        }

        public void ShowLastAll()
        {
            if (IsCurrentScreen)
                TryToShowScreen(_currentScreen);

            for (int i = 0; i < _generalScreens.Count; i++)
            {
                TryToShowScreen(_generalScreens[i]);
            }
        }

        public void HideAll(bool keepActive = false)
        {
            if (!keepActive)
            {
                Hide(_currentScreen);
                for (int i = 0; i < _generalScreens.Count; i++)
                {
                    HideGeneral(_generalScreens[i]);
                }
            }
            else
            {
                if (IsCurrentScreen)
                    TryToHideScreen(_currentScreen);
                
                for (int i = 0; i < _generalScreens.Count; i++)
                {
                    TryToHideScreen(_generalScreens[i]);
                }
            }
        }

        private bool IsCurrentScreen => _currentScreen != UNDEFINED;
        private bool IsPreviousScreen => _previousScreen != UNDEFINED;

        private bool TryToShowScreen(GameplayScreenType type)
        {
            if (_screens.TryGetValue(type, out IGameplayScreen screen))
            {
                screen.Show();
                return true;
            }

            return false;
        }
        
        private bool TryToHideScreen(GameplayScreenType type)
        {
            if (_screens.TryGetValue(type, out IGameplayScreen screen))
            {
                screen.Hide();
                return true;
            }

            return false;
        }
    }
}