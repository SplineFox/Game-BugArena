namespace SingleUseWorld
{
    public class MenuService : IMenuService
    {
        private IMenuFactory _menuFactory;
        private BaseMenu _currentMenu;

        public MenuService(IMenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }

        public void Open(MenuType menuType)
        {
            if(_currentMenu)
            {
                _currentMenu.Close();
                _currentMenu = null;
            }

            switch (menuType)
            {
                case MenuType.Main:
                    _currentMenu = _menuFactory.CreateMainMenu();
                    break;
                case MenuType.Pause:
                    _currentMenu = _menuFactory.CreatePauseMenu();
                    break;
                case MenuType.Restart:
                    _currentMenu = _menuFactory.CreateRestartMenu();
                    break;
            }
        }
    }
}
