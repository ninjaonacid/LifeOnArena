using System.Collections.Generic;
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
            _localizationSettings.GetAvailableLocales().GetLocale(Application.systemLanguage);
            var applicationLanguage = Application.systemLanguage;

            foreach (var locale in locales)
            {
                _availableLocales.Add(locale.Identifier, locale);
            }

            var language = Bridge.platform.language;
            ChangeLanguage(language);
            
        }

        public void ChangeLanguage(string localeCode)
        {
            _localizationSettings.SetSelectedLocale(_availableLocales[localeCode]);
        }

        public void ChangeLanguage(SystemLanguage language)
        {
            _localizationSettings.SetSelectedLocale(_availableLocales[language]);
        }
    }
}
