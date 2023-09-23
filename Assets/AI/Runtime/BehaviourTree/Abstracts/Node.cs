using System.Collections.Generic;
using UnityEngine;

namespace Kiadorn.BehaviourTree
{
    public abstract class Node : ScriptableObject
    {

        public enum NodeState
        {
            RUNNING,
            SUCCESS,
            FAILURE
        }

        public NodeState state = NodeState.RUNNING;
        public bool started = false;
        public string guid;

        //public Node parent;
        //protected List<Node> children = new List<Node>();

        //private Dictionary<string, object> m_dataContext = new Dictionary<string, object>();

        public virtual NodeState Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state == NodeState.FAILURE || state == NodeState.SUCCESS)
            {
                OnStop();
                started = false;
            }

            return state;

        }


        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();


        //public void SetData(string key, object value)
        //{
        //    m_dataContext[key] = value;
        //}

        //public object GetData(string key)
        //{
        //    object value = null;
        //    if (m_dataContext.TryGetValue(key, out value))
        //    {
        //        return value;
        //    }

        //    Node node = parent;
        //    while (node != null)
        //    {
        //        value = node.GetData(key);
        //        if (value != null)
        //        {
        //            return value;
        //        }

        //        node = node.parent;
        //    }
        //    return null;
        //}

        //public bool ClearData(string key)
        //{
        //    if (m_dataContext.ContainsKey(key))
        //    {
        //        m_dataContext.Remove(key);
        //        return true;
        //    }

        //    Node node = parent;
        //    while (node != null)
        //    {
        //        bool cleared = node.ClearData(key);
        //        if (cleared)
        //        {
        //            return true;
        //        }
        //        node = node.parent;
        //    }

        //    return false;
        //}
    }
}
