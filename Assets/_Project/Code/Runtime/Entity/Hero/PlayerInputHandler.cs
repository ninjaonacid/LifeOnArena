using System;
using System.Collections.Generic;
using Code.Runtime.Modules.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerControls _controls;
        private readonly List<InputAction> actionsList = new();
        private readonly List<Action<InputAction.CallbackContext>> actionDelegates = new();
        
        public event Action<int> OnAbilityActivated;
        
        [Inject]
        public void Construct(PlayerControls controls)
        {
            _controls = controls;
        }
        private void Start()
        {
            InitializeInputActions();
        }

        private void InitializeInputActions()
        {
            actionsList.Add(_controls.Player.SkillSlot1);
            actionsList.Add(_controls.Player.SkillSlot2);
            actionsList.Add(_controls.Player.Attack);

            for (var index = 0; index < actionsList.Count; index++)
            {
                var capturedIndex = index;
                Action<InputAction.CallbackContext> actionDelegate = context => OnAbilityInput(context, capturedIndex);
                actionDelegates.Add(actionDelegate);
                actionsList[index].performed += actionDelegate;
            }
        }
        
        private bool IsPointerOverUI()
        {
            if (EventSystem.current == null)
                return false;
            
            var pointerPosition = _controls.UI.Pointer.ReadValue<Vector2>();
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = pointerPosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }


        private void OnDestroy()
        {
            for (int index = 0; index < actionsList.Count; index++)
            {
                actionsList[index].performed -= actionDelegates[index];
            }
        }

        private void OnAbilityInput(InputAction.CallbackContext context, int actionIndex)
        {
            if (WebApplication.IsMobile)
            {
                OnAbilityActivated?.Invoke(actionIndex);
            }
            else if(!IsPointerOverUI())
            {
                OnAbilityActivated?.Invoke(actionIndex);
            }
        }
    }
}