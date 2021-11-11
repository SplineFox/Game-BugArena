using UnityEngine;
using SingleUseWorld.StateMachine.Runtime;
using SingleUseWorld.StateMachine.Edittime;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "New IsMovingStatement", menuName = "SingleUseWorld/StateMachine/Character/Statements/Create Is Moving Statement")]
    public class IsMovingStatementSO : StatementModel<IsMovingStatement> { }

    public class IsMovingStatement : Statement
    {
        private CharacterInput _characterInput;

        public override void OnInitState(StateRunner stateRunner)
        {
            _characterInput = stateRunner.GetComponent<CharacterInput>();
        }

        protected override bool Evaluate()
        {
            return _characterInput.MoveInput.magnitude != 0f;
        }
    }
}