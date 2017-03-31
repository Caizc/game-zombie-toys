using UnityEngine;

public class AVPlayer : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleEffect;
    [SerializeField]
    AudioSource audioEffect;

    void Reset()
    {
        particleEffect = GetComponent<ParticleSystem>();
        audioEffect = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (null != particleEffect)
        {
            particleEffect.Play(true);
        }

        if (null != audioEffect)
        {
            audioEffect.Play();
        }
    }
}
