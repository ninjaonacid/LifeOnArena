using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _interactPoint;
        [SerializeField] private float _interactRadius;
        [SerializeField] private LayerMask _interactMask;

        private readonly Collider[] _interactableColliders = new Collider[2];
        private PlayerControls _controls;

        [Inject]
        private void Construct(PlayerControls controls)
        {
            _controls = controls;
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
