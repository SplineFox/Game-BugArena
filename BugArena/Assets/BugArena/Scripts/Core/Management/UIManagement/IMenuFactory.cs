namespace BugArena
{
    public interface IMenuFactory
    {
        void CreateMenuRoot();
        BaseMenu CreateMainMenu();
        BaseMenu CreatePauseMenu();
        BaseMenu CreateRestartMenu();
    }
}