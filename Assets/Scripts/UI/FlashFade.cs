using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashFade : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    [SerializeField]
    float flashSpeed = 5f;

    void Reset()
    {
        image = GetComponent<Image>();
    }

    public void Flash()
    {
        StopCoroutine(Fade());

        image.color = flashColor;

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (image.color.a > 0.01f)
        {
            image.color = Color.Lerp(image.color, Color.clear, flashSpeed * Time.deltaTime);

            yield return null;
        }

        image.color = Color.clear;
    }
}
