using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HeaderAttribute("Player and Enemy Properties")]
    public PlayerHealth player;
    public Transform enemyTarget;

    [HeaderAttribute("UI Properties")]
    [SerializeField]
    Text infoText;

    int score = 0;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        enemyTarget = player.transform;
    }

    public void PlayerDied()
    {
        enemyTarget = null;
    }

    public void AddScore(int points)
    {
        score += points;

        if (null != infoText)
        {
            infoText.text = "Score: " + score;
        }
    }

    public void PlayerDeathComplete()
    {

    }
}
