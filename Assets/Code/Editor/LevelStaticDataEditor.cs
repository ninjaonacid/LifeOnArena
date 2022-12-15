
using System.Linq;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.Logic.LevelObjectsSpawners;
using Code.StaticData;
using Code.StaticData.Spawners;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPointTag = "InitialPoint";
        private const string NextLevelDoorTag = "NextLevelDoor";
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners =
                    FindObjectsOfType<SpawnMarker>()
                        .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id,
                            x.MonsterTypeId, x.transform.position, x.RespawnCount))
                        .ToList();

                levelData.WeaponPlatformSpawners =
                    FindObjectsOfType<WeaponPlatformMarker>()
                        .Select(x => new WeaponPlatformSpawnerData(x.GetComponent<UniqueId>().Id,
                            x.WeaponId, x.transform.position))
                        .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;

                levelData.HeroInitialPosition = GameObject.FindWithTag(InitialPointTag).transform.position;

                levelData.NextLevelDoorPosition = GameObject.FindWithTag(NextLevelDoorTag).transform.position;
                levelData.NextLevelDoorRotation = GameObject.FindWithTag(NextLevelDoorTag).transform.rotation;

            }

            EditorUtility.SetDirty(target);
        }
    }
}
