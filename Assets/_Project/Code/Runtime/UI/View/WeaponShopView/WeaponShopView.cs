﻿using UnityEngine;

namespace Code.Runtime.UI.View.WeaponShopView
{
    public class WeaponShopView : MonoBehaviour, IScreenView
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            Destroy(this);
        }
    }
}