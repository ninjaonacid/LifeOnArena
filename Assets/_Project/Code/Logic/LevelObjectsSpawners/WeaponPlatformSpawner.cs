using System;
using System.Threading.Tasks;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Logic.ShelterWeapons;
using Code.Services.PersistentProgress;
using Code.StaticData;
using Code.StaticData.Identifiers;
using UnityEngine;

namespace Code.Logic.LevelObjectsSpawners
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
            SpawnPlatform();
        }

      

        private async void SpawnPlatform() =>
             await _itemFactory.CreateWeaponPlatform(WeaponId, transform);


        private void WeaponPurchased() 
        {
            IsPurchased = true;
        }

        public  void LoadProgress(PlayerProgress progress)
        {
            if (progress.WorldData.WeaponPurchaseData.PurchasedWeapons.Contains(Id))
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

        public void UpdateProgress(PlayerProgress progress)
        {
            if (IsPurchased)
                progress.WorldData.WeaponPurchaseData.PurchasedWeapons.Add(Id);
        }
    }
}
