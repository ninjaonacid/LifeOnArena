using Code.Logic.ShelterWeapons;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(WeaponPlatform))]
    public class PlatformWeaponDataEditor : UnityEditor.Editor
    {
        private const string WeaponPlatformTag = "WeaponPlatform";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            WeaponPlatform weaponPlatform = (WeaponPlatform)target;

            if (GUILayout.Button("Set Weapon"))
            {
                var weapon = PrefabUtility.InstantiatePrefab(weaponPlatform.WeaponData.WeaponPrefab) as GameObject;
                weapon.transform.SetParent(weaponPlatform.WeaponContainer);
                weapon.gameObject.transform.localPosition = Vector3.zero;
            }
        }
    }

}
