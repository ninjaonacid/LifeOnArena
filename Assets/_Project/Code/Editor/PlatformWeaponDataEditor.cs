using Code.Runtime.Logic.ShelterWeapons;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(WeaponPlatform))]
    public class PlatformWeaponDataEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            WeaponPlatform weaponPlatform = (WeaponPlatform)target;

            if (GUILayout.Button("Set Weapon"))
            {
                var weaponObj = PrefabUtility.InstantiatePrefab(weaponPlatform.WeaponData.WeaponPrefab) as UnityEngine.GameObject;
                weaponObj.transform.SetParent(weaponPlatform.WeaponContainer);
                weaponObj.gameObject.transform.localPosition = Vector3.zero;

                PrefabUtility.RecordPrefabInstancePropertyModifications(weaponPlatform.gameObject);
                EditorUtility.SetDirty(weaponPlatform.gameObject);
                
            }
        }
    }

}
