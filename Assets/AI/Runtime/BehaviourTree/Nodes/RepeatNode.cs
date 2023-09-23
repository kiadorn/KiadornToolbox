using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiadorn.BehaviourTree
{
    public class RepeatNode : DecoratorNode
    {
        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {
            throw new System.NotImplementedException();
        }

        protected override NodeState OnUpdate()
        {
            child.Update();
            return NodeState.RUNNING;
        }
    }
}
