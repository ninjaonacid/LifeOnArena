using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialElement : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TutorialElementIdentifier _elementId;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("TutorialElementClicked");
        }
    }
}
