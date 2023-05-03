namespace Application.UI.Common
{
    public interface IScreenController
    {
        void Add(GameplayScreenType type, IGameplayScreen screen);
        void Show(GameplayScreenType type);
        void Hide(GameplayScreenType type, bool showPrevious = false);
        void ShowGeneral(GameplayScreenType type);
        void HideGeneral(GameplayScreenType type);
        void ShowLastAll();
        void HideAll(bool keepActive = false);
    }
}