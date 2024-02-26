using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Logic.LevelObjectsSpawners
{
    public class WeaponPlatformSpawner : MonoBehaviour, ISave
    {
        public WeaponId WeaponId;
        public string Id;
        public bool IsPurchased { get; private set; }

        private IItemFactory _itemFactory;
        public void Construct(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            //SpawnPlatform();
        }

      

        // private async void SpawnPlatform() =>
        //      await _itemFactory.CreateWeaponPlatform(WeaponId, transform);


        private void WeaponPurchased() 
        {
            IsPurchased = true;
        }

        public  void LoadData(PlayerData data)
        {
            if (data.WorldData.WeaponPurchaseData.PurchasedWeapons.Contains(Id))
            {
                IsPurchased = true;
               // var platform = await SpawnPlatform();
               // platform.UnlockWeapon();
            }

            {
                //var platform = await SpawnPlatform();
               // platform.OnWeaponPurchase += WeaponPurchased;
            }
        }

        public void UpdateData(PlayerData data)
        {
            if (IsPurchased)
                data.WorldData.WeaponPurchaseData.PurchasedWeapons.Add(Id);
        }
    }
}
