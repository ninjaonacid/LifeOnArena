using Code.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnerMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(spawner.transform.position, 1.5f);
        }
    }
}