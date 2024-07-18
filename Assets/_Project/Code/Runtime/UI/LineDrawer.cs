using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public enum Side
    {
        Top = 1,
        Bottom = 2,
        Left = 3,
        Right = 4,
        Center = 5,
        TopLeftCorner = 6,
        TopRightCorner = 7,
        BottomLeftCorner = 8,
        BottomRightCorner = 9
    }

    public class LineDrawer : MonoBehaviour
    {
        private static readonly Vector3[] _corners = new Vector3[4];
    
        [SerializeField] private float _lineHeight;
        [SerializeField] protected RectTransform _originRect;
        [SerializeField] protected RectTransform _targetRect;
        [SerializeField] protected Side _originConnectionSide = Side.Center;
        [SerializeField] protected Side _targetConnectionSide = Side.Center;
        [SerializeField] private Image _lineImage;
        private DrivenRectTransformTracker _tracker;

        private void Update()
        {
            if (_originRect != null && _targetRect != null)
            {
                DrawLine();
            }
        }

        private void OnValidate()
        {
            _tracker.Clear();
            _lineImage ??= GetComponent<Image>();
        }

        private void DrawLine()
        {
            
            _tracker.Add(this, _lineImage.rectTransform,
                DrivenTransformProperties.SizeDelta | DrivenTransformProperties.Rotation);
            
            var startPosWorld = GetConnectionPoint(_originRect, _originConnectionSide);
            var endPosWorld = GetConnectionPoint(_targetRect, _targetConnectionSide);

            var midPoint = (startPosWorld + endPosWorld) / 2;
            var direction = endPosWorld - startPosWorld;

            _lineImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            _lineImage.rectTransform.position = midPoint;
            
            var scaleFactor = _lineImage.rectTransform.lossyScale.x;
            _lineImage.rectTransform.sizeDelta = new Vector2(direction.magnitude / scaleFactor, _lineHeight);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _lineImage.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        private Vector3 GetConnectionPoint(RectTransform rectTransform, Side side)
        {
            rectTransform.GetWorldCorners(_corners);

            switch (side)
            {
                case Side.Top:
                    return (_corners[1] + _corners[2]) / 2;
                case Side.Bottom:
                    return (_corners[0] + _corners[3]) / 2;
                case Side.Left:
                    return (_corners[0] + _corners[1]) / 2;
                case Side.Right:
                    return (_corners[2] + _corners[3]) / 2;
                case Side.Center:
                    return (_corners[0] + _corners[2]) / 2;
                case Side.BottomLeftCorner:
                    return _corners[0];
                case Side.BottomRightCorner:
                    return _corners[3];
                case Side.TopLeftCorner:
                    return _corners[1];
                case Side.TopRightCorner:
                    return _corners[2];
                default:
                    return rectTransform.position;
            }
        }
    }
}