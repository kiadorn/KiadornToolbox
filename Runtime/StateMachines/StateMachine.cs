using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kiadorn.StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }

        public event Action<State> StateChanged;

        [SerializeField]
        protected State[] availableStates;

        private readonly Dictionary<Type, State> stateDictionary = new Dictionary<Type, State>();

        protected virtual void Awake()
        {
            CreateStateCopies();
            if (CurrentState == null)
            {
                enabled = false;
            }
        }

        protected virtual void Start()
        {
            InitializeStart();
        }

        protected virtual void Update()
        {
            CurrentState.Tick();
        }

        protected void OnDestroy()
        {
            for (int i = 0; i < availableStates.Length; i++)
            {
                Destroy(availableStates[i]);
            }
        }

        public virtual T GetState<T>() where T : State
        {
            Type requestedType = typeof(T);

            if (stateDictionary.TryGetValue(requestedType, out State state))
            {
                return (T)state;
            }

            foreach (var entry in stateDictionary.Values)
            {
                if (requestedType.IsAssignableFrom(entry.GetType()))
                {
                    return (T)entry;
                }
            }

            throw new NullReferenceException("No state of type: " + requestedType?.ToString() + " found");
        }

        public virtual void TransitionTo<T>() where T : State
        {
            Type currentStateType = CurrentState.GetType();
            Type newStateType = typeof(T);

            if (currentStateType == newStateType)
            {
                Debug.Log($"Already at state {newStateType}");
                return;
            }

            CurrentState.Exit();
            CurrentState = GetState<T>();
            CurrentState.Enter();
            StateChanged?.Invoke(CurrentState);
        }

        public virtual void TransitionTo(State state)
        {
            if (CurrentState.GetType() == state.GetType())
            {
                Debug.Log(string.Format("Already at state {0}", state.name));
                return;
            }

            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
            StateChanged?.Invoke(CurrentState);
        }

        public int GetStateIndex(State state)
        {
            for (int i = 0; i < availableStates.Length; i++)
            {
                if (availableStates[i].GetType() == state.GetType())
                {
                    return i;
                }
            }
            return -1;
        }

        public State GetState(int index)
        {
            if (index >= 0 && index < availableStates.Length)
            {
                return availableStates[index];
            }
            return null;
        }

        private void CreateStateCopies()
        {
            for (int i = 0; i < availableStates.Length; i++)
            {
                State state = availableStates[i];
                if (state == null)
                {
                    continue;
                }

                State instance = Instantiate(state);
                stateDictionary.Add(instance.GetType(), instance);
                instance.InitializeAwake(this);
                availableStates[i] = instance;

                if (CurrentState != null)
                {
                    continue;
                }
                CurrentState = instance;
                CurrentState.Enter();
            }
        }

        private void InitializeStart()
        {
            var stateList = stateDictionary.Values.ToArray();
            for (int i = 0; i < stateList.Length; i++)
            {
                State state = stateList[i];
                if (state == null)
                {
                    continue;
                }

                state.InitializeStart(this);
            }
        }
    }
}