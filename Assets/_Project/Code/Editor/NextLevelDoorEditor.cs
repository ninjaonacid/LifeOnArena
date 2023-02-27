using Code.Logic.LevelObjectsSpawners;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Code.Editor
{
    [CustomEditor(typeof(NextLevelDoorMarker))]
    public class NextLevelDoorEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(NextLevelDoorMarker doorMarker, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = doorMarker.transform.localToWorldMatrix;

            var meshFilters = doorMarker.DoorPrefab.GetComponentsInChildren<MeshFilter>();

            for (var index = 0; index < meshFilters.Length; index++)
            {
                var meshFilter = meshFilters[index];
                
                Gizmos.DrawMesh(meshFilter.sharedMesh, 
                    Vector3.zero + doorMarker.DoorPrefab.transform.GetChild(index).transform.localPosition,
                    Quaternion.identity);
            }
      
        }
    }
}
