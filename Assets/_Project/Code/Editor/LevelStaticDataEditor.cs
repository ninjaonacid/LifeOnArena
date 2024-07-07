using System.Linq;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.ConfigData.Spawners;
using Code.Runtime.Logic;
using Code.Runtime.Logic.EnemySpawners;
using Code.Runtime.Logic.LevelObjectsSpawners;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
    [CustomEditor(typeof(LevelConfig))]
    public class LevelStaticDataEditor : OdinEditor
    {
        private const string InitialPointTag = "InitialPoint";
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelConfig levelData = (LevelConfig)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners =
                    FindObjectsOfType<SpawnMarker>()
                        .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id,
                            x.MobId, x.transform.position, x.SpawnCount, x.SpawnTimer, x.EnemyType))
                        .ToList();

                levelData.SceneKey = SceneManager.GetActiveScene().name;

                levelData.HeroInitialPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
                levelData.HeroInitialRotation = GameObject.FindWithTag(InitialPointTag).transform.rotation;


            }

            EditorUtility.SetDirty(target);
        }
    }
}
