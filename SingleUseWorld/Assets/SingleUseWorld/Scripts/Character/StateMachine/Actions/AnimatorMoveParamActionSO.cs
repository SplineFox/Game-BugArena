using UnityEngine;
using SingleUseWorld.StateMachine.Runtime;
using SingleUseWorld.StateMachine.Edittime;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "New AnimatorMoveParamAction", menuName = "SingleUseWorld/StateMachine/Character/Actions/Create Animator Move Param Action")]
    public class AnimatorMoveParamActionSO : ActionModel<AnimatorMoveParamAction>
    {
        public string MoveXParamName = "MoveX";
        public string MoveYParamName = "MoveY";
    }

    public class AnimatorMoveParamAction : Action
    {
        private CharacterInput _characterInput;
        private Animator _animator;

        private int _moveXParamHash;
        private int _moveYParamHash;

        public override void OnInitState(StateRunner stateRunner)
        {
            _characterInput = stateRunner.GetComponent<CharacterInput>();
            _animator = stateRunner.GetComponent<Animator>();

            var originSO = (AnimatorMoveParamActionSO) base.OriginModel;
            _moveXParamHash = Animator.StringToHash(originSO.MoveXParamName);
            _moveYParamHash = Animator.StringToHash(originSO.MoveYParamName);
        }

        public override void Perform()
        {
            if (_characterInput.MoveInput != Vector2.zero)
            {
                _animator.SetFloat(_moveXParamHash, _characterInput.MoveInput.x);
                _animator.SetFloat(_moveYParamHash, _characterInput.MoveInput.y);
            }
        }
    }
}