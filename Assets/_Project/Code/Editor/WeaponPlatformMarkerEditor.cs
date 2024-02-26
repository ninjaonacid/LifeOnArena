using Code.Runtime.Logic.LevelObjectsSpawners;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(WeaponPlatformMarker))]
    public class WeaponPlatformMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]

        
        public static void RenderCustomGizmo(WeaponPlatformMarker weaponPlatformSpawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = weaponPlatformSpawner.transform.localToWorldMatrix;

            var meshFilter = weaponPlatformSpawner.gizmoVisualization.GetComponent<MeshFilter>();
            Gizmos.DrawMesh(meshFilter.sharedMesh);
        }
    }
}
