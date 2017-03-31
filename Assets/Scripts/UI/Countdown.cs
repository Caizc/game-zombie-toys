using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    float timeOfCountdownFinish;
    static WaitForSeconds updateDelay = new WaitForSeconds(0.25f);

    void Reset()
    {
        slider = GetComponent<Slider>();
    }

    void Awake()
    {
        slider.minValue = 0f;
        slider.maxValue = 0f;
    }

    public void BeginCountdown(float cooldown)
    {
        timeOfCountdownFinish = Time.time + cooldown;

        slider.maxValue = cooldown;
        slider.value = cooldown;

        StartCoroutine(UpdateCountdownBar());
    }

    IEnumerator UpdateCountdownBar()
    {
        while (slider.value > 0f)
        {
            slider.value = timeOfCountdownFinish - Time.time;
            yield return updateDelay;
        }

        slider.maxValue = 0f;
    }
}
