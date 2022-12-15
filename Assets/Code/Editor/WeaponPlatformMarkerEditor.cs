using Code.Logic.LevelObjectsSpawners;
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
            Gizmos.color = Color.white;
            Gizmos.DrawCube(weaponPlatformSpawner.transform.position, new Vector3(5,5,5));
        }
    }
}
