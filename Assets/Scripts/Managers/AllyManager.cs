using UnityEngine;
using UnityEngine.UI;

public class AllyManager : MonoBehaviour
{
    [SerializeField]
    int allyCost;
    [SerializeField]
    GameObject allyPrefab;
    [SerializeField]
    Transform allySpawnPoint;
    [SerializeField]
    Image allyImage;

    Ally ally;
    int allyPoints;

    void Awake()
    {
        GameObject obj = (GameObject)Instantiate(allyPrefab);
        obj.transform.parent = transform;
        ally = obj.GetComponent<Ally>();

        obj.SetActive(false);

        if (null != allyImage)
        {
            allyImage.enabled = false;
        }
    }

    public void AddPoints(int amount)
    {
        allyPoints += amount;

        if (null != allyImage && CanSummonAlly())
        {
            allyImage.enabled = true;
        }
    }

    public bool CanSummonAlly()
    {
        return (allyPoints >= allyCost) && !ally.gameObject.activeSelf && (null != GameManager.instance.enemyTarget);
    }

    public Ally SummonAlly()
    {
        if (!CanSummonAlly())
        {
            return null;
        }

        ally.transform.position = allySpawnPoint.position;
        ally.transform.rotation = allySpawnPoint.rotation;

        ally.gameObject.SetActive(true);
        ally.Move(GameManager.instance.enemyTarget.position);

        if (null != allyImage)
        {
            allyImage.enabled = false;
        }

        return ally;
    }

    public void UnSummonAlly()
    {
        allyPoints = 0;
        ally.gameObject.SetActive(false);
    }
}
