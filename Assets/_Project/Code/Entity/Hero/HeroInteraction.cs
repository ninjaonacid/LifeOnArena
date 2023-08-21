using Code.Infrastructure.InputSystem;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
{
    public class HeroInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _interactPoint;
        [SerializeField] private float _interactRadius;
        [SerializeField] private LayerMask _interactMask;

        private readonly Collider[] _interactableColliders = new Collider[2];
        private IInputSystem _input;

        [Inject]
        private void Construct(IInputSystem input)
        {
            _input = input;
        }

        // private void Update()
        // {
        //     if (_input.IsInteractButtonUp())
        //     {
        //         for (int i = 0; i < GetInteractables(); i++)
        //         {
        //             _interactableColliders[i].GetComponent<IInteractable>().Interact(this);
        //             break;
        //         }
        //     }
        // }

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
