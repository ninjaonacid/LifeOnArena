using System.Collections.Generic;
using InstantGamesBridge;
using UnityEngine;
using UnityEngine.UI;

namespace Examples
{
    public class PaymentsPanel : MonoBehaviour
    {
        [SerializeField] private Text _isSupported;
        [SerializeField] private Button _getCatalogButton;
        [SerializeField] private Button _getPurchasesButton;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Button _consumePurchaseButton;
        [SerializeField] private GameObject _overlay;

        private void Start()
        {
            _isSupported.text = $"Is Supported: { Bridge.payments.isSupported }";
            _getCatalogButton.onClick.AddListener(OnGetCatalogButtonClicked);
            _getPurchasesButton.onClick.AddListener(OnGetPurchasesButtonClicked);
            _purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
            _consumePurchaseButton.onClick.AddListener(OnConsumePurchaseButtonClicked);
        }

        private void OnGetCatalogButtonClicked()
        {
            _overlay.SetActive(true);

            Bridge.payments.GetCatalog((success, list) =>
            {
                Debug.Log($"OnGetCatalogCompleted, success: {success}, items:");
                
                if (success)
                {
                    foreach (var catalogItemData in list)
                    {
                        Debug.Log($"ID: {catalogItemData.id}, title: {catalogItemData.title}, description: {catalogItemData.description}, icon: {catalogItemData.icon}, price: {catalogItemData.price}, priceValue: {catalogItemData.priceValue}, priceCurrencyCode: {catalogItemData.priceCurrencyCode}");
                    }
                }
                
                _overlay.SetActive(false);
            });
        }

        private void OnGetPurchasesButtonClicked()
        {
            _overlay.SetActive(true);

            Bridge.payments.GetPurchases((success, list) =>
            {
                Debug.Log($"OnGetPurchasesCompleted, success: {success}, items:");
                
                if (success)
                {
                    foreach (var purchaseData in list)
                    {
                        Debug.Log($"\n ID: {purchaseData.id}, token: {purchaseData.token}");
                    }
                }
                
                _overlay.SetActive(false);
            });
        }
        
        private void OnPurchaseButtonClicked()
        {
            _overlay.SetActive(true);
            
            var options = new Dictionary<string, object>();
            switch (Bridge.platform.id)
            {
                case "yandex":
                    options.Add("id", "PURCHASE_ID");
                    break;
            }
            
            Bridge.payments.Purchase(options, _ => { _overlay.SetActive(false); });
        }

        private void OnConsumePurchaseButtonClicked()
        {
            _overlay.SetActive(true);
            
            var options = new Dictionary<string, object>();
            switch (Bridge.platform.id)
            {
                case "yandex":
                    options.Add("purchaseToken", "PURCHASE_TOKEN");
                    break;
            }
            
            Bridge.payments.ConsumePurchase(options, _ => { _overlay.SetActive(false); });
        }
    }
}