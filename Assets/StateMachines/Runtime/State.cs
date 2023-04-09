using Kiadorn.CustomUnityObject;

namespace Kiadorn.StateMachines
{
    public abstract class State : CustomScriptableObject
    {
        protected StateMachine owner;

        public virtual void Initialize(StateMachine owner)
        {
            this.owner = owner;
        }

        public virtual void Tick() { }

        public virtual void Enter() { }

        public virtual void Exit() { }
    }
}