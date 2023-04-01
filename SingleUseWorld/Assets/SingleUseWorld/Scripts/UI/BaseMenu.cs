using UnityEngine;

namespace SingleUseWorld
{
    public abstract class BaseMenu : MonoBehaviour
    {
        protected IInputService _inputService;

        public void OnCreate(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _inputService.SwitchTo(InputType.Menu);
            OnOpen();
        }

        private void OnDestroy()
        {
            _inputService.SwitchTo(InputType.Gameplay);
            OnClose();
        }

        protected void Close()
        {
            Destroy(gameObject);
        }

        protected abstract void OnOpen();
        protected abstract void OnClose();
    }
}