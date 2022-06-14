using UnityEngine;

namespace CodeBase.Enemy
{
    public class PhysicsDebug : MonoBehaviour
    {
        public static void DrawDebug(Vector3 worldPos, float radius, float seconds)
        {
            Debug.DrawRay(worldPos, radius * Vector3.up, Color.red, seconds);
            Debug.DrawRay(worldPos, radius * Vector3.down, Color.red, seconds);
            Debug.DrawRay(worldPos, radius * Vector3.left, Color.red, seconds);
            Debug.DrawRay(worldPos, radius * Vector3.right, Color.red, seconds);
            Debug.DrawRay(worldPos, radius * Vector3.forward, Color.red, seconds);
            Debug.DrawRay(worldPos, radius * Vector3.back, Color.red, seconds);
        }
    }
}