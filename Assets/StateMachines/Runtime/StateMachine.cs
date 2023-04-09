using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kiadorn.StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }

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
            CurrentState.Exit();
            CurrentState = GetState<T>() as State;
            CurrentState.Enter();
        }

        private void CreateStateCopies()
        {
            foreach (State state in availableStates)
            {
                if (state == null)
                {
                    continue;
                }

                State instance = Instantiate(state);
                instance.Initialize(this);
                stateDictionary.Add(instance.GetType(), instance);

                if (CurrentState != null)
                {
                    continue;
                }
                CurrentState = instance;
                CurrentState.Enter();
            }
        }
    }
}