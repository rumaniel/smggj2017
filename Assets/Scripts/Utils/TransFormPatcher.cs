using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TransFormPatcher : MonoBehaviour 
{

    [ContextMenu ("Change Transform To Rect")]
    public void TransformToRectTransform()
    {
        Transform[] childrenTransform = GetComponentsInChildren<Transform>();

        foreach (Transform childTransform in childrenTransform)
        {
            childTransform.gameObject.AddComponent<RectTransform>();
        }   
    }

    [ContextMenu ("Sprite Renderer to Image")]
    public void RendererToImage()
    {
        SpriteRenderer[] childrenTransform = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer childTransform in childrenTransform)
        {
            if (childTransform.gameObject.GetComponent<Image>() == null)
            {
                Image newImage = childTransform.gameObject.AddComponent<Image>();
                newImage.sprite = childTransform.sprite;
                newImage.type = Image.Type.Simple;

                RectTransform rt = childTransform.gameObject.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(childTransform.sprite.rect.width, childTransform.sprite.rect.height);                
            }
            DestroyImmediate(childTransform);
        }
    }
}