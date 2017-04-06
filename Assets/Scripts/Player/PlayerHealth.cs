using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [HeaderAttribute("Health Properties")]
    [SerializeField]
    int maxHealth = 100;
    [SerializeField]
    AudioClip deathClip = null;

    [HeaderAttribute("Script References")]
    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    PlayerAttack playerAttack;

    [HeaderAttribute("Components")]
    [SerializeField]
    Animator animator;
    [SerializeField]
    CapsuleCollider capsuleCollider;
    [SerializeField]
    AudioSource audioSource;

    [HeaderAttribute("UI")]
    [SerializeField]
    FlashFade damageImage;
    [SerializeField]
    Slider healthSlider;

    [HeaderAttribute("Debugging Properties")]
    [SerializeField]
    bool isInvulnerable = false;

    int currentHealth;

    void Reset()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!IsAlive())
        {
            return;
        }

        if (!isInvulnerable)
        {
            currentHealth -= amount;
        }

        if (null != damageImage)
        {
            damageImage.Flash();
        }

        if (null != healthSlider)
        {
            healthSlider.value = currentHealth / (float)maxHealth;
        }

        if (!IsAlive())
        {
            if (null != playerMovement)
            {
                playerMovement.Defeated();
            }

            if (null != playerAttack)
            {
                playerAttack.Defeated();
            }

            animator.SetTrigger("Die");

            if (null != capsuleCollider)
            {
                capsuleCollider.enabled = false;
            }

            if (null != audioSource)
            {
                audioSource.clip = deathClip;
            }

            GameManager.instance.PlayerDied();
        }

        if (null != audioSource)
        {
            audioSource.Play();
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    void DeathComplete()
    {
        if (this == GameManager.instance.player)
        {
            GameManager.instance.PlayerDeathComplete();
        }
    }
}
