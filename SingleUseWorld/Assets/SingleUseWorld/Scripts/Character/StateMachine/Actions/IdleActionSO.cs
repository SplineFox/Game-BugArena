using UnityEngine;
using SingleUseWorld.StateMachine.Runtime;
using SingleUseWorld.StateMachine.Edittime;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "New IdleAction", menuName = "SingleUseWorld/StateMachine/Character/Actions/Create Idle Action")]
    public class IdleActionSO : ActionModel<IdleAction> { }

    public class IdleAction : Action
    {
        private CharacterAnimator _characterAnimator;
        private CharacterController2D _characterController2D;

        public override void OnInitState(StateRunner stateRunner)
        {
            _characterAnimator = stateRunner.GetComponent<CharacterAnimator>();
            _characterController2D = stateRunner.GetComponent<CharacterController2D>();
        }

        public override void OnEnterState()
        {
            _characterAnimator.PlayIdle();
            _characterController2D.SetVelocity(Vector2.zero);
        }

        public override void Perform()
        { }
    }
}