using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public enum Side
    {
        Top = 1,
        Bottom = 2,
        Left = 3,
        Right = 4
    }

    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private float _lineHeight;
        [SerializeField] private Side _originSide;
        [SerializeField] private Side _targetSide;
        [SerializeField] private RectTransform _canvasRect;
        [SerializeField] protected RectTransform _originRect;
        [SerializeField] protected RectTransform _targetRect;

        private Image _lineImage;

        private void DrawLine()
        {
            if (_lineImage is null)
            {
                _lineImage = GetComponent<Image>();
            }

            Vector3 startPosition = _originRect.TransformPoint(_originRect.rect.center);
            Vector3 endPosition = _targetRect.TransformPoint(_targetRect.rect.center);
            
            
            _lineImage.rectTransform.position = (startPosition + endPosition) / 2;
            
            Vector3 direction = endPosition - startPosition;

            _lineImage.rectTransform.sizeDelta = new Vector2(direction.magnitude, _lineHeight);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _lineImage.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void OnRectTransformDimensionsChange()
        {
            DrawLine();
            //AdjustLineScale();
        }

        private void AdjustLineScale()
        {
            float aspectRatio = (float)Screen.width / Screen.height;
            _lineImage.rectTransform.localScale = new Vector2(aspectRatio, 1);
        }

        private Vector3 GetSideCenter(RectTransform rectTransform, Side side)
        {
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            switch (side)
            {
                case Side.Top:
                    return (worldCorners[1] + worldCorners[2]) / 2;
                case Side.Bottom:
                    return (worldCorners[0] + worldCorners[3]) / 2;
                case Side.Left:
                    return (worldCorners[0] + worldCorners[1]) / 2;
                case Side.Right:
                    return (worldCorners[2] + worldCorners[3]) / 2;
                default:
                    return rectTransform.position;
            }
        }
    }
}