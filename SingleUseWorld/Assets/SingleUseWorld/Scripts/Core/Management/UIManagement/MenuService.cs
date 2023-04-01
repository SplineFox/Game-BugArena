namespace SingleUseWorld
{
    public class MenuService : IMenuService
    {
        IMenuFactory _menuFactory;

        public MenuService(IMenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }

        public void Open(MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.Main:
                    _menuFactory.CreateMainMenu();
                    break;
                case MenuType.Pause:
                    _menuFactory.CreatePauseMenu();
                    break;
                case MenuType.Restart:
                    _menuFactory.CreateRestartMenu();
                    break;
            }
        }
    }
}
