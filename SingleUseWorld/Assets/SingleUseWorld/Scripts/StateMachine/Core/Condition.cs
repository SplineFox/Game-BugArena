namespace SingleUseWorld.StateMachine
{
    /// <summary>
    /// Represents a condition to evaluate that 
    /// statement is met with expected result.
    /// </summary>
    public readonly struct Condition
    {
        internal readonly Statement _statement;
        internal readonly bool _expectedResult;

        public Condition(Statement statement, bool expectedResult)
        {
            _statement = statement;
            _expectedResult = expectedResult;
        }

        public bool IsMet()
        {
            bool result = _statement.GetEvaluation();
            return result == _expectedResult;
        }
    }
}
