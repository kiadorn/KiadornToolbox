using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiadorn.BehaviourTree
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        BehaviourTree tree;

        // Start is called before the first frame update
        void Start()
        {
            tree = ScriptableObject.CreateInstance<BehaviourTree>();

            var log = ScriptableObject.CreateInstance<DebugLogNode>();
            log.message = "YEehaw";            
            
            var log1 = ScriptableObject.CreateInstance<DebugLogNode>();
            log1.message = "YEehaw 2 ";            
            
            var log2 = ScriptableObject.CreateInstance<DebugLogNode>();
            log2.message = "YEehaw 3 ";

            var sequence = ScriptableObject.CreateInstance<SequencerNode>();
            sequence.children.Add(log);
            sequence.children.Add(log1);
            sequence.children.Add(log2);


            var repeat = ScriptableObject.CreateInstance<RepeatNode>();
            repeat.child = sequence;

            tree.rootNode = repeat;
        }

        // Update is called once per frame
        void Update()
        {
            tree.Update();
        }
    }
}
