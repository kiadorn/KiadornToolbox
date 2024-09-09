using System.Collections.Generic;

namespace Kiadorn.BehaviourTree
{
    //public class Selector : Node
    //{
    //    public Selector() : base() { }
    //    public Selector(List<Node> children) : base(children) { }

    //    public override NodeState Update()
    //    {
    //        foreach (Node node in children)
    //        {
    //            switch (node.Update())
    //            {
    //                case NodeState.FAILURE:
    //                    state = NodeState.FAILURE;
    //                    return state;
    //                case NodeState.SUCCESS:
    //                    state = NodeState.SUCCESS;
    //                    return state;
    //                case NodeState.RUNNING:
    //                    state = NodeState.RUNNING;
    //                    return state;
    //                default:
    //                    continue;
    //            }
    //        }

    //        state = NodeState.FAILURE;
    //        return state;
    //    }
    //}
}
