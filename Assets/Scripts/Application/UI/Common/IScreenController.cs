namespace Application.UI.Common
{
    public interface IScreenController
    {
        void Add(GameplayScreenType type, IGameplayScreen screen);
        void ActiveScreen(GameplayScreenType type, bool asGeneralScreen = false);
        void HideLastScreen();
        void HideScreen(GameplayScreenType type);
    }
}