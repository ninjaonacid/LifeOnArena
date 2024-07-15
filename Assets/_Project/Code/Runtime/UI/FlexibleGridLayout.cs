using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public class FlexibleGridLayout : LayoutGroup
    {
        public int rows;
        public int columns;
        public Vector2 cellSize;
        public Vector2 spacing;
        

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            RectTransform parentRect = rectTransform;
            var rect = rectTransform.rect;
            float parentWidth = rect.width;
            float parentHeight = rect.height;
            float cellWidth = (parentWidth - (columns - 1) * spacing.x) / columns;
            float cellHeight = (parentHeight - (rows - 1) * spacing.y / rows);
            cellSize.x = cellWidth;
            cellSize.y = cellHeight;
        }

        public override void CalculateLayoutInputVertical()
        {
            // float parentHeight = rectTransform.rect.height;
            // float cellHeight = (parentHeight - (rows - 1) * spacing.y) / rows;
            // cellSize.y = cellHeight;
        }

        public override void SetLayoutHorizontal()
        {
            float x = 0f;
            float y = 0f;
            int column = 0;
            int row = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform child = rectChildren[i];

                child.sizeDelta = cellSize;
                child.anchoredPosition = new Vector2(x, -y);

                x += cellSize.x + spacing.x;
                column++;

                if (column >= columns)
                {
                    column = 0;
                    row++;
                    x = 0f;
                    y += cellSize.y + spacing.y;
                }
            }
        }

        public override void SetLayoutVertical()
        {
            // Not needed for this layout group
        }
    }
}