using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kiadorn.BehaviourTree
{
    [CreateAssetMenu()]
    public class BehaviourTree : ScriptableObject
    {
        public Node rootNode = null;
        public Node.NodeState treeState = Node.NodeState.RUNNING;
        public List<Node> nodes = new List<Node>();

        public virtual Node.NodeState Update()
        {
            if (rootNode.state == Node.NodeState.RUNNING)
            {
                treeState = rootNode.Update();
            }

            return treeState;
        }

        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
#if UNITY_EDITOR
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
#endif
            return node;
        }

        public void DeleteNode(Node node)
        {
#if UNITY_EDITOR
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
#endif
        }


        public override string ToString()
        {
            if (rootNode != null)
            {
                return rootNode.ToString();

            }
            return base.ToString();
        }
    }
}
