using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class EllipsisUGUIText : Text
{
    private readonly StringBuilder updatedStringBuilder = new StringBuilder(50);
    private const char ellipsis = (char)8230;

    private string cachedText = "";
    public override string text
    {
        get
        {
            return cachedText;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                if (string.IsNullOrEmpty(cachedText))
                    return;
                cachedText = "";
                UpdateGenerateText();

                SetVerticesDirty();
            }
            else if (cachedText != value)
            {
                cachedText = value;
                UpdateGenerateText();

                SetVerticesDirty();
                SetLayoutDirty();
            }
        }
    }

    private void UpdateGenerateText()
    {
        m_Text = cachedText;

        Vector2 extents = rectTransform.rect.size;
        extents.x = extents.x < 0 ? -extents.x : extents.x;

        if (extents.x / preferredWidth < 1)
        {
            float tempPrefferedWidth = preferredWidth;

            var settings = GetGenerationSettings(Vector2.zero);

            string tempString = m_Text;

            while (extents.x / tempPrefferedWidth < 1)
            {
                tempString = tempString.Substring(0, tempString.Length - 1);
                tempPrefferedWidth = cachedTextGeneratorForLayout.GetPreferredWidth(tempString, settings) / pixelsPerUnit;
            }

            updatedStringBuilder.Length = 0;
            updatedStringBuilder.Append(tempString);
            updatedStringBuilder.Append(ellipsis);
            m_Text = updatedStringBuilder.ToString();
            updatedStringBuilder.Length = 0;
        }
    }

    readonly UIVertex[] m_TempVerts = new UIVertex[4];
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        if (font == null)
            return;

        // We don't care if we the font Texture changes while we are doing our Update.
        // The end result of cachedTextGenerator will be valid for this instance.
        // Otherwise we can get issues like Case 619238.
        m_DisableFontTextureRebuiltCallback = true;

        Vector2 extents = rectTransform.rect.size;

        var settings = GetGenerationSettings(extents);
        cachedTextGenerator.Populate(m_Text, settings);

        Rect inputRect = rectTransform.rect;

        // get the text alignment anchor point for the text in local space
        Vector2 textAnchorPivot = GetTextAnchorPivot(base.alignment);
        Vector2 refPoint = Vector2.zero;
        refPoint.x = (textAnchorPivot.x == 1 ? inputRect.xMax : inputRect.xMin);
        refPoint.y = (textAnchorPivot.y == 0 ? inputRect.yMin : inputRect.yMax);

        // Determine fraction of pixel to offset text mesh.
        Vector2 roundingOffset = PixelAdjustPoint(refPoint) - refPoint;

        // Apply the offset to the vertices
        IList<UIVertex> verts = cachedTextGenerator.verts;
        float unitsPerPixel = 1 / pixelsPerUnit;
        //Last 4 verts are always a new line...
        int vertCount = verts.Count - 4;

        toFill.Clear();
        if (roundingOffset != Vector2.zero)
        {
            for (int i = 0; i < vertCount; ++i)
            {
                int tempVertsIndex = i & 3;
                m_TempVerts[tempVertsIndex] = verts[i];
                m_TempVerts[tempVertsIndex].position *= unitsPerPixel;
                m_TempVerts[tempVertsIndex].position.x += roundingOffset.x;
                m_TempVerts[tempVertsIndex].position.y += roundingOffset.y;
                if (tempVertsIndex == 3)
                    toFill.AddUIVertexQuad(m_TempVerts);
            }
        }
        else
        {
            for (int i = 0; i < vertCount; ++i)
            {
                int tempVertsIndex = i & 3;
                m_TempVerts[tempVertsIndex] = verts[i];
                m_TempVerts[tempVertsIndex].position *= unitsPerPixel;
                if (tempVertsIndex == 3)
                    toFill.AddUIVertexQuad(m_TempVerts);
            }
        }
        m_DisableFontTextureRebuiltCallback = false;
    }
}