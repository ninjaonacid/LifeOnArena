using System;
using System.Collections.Generic;
using Code.Runtime.Core.Config;
using Code.Runtime.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialService
    {
        public Action<TutorialTask> OnTaskChanged;
        private readonly ConfigProvider _configProvider;
        private Queue<TutorialTask> _tutorialTasks = new();
        private List<TutorialTask> _completedTasks = new();
        private TutorialTask _currentTask;
        private ArrowUI _arrowPrefab;
        private ArrowUI _arrow;

        public TutorialService(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
            Initialize();
        }

        private void Initialize()
        {
            var tutorialConfig = _configProvider.GetTutorialConfig();
            _tutorialTasks = new Queue<TutorialTask>(tutorialConfig.TutorialTasks);
            _arrowPrefab = tutorialConfig.ArrowPrefab;
            _currentTask = _tutorialTasks.Peek();
        }
        
        public void UpdateTutorialStatus(TutorialTask task)
        {
            _completedTasks.Add(_currentTask);
            _tutorialTasks.Dequeue();
            if (_tutorialTasks.Count > 0)
            {
                _currentTask = _tutorialTasks.Peek();
                OnTaskChanged.Invoke(_currentTask);
            }
            else
            {
                
            }
            
            Debug.Log($"Current task is {_currentTask}");
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
