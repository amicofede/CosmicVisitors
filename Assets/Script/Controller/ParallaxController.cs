using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxController: MonoBehaviour
{
    [SerializeField] private List<Image> images = new List<Image>();

    #region UnityMessages
    private void Awake()
    {
        SetImageSize();
        SetImagePosition();
    }

    private void Update()
    {
        foreach (var image in images)
        {
            image.transform.Translate(Vector3.right * 100 * Time.deltaTime);
            if (image.rectTransform.position.y < 0)
            {
                image.rectTransform.position = new Vector2(0, images.Count * 1920f);
            }
        }
    }
    #endregion

    private void SetImageSize()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].rectTransform.sizeDelta = new Vector2(Screen.height, Screen.width);
        }
    }

    private void SetImagePosition()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].rectTransform.position = new Vector3(0f, (1920f + i * Screen.height), 0f);
        }
    }

}
