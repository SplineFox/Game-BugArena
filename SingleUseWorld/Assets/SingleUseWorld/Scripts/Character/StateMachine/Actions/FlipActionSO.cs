using UnityEngine;
using SingleUseWorld.StateMachine.Runtime;
using SingleUseWorld.StateMachine.Edittime;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "New FlipAction", menuName = "SingleUseWorld/StateMachine/Character/Actions/Create Flip Action")]
    public class FlipActionSO : ActionModel<FlipAction> { }

    public class FlipAction : Action
    {
        private CharacterInput _characterInput;
        private SpriteRenderer _spriteRenderer;

        public override void OnInitState(StateRunner stateRunner)
        {
            _characterInput = stateRunner.GetComponent<CharacterInput>();

            var character = stateRunner.GetComponent<Character>();
            _spriteRenderer = character.View.GetComponent<SpriteRenderer>();
        }

        public override void Perform()
        {
            if(_characterInput.MoveInput != Vector2.zero)
            {
                _spriteRenderer.flipX = _characterInput.MoveInput.x < 0;
            }
        }
    }
}