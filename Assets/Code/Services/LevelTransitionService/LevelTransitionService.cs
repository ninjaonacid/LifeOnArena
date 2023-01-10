using System.Collections.Generic;
using System.Linq;
using Code.Services.RandomService;
using Code.StaticData.Levels;

namespace Code.Services.LevelTransitionService
{
    public class LevelTransitionService : ILevelTransitionService
    {
        private LevelConfig _currentLevel;
        private List<LevelConfig> _allLevels;
        private Dictionary<LocationType, List<LevelConfig>> _availableTransitions;
        private LocationType CurrentLocationGroup;

        private readonly IRandomService _randomService;

        public LevelTransitionService(IStaticDataService staticDataService, IRandomService random)
        {
            SetLevels(staticDataService.GetAllLevels());
            _randomService = random;
            SortLocations();
        }

        public void SetLevels(List<LevelConfig> allLevels)
        {
            _allLevels = allLevels;
        }

        public void SetCurrentLevel(LevelConfig levelData)
        {
            _currentLevel = levelData;

            CurrentLocationGroup = levelData.LocationType;
            RemoveCurrentLevelFromTransitions();
        }
        public LevelConfig GetNextLevel()
        {
            if (FindTransition(out var levelStaticData1)) return levelStaticData1;

            NextLocationGroup(CurrentLocationGroup);

            if (FindTransition(out var levelStaticData2)) return levelStaticData2;

            return null;

        }

        private bool FindTransition(out LevelConfig levelStaticData1)
        {
            if (_availableTransitions.TryGetValue(CurrentLocationGroup, out var levelTransitions) &&
                _availableTransitions[CurrentLocationGroup].Count > 0)
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
            if (_availableTransitions[CurrentLocationGroup].Contains(_currentLevel))
                _availableTransitions[CurrentLocationGroup].Remove(_currentLevel);
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
                    CurrentLocationGroup = LocationType.StoneDungeon;
                    break;
                case LocationType.StoneDungeon:
                    CurrentLocationGroup = LocationType.Forest;
                    break;
                case LocationType.Forest:
                    break;

                default: break;
            }
        }
    }
}
