namespace Kiadorn.BehaviourTree
{
    public class SequencerNode : CompositorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {

        }

        protected override NodeState OnUpdate()
        {
            bool anyChildIsRunning = false;

            foreach (Node child in children)
            {
                switch (child.Update())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}
