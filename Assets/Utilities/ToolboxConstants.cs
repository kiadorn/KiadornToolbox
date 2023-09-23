using UnityEngine;

namespace Kiadorn.Utilities
{
    public class Constants
    {
        public const string BaseMenuName = "Kiadorn/";

        #region AnimatorVariables
        public const string VelocityX = "VelocityX";
        public const string VelocityZ = "VelocityZ";
        public const string Velocity = "Velocity";

        public static int VelocityXHash = Animator.StringToHash(VelocityX);
        public static int VelocityZHash = Animator.StringToHash(VelocityZ);
        public static int VelocityHash = Animator.StringToHash(Velocity);
        #endregion
    }
}
