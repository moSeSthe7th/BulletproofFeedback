using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    public Text goldCounterText;
    public Text pointCounterText;

    //public GameObject player;
    public Text highScoreText;
    public Text highScorePoint;
    public Text newHighScoreMade;

    public Text countDown;
    public Text panelPointCounterText;

    public Button restartButton;

    public GameObject gameOverPanel;

    public GameObject NextLevelPanel;

    public Slider levelIndicateSlider;

    public Button secondChance;

    public Image advertiseTimer;

    public Text skip;
    public Button skipButton;

    public static int highScore;

    public Slider energySlider;

    private AudioSource audioSource;

    public Color normalGunColor;

    private AdsService ads;

    public Text gainedPoints;
    static bool skipPressed;

    void Start()
    {
        if (DataScript.isGameModeEndless == 1)
        {
            highScore = PlayerPrefs.GetInt("highScore", highScore);
        }
        else
        {
            highScore = PlayerPrefs.GetInt("LevelHighScore", highScore);
        }

        ads = FindObjectOfType(typeof(AdsService)) as AdsService;

        highScorePoint.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        pointCounterText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        secondChance.gameObject.SetActive(false);
        advertiseTimer.gameObject.SetActive(false);
        countDown.gameObject.SetActive(false);
        skip.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        NextLevelPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        skipPressed = false;
    }

    public void PlayHitSound()
    {
        audioSource.Play();
    }

    public void setPointText()
    {
        pointCounterText.text = GameConst.instance.points.ToString();
        panelPointCounterText.text = GameConst.instance.points.ToString();
    }

    public IEnumerator SetGainedPointText(int point)
    {
        gainedPoints.text = "+" + point.ToString();
        while (gainedPoints.color.a < 1)
        {
            gainedPoints.color = new Color(gainedPoints.color.r, gainedPoints.color.g, gainedPoints.color.b, gainedPoints.color.a + 0.25f);
            yield return new WaitForSeconds(.02f);
        }
        yield return new WaitForSeconds(.2f);
        while (gainedPoints.color.a > 0)
        {
            gainedPoints.color = new Color(gainedPoints.color.r, gainedPoints.color.g, gainedPoints.color.b, gainedPoints.color.a - 0.25f);
            yield return new WaitForSeconds(.02f);
        }
        StopCoroutine(SetGainedPointText(point));
    }

    public void DestroyAdCanvas()
    {
        secondChance.gameObject.GetComponent<Image>().enabled = false;
        secondChance.transform.GetChild(0).gameObject.SetActive(false);
        secondChance.transform.GetChild(1).gameObject.SetActive(false);
        secondChance.interactable = false;
        advertiseTimer.gameObject.SetActive(false);
        skip.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
    }

    public IEnumerator AdvertiseReward()
    {
        Debug.LogError("Advertise or Game over");
        Player.instance.StopAll();
        var main = GameObject.FindWithTag("WarpMode").GetComponent<ParticleScript>().warpMode.main;
        main.startSpeed = 0;
        GameObject.FindWithTag("Explode").GetComponent<ExplosionParticleScript>().PlayExplosion(Player.instance.transform.position);
        yield return new WaitForSecondsRealtime(.1f);
        Player.instance.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(.65f);
        if (GameConst.instance.secondPushed || (GameConst.instance.gameTime < 15f || GameConst.instance.points < (PlayerPrefs.GetInt("highScore", highScore) / 10f)))
        {
            GameOver();
        }
        else
        {
            float timer = Time.realtimeSinceStartup;
            //Time.timeScale = 0;
            secondChance.gameObject.SetActive(true);
            advertiseTimer.gameObject.SetActive(true);
            bool didClosed = false;
            while (timer + 5 > Time.realtimeSinceStartup)
            {
                if (!didClosed && timer + .7f < Time.realtimeSinceStartup && !GameConst.instance.secondPushed)
                {
                    didClosed = true;
                    skip.gameObject.SetActive(true);
                    skipButton.gameObject.SetActive(true);
                    while (skip.color.a < 1)
                    {
                        skip.color = new Color(skip.color.r, skip.color.g, skip.color.b, skip.color.a + 0.50f);
                        yield return new WaitForSecondsRealtime(.05f);
                    }
                }
                if (skipPressed && skipButton.gameObject.activeInHierarchy && !GameConst.instance.secondPushed)
                {
                    DestroyAdCanvas();
                    yield return new WaitForSecondsRealtime(.2f);
                    if (!GameConst.instance.secondPushed)
                    {
                        GameOver();
                    }
                    StopCoroutine(AdvertiseReward());
                    yield break;
                }
                yield return null;
            }
            DestroyAdCanvas();
            if (!GameConst.instance.secondPushed)
            {
                GameOver();
            }
        }
        StopCoroutine(AdvertiseReward());
    }

    public void SkipPressed()
    {
        skipPressed = true;
    }

    public void NextLevel()
    {
		if (GameConst.instance.points > highScore) {
			PlayerPrefs.SetInt ("LevelHighScore", GameConst.instance.points);
			highScore = GameConst.instance.points;
		}
        NextLevelPanel.SetActive(true);
    }

    public void GameOver()
    {
        Debug.LogError("game over");
        GameConst.instance.isGameOn = false;

        secondChance.gameObject.SetActive(false);

        //Time.timeScale = 0;

        if (DataScript.isGameModeEndless == 0)
        {
            DataScript.isSessionEnded = true;
            DataScript.pointHolder = 0;
        }

        gameOverPanel.SetActive(true);

        StopAllCoroutines();

        GameConst.playedTime++;
        PlayerPrefs.SetInt("PlayedTime", GameConst.playedTime);

        if (!DataScript.isAdsPurchased)
        {
            if (PlayerPrefs.GetInt("PlayedTime") % 5 == 0 && !GameConst.instance.secondPushed)
            {
                ads.ShowAd();
            }
        }
        setGoldText();
        goldCounterText.gameObject.SetActive(true);

        if (DataScript.isGameModeEndless == 1)
        {
            if (GameConst.instance.points > highScore)
            {
                PlayerPrefs.SetInt("highScore", GameConst.instance.points);
                highScore = GameConst.instance.points;
                highScorePoint.gameObject.SetActive(false);
                highScoreText.gameObject.SetActive(false);
                newHighScoreMade.gameObject.SetActive(true);

            }
            else
            {
                highScore = PlayerPrefs.GetInt("highScore", highScore);
                highScoreText.text = "High Score";
                highScorePoint.text = highScore.ToString();
                highScorePoint.gameObject.SetActive(true);
                highScoreText.gameObject.SetActive(true);
                newHighScoreMade.gameObject.SetActive(false);
            }
        }
        else
        {
            if (GameConst.instance.points > highScore)
            {
                PlayerPrefs.SetInt("LevelHighScore", GameConst.instance.points);
                highScore = GameConst.instance.points;
                highScorePoint.gameObject.SetActive(false);
                highScoreText.gameObject.SetActive(false);
                newHighScoreMade.gameObject.SetActive(true);

            }
            else
            {
                highScore = PlayerPrefs.GetInt("LevelHighScore", highScore);
                highScoreText.text = "High Score";
                highScorePoint.text = highScore.ToString();
                highScorePoint.gameObject.SetActive(true);
                highScoreText.gameObject.SetActive(true);
                newHighScoreMade.gameObject.SetActive(false);
            }
        }
        pointCounterText.gameObject.SetActive(false);

        energySlider.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void startGame()
    {
		if (DataScript.isGameModeEndless == 0) {
		
			levelIndicateSlider.gameObject.SetActive(true);
		}
       
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        //energySlider.gameObject.SetActive(true);
        pointCounterText.gameObject.SetActive(true);
        setPointText();
        goldCounterText.gameObject.SetActive(false);

    }

    public void setGoldText()
    {
        goldCounterText.text = PlayerPrefs.GetInt("Gold", 0).ToString();
    }
}

