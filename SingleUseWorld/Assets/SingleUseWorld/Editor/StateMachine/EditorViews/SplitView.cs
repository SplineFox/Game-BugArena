using UnityEngine;
using UnityEngine.UIElements;

namespace SingleUseWorld.StateMachine.Edittime
{
    public class SplitView : TwoPaneSplitView
    {
        #region Nested Classes
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }
        #endregion
    }
}