using Kiadorn.CustomUnityObject;

namespace Kiadorn.StateMachines
{
    public abstract class State : CustomScriptableObject
    {
        public StateMachine Owner { get { return owner; } }

        protected StateMachine owner;

        public virtual void InitializeAwake(StateMachine owner)
        {
            this.owner = owner;
        }

        public virtual void InitializeStart(StateMachine owner) { }

        public virtual void Tick() { }

        public virtual void Enter() { }

        public virtual void Exit() { }
    }
}