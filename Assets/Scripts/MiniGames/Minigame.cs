using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    private const int oneSecond = 1;
    private string startString = "Start!";

    private int score = 0;
    protected int lives = 3;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

    Action<Minigame, bool> onGameEnded;

    public virtual void CloseSelf()
    {
        gameObject.SetActive(false);
    }

    public virtual int GetMiniGameType()
    {
        return 0;
    }

    public IEnumerator CountdownTimerCoroutine(int timerCountdownSeconds, Action onTimerComplete = null)
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = timerCountdownSeconds.ToString();
        for (int i = 0; i < timerCountdownSeconds;)
        {
            yield return new WaitForSeconds(oneSecond);
            timerCountdownSeconds--;
            countdownText.text = timerCountdownSeconds.ToString();
            // play sound
        }

        countdownText.text = startString;
        // play sound

        yield return new WaitForSeconds(oneSecond);


        countdownText.gameObject.SetActive(false);
        onTimerComplete?.Invoke();
    }

    public virtual void Init(int timerCountdownSeconds, int playerScore, int playerMiniGameLives,
        Action onTimerDone = null, Action<Minigame, bool> onMiniGameEnded = null)
    {
        onGameEnded = onMiniGameEnded;
        if (playerMiniGameLives > 0)
            lives = playerMiniGameLives;
        
        if (playerScore >= 0)
            score = playerScore;

        scoreText.text = score.ToString();
        livesText.text = lives.ToString();


        StartCoroutine(CountdownTimerCoroutine(timerCountdownSeconds, onTimerDone));
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
        Debug.Log($"Player score increased by {amount}, its now {score}");
    }

    public void ReduceLife()
    {
        Debug.Log($"Player Lives Reduced By 1");
        lives--;
        if (lives == 0)
        {
            // minigame lost
            Debug.Log($"Player Lost MiniGame");
            EndGame();
        }

        livesText.text = lives.ToString();
    }

    public virtual void EndGame()
    {
        onGameEnded?.Invoke(this, lives>0);
    }
}
