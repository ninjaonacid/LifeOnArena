using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer.Unity;

namespace Code.Runtime.Modules.LocalizationProvider
{
    public class LocalizationService : IInitializable
    {
        public Action Initialized;
        private LocalizationSettings _localizationSettings;
        private readonly Dictionary<LocaleIdentifier, Locale> _availableLocales = new();

        public void Initialize()
        {
            var initializationTask = LocalizationSettings.InitializationOperation;
            initializationTask.Completed += OnLocalizationInitializationComplete;
        }

        private void OnLocalizationInitializationComplete(AsyncOperationHandle<LocalizationSettings> obj)
        {
            _localizationSettings = obj.Result;
            var locales = _localizationSettings.GetAvailableLocales().Locales;

            foreach (var locale in locales)
            {
                _availableLocales.Add(locale.Identifier, locale);
            }
         
            Initialized?.Invoke();
        }

        public void ChangeLanguage(string localeCode)
        {
            if (_availableLocales.TryGetValue(localeCode, out var locale))
            {
                _localizationSettings.SetSelectedLocale(locale);
            }
        }

        public void ChangeLanguage(SystemLanguage language)
        {
            if (_availableLocales.TryGetValue(language, out var locale))
            {
                _localizationSettings.SetSelectedLocale(locale);
            }
        }
    }
}
