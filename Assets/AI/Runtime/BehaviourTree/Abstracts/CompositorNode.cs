using System.Collections.Generic;

namespace Kiadorn.BehaviourTree
{
    public abstract class CompositorNode : Node
    {
        public List<Node> children = new List<Node>();
    }
}