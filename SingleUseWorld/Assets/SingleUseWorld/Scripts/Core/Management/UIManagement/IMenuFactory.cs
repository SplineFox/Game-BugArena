namespace SingleUseWorld
{
    public interface IMenuFactory
    {
        void CreateMenuRoot();
        void CreateMainMenu();
        void CreatePauseMenu();
        void CreateRestartMenu();
    }
}