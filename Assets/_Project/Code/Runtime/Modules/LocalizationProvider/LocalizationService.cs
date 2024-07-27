using System;
using System.Collections.Generic;
using Code.Runtime.Modules.WebApplicationModule;
using InstantGamesBridge;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer.Unity;

namespace Code.Runtime.Modules.LocalizationProvider
{
    public class LocalizationService : IInitializable
    {
        private LocalizationSettings _localizationSettings;
        private Locale _currentLocale;
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

            if (WebApplication.IsWebApp)
            {
                ChangeLanguage(Bridge.platform.language);
            }
            else
            {
                var systemLang = Application.systemLanguage;
                ChangeLanguage(systemLang);
            }

            _localizationSettings.OnSelectedLocaleChanged += SelectedLocaleChanged;
        }

        public void ChangeLanguage(string localeCode)
        {
            if (_availableLocales.TryGetValue(localeCode, out var locale))
            {
                _localizationSettings.SetSelectedLocale(locale);
                _currentLocale = locale;
            }
        }

        public void ChangeLanguage(SystemLanguage language)
        {
            if (_availableLocales.TryGetValue(language, out var locale))
            {
                _localizationSettings.SetSelectedLocale(locale);
                _currentLocale = locale;
            }
        }

        public string GetLocalizedString(string key)
        {
            var localizedString = _localizationSettings
                .GetStringDatabase()
                .GetLocalizedString(key, _currentLocale);

            return localizedString;
        }

        private void SelectedLocaleChanged(Locale obj)
        {
            _currentLocale = obj;
        }
        
    }
}
