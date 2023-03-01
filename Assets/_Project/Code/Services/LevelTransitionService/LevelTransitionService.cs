using System;
using System.Collections.Generic;
using System.Linq;
using Code.Services.RandomService;
using Code.StaticData.Levels;
using TMPro;


namespace Code.Services.LevelTransitionService
{
    public class LevelTransitionService : ILevelTransitionService
    {
        private LevelConfig _currentLevel;
        private List<LevelConfig> _allLevels;
        private Dictionary<LocationType, List<LevelConfig>> _availableTransitions;
        private List<LevelReward> _allRewards;
        private List<LevelReward> _availableRewards = new List<LevelReward>();

        private LocationType _currentLocationGroup;

        private readonly IRandomService _randomService;

        public LevelTransitionService(IStaticDataService staticDataService, IRandomService random)
        {
            Init(staticDataService.LoadLevels(), staticDataService.LoadRewards());

            _randomService = random;
            SortLocations();
        }

        public void Init(List<LevelConfig> allLevels, List<LevelReward> allRewards)
        {
            _allLevels = allLevels;
            _allRewards = allRewards;
        }


        public void SetCurrentLevel(LevelConfig levelData)
        {
            _currentLevel = levelData;

            _availableRewards = _allRewards.Select(x => x).ToList();

            _currentLocationGroup = levelData.LocationType;
            RemoveCurrentLevelFromTransitions();
        }

        public LevelReward GetReward()
        {
            var index = _randomService.GetRandomNumber(_availableRewards.Count);
            var reward = _availableRewards[index];
            _availableRewards.RemoveAt(index);
            return reward;
        }

        public LevelConfig GetNextLevel()
        {
            if (FindTransition(out var levelStaticData1)) return levelStaticData1;

            NextLocationGroup(_currentLocationGroup);

            if (FindTransition(out var levelStaticData2)) return levelStaticData2;

            return null;
        }

        private bool FindTransition(out LevelConfig levelStaticData1)
        {
            if (_availableTransitions.TryGetValue(_currentLocationGroup, out var levelTransitions) &&
                _availableTransitions[_currentLocationGroup].Count > 0)
            {
                var nextLevelIndex = _randomService.GetRandomNumber(levelTransitions.Count);
                var nextLevel = levelTransitions[nextLevelIndex];

                levelStaticData1 = nextLevel;
                return true;

            }

            levelStaticData1 = null;
            return false;
        }

        private void RemoveCurrentLevelFromTransitions()
        {
            if (_availableTransitions[_currentLocationGroup].Contains(_currentLevel))
                _availableTransitions[_currentLocationGroup].Remove(_currentLevel);
        }

        private void SortLocations()
        {
            _availableTransitions = _allLevels
                .GroupBy(x => x.LocationType)
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        private void NextLocationGroup(LocationType location)
        {
            switch (location)
            {
                case LocationType.Shelter:
                    _currentLocationGroup = LocationType.StoneDungeon;
                    break;
                case LocationType.StoneDungeon:
                    _currentLocationGroup = LocationType.Forest;
                    break;
                case LocationType.Forest:
                    break;

                default: break;
            }
        }
    }
}
