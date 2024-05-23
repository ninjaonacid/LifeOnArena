using System.Collections.Generic;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Audio;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class ActiveAbilityBlueprintBase : AbilityBlueprintBase
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private float _activeTime;
        [SerializeField] private bool _isCastAbility;
        [SerializeField] private VisualEffectData _visualEffect;
        [SerializeField] private SoundAudioFile _abilitySound;
        [SerializeField] private List<GameplayEffectBlueprint> _gameplayEffectBlueprints;
        public float Cooldown => _cooldown;
        public float ActiveTime => _activeTime;
        public bool IsCastAbility => _isCastAbility;
        public VisualEffectData VisualEffectData => _visualEffect;
        public SoundAudioFile AbilitySound => _abilitySound;

        public IReadOnlyList<GameplayEffectBlueprint> GameplayEffectsBlueprints =>
            _gameplayEffectBlueprints;

        public abstract ActiveAbility GetAbility();
    }
}
