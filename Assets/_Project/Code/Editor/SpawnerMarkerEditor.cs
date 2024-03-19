using Code.Runtime.Logic.EnemySpawners;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnerMarkerEditor : OdinEditor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(spawner.transform.position, 2f);
        }
    }
}