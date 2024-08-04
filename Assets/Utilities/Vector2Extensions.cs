using UnityEngine;

namespace Kiadorn.Utilities
{
    public static class Vector2Extensions
    {
        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }
    }
}