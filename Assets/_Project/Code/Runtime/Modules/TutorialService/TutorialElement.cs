using System;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.UI;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.Modules.TutorialService
{
    public class TutorialElement : CanvasElement, IPointerClickHandler
    {
        [SerializeField] private TutorialElementIdentifier _elementId;
        [SerializeField] private CanvasGroup _canvas;
        private Subject<TutorialElementIdentifier> _subject;
        public void OnPointerClick(PointerEventData eventData)
        {
            _subject?.OnNext(_elementId);
        }

        public IObservable<TutorialElementIdentifier> OnClickAsObservable() =>
            _subject ??= new Subject<TutorialElementIdentifier>();

        public void BlockInteractions(bool value)
        {
            if(_canvas is null) Debug.LogError(gameObject);

            if (_canvas != null) _canvas.blocksRaycasts = !value;
        }

        public TutorialElementIdentifier GetId() => _elementId;
    }
}
