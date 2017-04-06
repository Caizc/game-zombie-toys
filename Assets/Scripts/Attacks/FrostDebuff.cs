using UnityEngine;

public class FrostDebuff : MonoBehaviour
{
    [SerializeField]
    GameObject mist;
    [SerializeField]
    GameObject iceBlock;
    [SerializeField]
    float freezeDelay = 1f;
    [SerializeField]
    float freezeDuration = 2f;

    EnemyMovement target;
    float timeToToggleEffect;
    bool isFreezing;
    bool isAttached;

    void OnEnable()
    {
        mist.SetActive(true);
        iceBlock.SetActive(false);

        isAttached = false;
        isFreezing = false;
    }

    void Update()
    {
        transform.position = target.transform.position;

        if (!isAttached && !isFreezing)
        {
            if (null != target.frostDebuff)
            {
                target.frostDebuff = null;
            }

            target = null;
            gameObject.SetActive(false);
        }
        else if (isAttached && !isFreezing)
        {
            CheckForFreeze();
        }
        else if (!isAttached && isFreezing)
        {
            CheckForUnFreeze();
        }
    }

    public void AttachToEnemy(EnemyMovement enemy)
    {
        if (null != target)
        {
            return;
        }

        target = enemy;
        target.frostDebuff = this;
        isAttached = true;
        timeToToggleEffect = Time.time + freezeDelay;
    }

    public void ReleaseEnemy()
    {
        if (null == target)
        {
            return;
        }

        isAttached = false;
        if (isFreezing)
        {
            timeToToggleEffect = Time.time + freezeDuration;
        }
    }

    void CheckForFreeze()
    {
        if (Time.time >= timeToToggleEffect)
        {
            FreezeTarget();
        }
    }

    void CheckForUnFreeze()
    {
        if (Time.time >= timeToToggleEffect)
        {
            UnFreezeTarget();
        }
    }

    void FreezeTarget()
    {
        isFreezing = true;
        target.Freeze();

        mist.SetActive(false);
        iceBlock.SetActive(true);
    }

    void UnFreezeTarget()
    {
        isFreezing = false;
        target.UnFreeze();
    }
}
