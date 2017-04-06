using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HeaderAttribute("Player and Enemy Properties")]
    public PlayerHealth player;
    public Transform enemyTarget;

    [SerializeField]
    float delayOnPlayerDeath = 1f;

    [HeaderAttribute("UI Properties")]
    [SerializeField]
    Text infoText;
    [SerializeField]
    Animator gameOverAnimator;

    [HeaderAttribute("Player Selection Properties")]
    [SerializeField]
    GameObject enemySpawners;
    [SerializeField]
    Animator cameraAnimator;

    [HeaderAttribute("Ally Properties")]
    [SerializeField]
    AllyManager allyManager;

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

    public void PlayerChosen(PlayerHealth selected)
    {
        player = selected;
        enemyTarget = player.transform;

        if (null != infoText)
        {
            infoText.text = "Score: 0";
        }

        if (null != enemySpawners)
        {
            enemySpawners.SetActive(true);
        }

        if (null != cameraAnimator)
        {
            cameraAnimator.SetTrigger("Start");
        }
    }

    public void PlayerDied()
    {
        enemyTarget = null;

        GameOver();
    }

    public void AddScore(int points)
    {
        score += points;

        if (null != infoText)
        {
            infoText.text = "Score: " + score;
        }

        if (null != allyManager)
        {
            allyManager.AddPoints(points);
        }
    }

    public void SummonAlly()
    {
        if (null == allyManager)
        {
            return;
        }

        Ally ally = allyManager.SummonAlly();

        if (null != ally)
        {
            enemyTarget = ally.transform;
            Invoke("UnSummonAlly", ally.duration);
        }
    }

    void UnSummonAlly()
    {
        enemyTarget = player.transform;
        allyManager.UnSummonAlly();
    }

    public void PlayerDeathComplete()
    {
        Invoke("ReloadScene", delayOnPlayerDeath);
    }

    void GameOver()
    {
        if (null != gameOverAnimator)
        {
            gameOverAnimator.SetTrigger("GameOver");
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
