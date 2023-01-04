using Code.Logic;
using Code.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    public class HeroInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _interactPoint;
        [SerializeField] private float _interactRadius;
        [SerializeField] private LayerMask _interactMask;

        private Collider[] _interactableColliders = new Collider[2];
        private IInputService _input;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (_input.IsInteractButtonUp())
            {
                for (int i = 0; i < GetInteractables(); i++)
                {
                    _interactableColliders[i].GetComponent<IInteractable>().Interact(this);
                    break;
                }
            }
        }

        private int GetInteractables()
        {

            return Physics.OverlapSphereNonAlloc(
                _interactPoint.gameObject.transform.position,
                _interactRadius,
                _interactableColliders,
                _interactMask);
        }

    }
}
