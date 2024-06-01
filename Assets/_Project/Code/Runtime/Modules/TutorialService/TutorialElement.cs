using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialElement : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] public string Id;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}
