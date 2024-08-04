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

        public virtual T GetState<T>()
        {
            Type type = typeof(T);
            if (!stateDictionary.ContainsKey(type))
            {
                throw new NullReferenceException("No state of type: " + type + " found");
            }
            return (T)Convert.ChangeType(stateDictionary[type], type);
        }

        public virtual void TransitionTo<T>()
        {
            if (CurrentState.GetType() == typeof(T))
            {
                Debug.Log(string.Format("Already at state {0}", typeof(T).ToString()));
                return;
            }

            CurrentState.Exit();
            CurrentState = GetState<T>() as State;
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
            int stateIndex = -1;
            for (int i = 0; i < availableStates.Length; i++)
            {
                if (availableStates[i].GetType() == state.GetType())
                {
                    return i;
                }
            }
            return stateIndex;
        }

        public State GetState(int index)
        {
            stateDictionary.TryGetValue(availableStates[index].GetType(), out State state);
            return state;
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