using UnityEngine;

namespace Kiadorn.Rigging
{
    public abstract class RigConstraintHolder : MonoBehaviour
    {
        protected float targetWeight;

        public virtual void SetTargetWeight(float weight)
        {
            targetWeight = weight;
        }

        public abstract void SetTargetPosition(Vector3 target);
    }
}