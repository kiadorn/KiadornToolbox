using UnityEngine;

namespace Kiadorn.BehaviourTree
{
    public class WaitNode : ActionNode
    {
        public float duration = 1;
        private float currentTimer;

        protected override void OnStart()
        {
            currentTimer = 0;
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= duration)
            {
                return NodeState.SUCCESS;
            }

            return NodeState.RUNNING;
        }
    }
}
