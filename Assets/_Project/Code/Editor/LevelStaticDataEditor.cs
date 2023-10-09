using System.Linq;
using Code.ConfigData.Levels;
using Code.ConfigData.Spawners;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.Logic.LevelObjectsSpawners;
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
                            x.MobId.Id, x.transform.position, x.RespawnCount))
                        .ToList();

                levelData.NextLevelDoorSpawners =
                    FindObjectsOfType<NextLevelDoorMarker>()
                        .Select(x => new NextLevelDoorSpawnerData(x.transform.position, x.transform.rotation))
                        .ToList();
                        
                levelData.LevelKey = SceneManager.GetActiveScene().name;

                levelData.HeroInitialPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
                levelData.HeroInitialRotation = GameObject.FindWithTag(InitialPointTag).transform.rotation;


            }

            EditorUtility.SetDirty(target);
        }
    }
}
