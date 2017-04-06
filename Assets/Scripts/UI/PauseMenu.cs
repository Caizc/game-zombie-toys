using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused = false;

    [HeaderAttribute("UI")]
    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    Slider effectsSlider = null;
    [SerializeField]
    Slider musicSlider = null;

    [HeaderAttribute("Audio")]
    [SerializeField]
    AudioMixer masterMixer = null;
    [SerializeField]
    AudioMixerSnapshot mutedSnapshot = null;
    [SerializeField]
    AudioMixerSnapshot unMutedSnapshot = null;

    float originalTimeScale;

    void Awake()
    {
        originalTimeScale = Time.timeScale;

        float value;

        if (masterMixer.GetFloat("sfxVol", out value))
        {
            effectsSlider.value = value;
        }

        if (masterMixer.GetFloat("musicVol", out value))
        {
            musicSlider.value = value;
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            originalTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = originalTimeScale;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetEffectsLevel(float level)
    {
        masterMixer.SetFloat("sfxVol", level);
    }

    public void SetMusicLevel(float level)
    {
        masterMixer.SetFloat("musicVol", level);
    }

    public void ToggleVolume(bool soundOn)
    {
        if (soundOn)
        {
            unMutedSnapshot.TransitionTo(0f);
        }
        else
        {
            mutedSnapshot.TransitionTo(0f);
        }
    }
}
