using System;
using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Config;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialService
    {
        public event Action<TutorialTask> OnTaskChanged;
        public event Action<TutorialElementIdentifier> OnElementHighlighted;
        public event Action<TutorialElementIdentifier> OnElementUnhighlighted;
        public event Action OnTutorialCompleted;

        private readonly ConfigProvider _configProvider;
        private readonly IGameDataContainer _gameData;
        private readonly SaveLoadService _saveLoad;
        
        private Queue<TutorialTask> _tutorialTasks = new();
        private List<TutorialTask> _completedTasks = new();
        private TutorialTask _currentTask;
        private ArrowUI _arrowPrefab;
        private ArrowUI _arrow;
       
        public TutorialService(ConfigProvider configProvider, IGameDataContainer gameData, SaveLoadService saveLoad)
        {
            _configProvider = configProvider;
            _gameData = gameData;
            _saveLoad = saveLoad;
            Initialize();
        }

        private void Initialize()
        {
            var tutorialConfig = _configProvider.GetTutorialConfig();
            _tutorialTasks = new Queue<TutorialTask>(tutorialConfig.TutorialTasks);
            _arrowPrefab = tutorialConfig.ArrowPrefab;
            _currentTask = _tutorialTasks.Count > 0 ? _tutorialTasks.Peek() : null;
        }

        public void StartTutorial()
        {
            if (_currentTask != null)
            {
                OnTaskChanged?.Invoke(_currentTask);
                HighlightCurrentElement();
            }
            else
            {
                OnTutorialCompleted?.Invoke();
            }
        }

        public void HandleElementInteraction(string elementId)
        {
            if (_currentTask == null) return;

            if (elementId == _currentTask.ElementId.Name)
            {
                UpdateTutorialStatus();
            }
        }
        
        public bool IsPreviousStepElement(TutorialElementIdentifier tutorialElementId)
        {
            foreach (var task in _completedTasks)
            {
                if (task.ElementId.Id == tutorialElementId.Id)
                {
                    return true;
                }
            }

            return false;
        }

        private void UpdateTutorialStatus()
        {
            if (_currentTask != null)
            {
                UnhighlightCurrentElement();
                _completedTasks.Add(_currentTask);
                _tutorialTasks.Dequeue();

                if (_tutorialTasks.Count > 0)
                {
                    _currentTask = _tutorialTasks.Peek();
                    OnTaskChanged?.Invoke(_currentTask);
                    HighlightCurrentElement();
                }
                else
                {
                    _currentTask = null;
                    OnTutorialCompleted?.Invoke();
                    _gameData.PlayerData.TutorialData.IsTutorialCompleted = true;
                    _saveLoad.SaveData();
                }

                Debug.Log($"Current task is {_currentTask}");
            }
        }

        private void HighlightCurrentElement()
        {
            if (_currentTask != null)
            {
                OnElementHighlighted?.Invoke(_currentTask.ElementId);
                Debug.Log($"{_currentTask.ElementId.Name}");
            }
        }

        private void UnhighlightCurrentElement()
        {
            if (_currentTask != null)
            {
                OnElementUnhighlighted?.Invoke(_currentTask.ElementId);
            }
        }

        public void HandlePointer(TutorialElement element)
        {
            if (_arrow != null)
            {
                Object.Destroy(_arrow.gameObject);
                _arrow = Object.Instantiate(_arrowPrefab);
            }
            else
            {
                _arrow = Object.Instantiate(_arrowPrefab);
            }

            _arrow.gameObject.SetActive(true);

            RectTransform transform;
            (transform = (RectTransform)_arrow.transform).SetParent(element.transform);

            transform.localScale = Vector3.one;
            transform.anchoredPosition = _currentTask.TaskData.TutorialArrowPosition;
            var transformLocalRotation = transform.localRotation;
            var eulerAngles = transformLocalRotation.eulerAngles;
            eulerAngles.z = _currentTask.TaskData.ZRotation;
            transform.localRotation = Quaternion.Euler(eulerAngles);

            var movementDistance = 50f;

            Vector2 forwardDirection = transform.up;

            Vector2 newPosition =
                transform.anchoredPosition + forwardDirection * movementDistance;

            _arrow.Movement(newPosition);
        }

        public TutorialTask GetCurrentTask()
        {
            return _currentTask;
        }

        public ArrowUI GetArrowUI()
        {
            return _arrowPrefab;
        }
    }
}