using UnityEngine;

public class CharacterSpotlight : MonoBehaviour
{
    [SerializeField]
    GameObject spotlightPC;
    [SerializeField]
    GameObject spotlightMobile;

    void Awake()
    {
        spotlightPC.SetActive(true);
    }

    void Update()
    {
        if (null != GameManager.instance.player)
        {
            gameObject.SetActive(false);
        }
    }
}
