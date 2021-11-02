using System;
using System.Collections.Generic;
using UnityEngine;
using SingleUseWorld.StateMachine.EditorTime;

namespace SingleUseWorld.StateMachine.RunTime
{
    public class StateRunner : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GraphModel _graphModel = default;

        private State _currentState;
        private State[] _states;

        private readonly Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            InstantiateStates();

            foreach (var state in _states)
                state.OnInitState(this);
        }

        private void Start()
        {
            _currentState.OnEnterState();
        }

        private void Update()
        {
            if (_currentState.OnTryGetTransition(out var transitionState))
                Transit(transitionState);

            _currentState.OnUpdateState();
        }
        #endregion

        #region Public Methods
        public new bool TryGetComponent<T>(out T component) where T : Component
        {
            var type = typeof(T);
            if (!_cachedComponents.TryGetValue(type, out var value))
            {
                if (base.TryGetComponent<T>(out component))
                    _cachedComponents.Add(type, component);

                return component != null;
            }

            component = (T)value;
            return true;
        }

        public T GetOrAddComponent<T>() where T : Component
        {
            if (!TryGetComponent<T>(out var component))
            {
                component = gameObject.AddComponent<T>();
                _cachedComponents.Add(typeof(T), component);
            }

            return component;
        }

        public new T GetComponent<T>() where T : Component
        {
            return TryGetComponent(out T component) ?
                component : throw new InvalidOperationException($"{typeof(T).Name} not found in {name}.");
        }
        #endregion

        #region Private Methods
        private void InstantiateStates()
        {
            var createdInstances = new Dictionary<ScriptableObject, object>();
            _states = GetStateInstances(createdInstances);
            _currentState = GetInitialStateInstance(createdInstances);
        }

        private State[] GetStateInstances(Dictionary<ScriptableObject, object> createdInstances)
        {
            var states = new List<State>();
            foreach (var stateModel in _graphModel.States)
            {
                var state = stateModel.GetStateInstance(createdInstances);
                states.Add(state);
            }
            return states.ToArray();
        }

        private State GetInitialStateInstance(Dictionary<ScriptableObject, object> createdInstances)
        {
            return _graphModel.InitialState.GetStateInstance(createdInstances);
        }

        private void Transit(State transitionState)
        {
            _currentState.OnExitState();
            _currentState = transitionState;
            _currentState.OnEnterState();
        }
        #endregion

#if UNITY_EDITOR
        private void OnGUI()
        {
            var contentText = (_currentState != null) ? _currentState._originModel.Name : "(no current state)";
            var contentStyle = GUI.skin.GetStyle("label");
            contentStyle.fontSize = 16;
            contentStyle.contentOffset = new Vector2(16, 16);
            contentStyle.normal.textColor = (_currentState != null) ? _currentState._originModel.Color : Color.white;

            GUILayout.Label(contentText, contentStyle);
        }
#endif
    }
}
