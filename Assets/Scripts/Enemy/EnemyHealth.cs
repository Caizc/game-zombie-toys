using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public EnemySpawner spawner;

    [HeaderAttribute("Health Properties")]
    [SerializeField]
    int maxHealth = 100;
    [SerializeField]
    int scoreValue = 10;

    [HeaderAttribute("Defeated Effects")]
    [SerializeField]
    float sinkSpeed = 2.5f;
    [SerializeField]
    float deathEffectTime = 2f;
    [SerializeField]
    AudioClip deathClip = null;
    [SerializeField]
    AudioClip hurtClip = null;

    [HeaderAttribute("Script References")]
    [SerializeField]
    EnemyMovement enemyMovement;
    [SerializeField]
    EnemyAttack enemyAttack;

    [HeaderAttribute("Components")]
    [SerializeField]
    Animator animator;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    CapsuleCollider capsuleCollider;
    [SerializeField]
    ParticleSystem hitParticles;

    [HeaderAttribute("Debugging Properties")]
    [SerializeField]
    bool isInvulnerable;

    int currentHealth;
    bool isSinking;

    void Reset()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
        isSinking = false;
        capsuleCollider.isTrigger = false;

        if (null != audioSource)
        {
            audioSource.clip = hurtClip;
        }
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0 || isInvulnerable)
        {
            return;
        }
        else
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Defeated();
            }

            if (null != audioSource)
            {
                audioSource.Play();
            }

            hitParticles.Play();
        }
    }

    void Defeated()
    {
        capsuleCollider.isTrigger = true;

        animator.enabled = true;
        animator.SetTrigger("Dead");

        if (null != audioSource)
        {
            audioSource.clip = deathClip;
        }

        enemyMovement.Defeated();
        enemyAttack.Defeated();

        GameManager.instance.AddScore(scoreValue);

        Invoke("TurnOff", deathEffectTime);
    }

    void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void StartSinking()
    {
        isSinking = true;
    }
}
