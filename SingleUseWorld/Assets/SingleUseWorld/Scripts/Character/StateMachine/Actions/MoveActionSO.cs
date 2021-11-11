using UnityEngine;
using SingleUseWorld.StateMachine.Runtime;
using SingleUseWorld.StateMachine.Edittime;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "New MoveAction", menuName = "SingleUseWorld/StateMachine/Character/Actions/Create Move Action")]
    public class MoveActionSO : ActionModel<MoveAction>
    {
        public float Speed = 5;
    }

    public class MoveAction : Action
    {
        private MoveActionSO _origin;

        private CharacterInput _characterInput;
        private CharacterAnimator _characterAnimator;
        private CharacterController2D _characterController2D;

        public override void OnInitState(StateRunner stateRunner)
        {
            _origin = (MoveActionSO)base.OriginModel;

            _characterInput = stateRunner.GetComponent<CharacterInput>();
            _characterAnimator = stateRunner.GetComponent<CharacterAnimator>();
            _characterController2D = stateRunner.GetComponent<CharacterController2D>();
        }

        public override void OnEnterState()
        {
            _characterAnimator.PlayMove();
        }

        public override void Perform()
        {
            var direction = _characterInput.MoveInput;
            var velocity = direction * _origin.Speed;
            _characterController2D.SetVelocity(velocity);
        }
    }
}