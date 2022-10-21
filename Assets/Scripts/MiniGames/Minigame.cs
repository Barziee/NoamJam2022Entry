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
    private int lives = 3;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

        internal void DestroySelf()
        {
                gameObject.SetActive(false);
                DestroyImmediate(gameObject, true);
        }
        
        public IEnumerator CountdownTimerCoroutine(int seconds, Action onComplete = null)
        {
                countdownText.gameObject.SetActive(true);
                
                countdownText.text = seconds.ToString();
                for (int i = 0; i < seconds;)
                { 
                        yield return new WaitForSeconds(oneSecond);
                        seconds--;
                        countdownText.text = seconds.ToString();
                        // play sound
                }

                countdownText.text = startString;
                // play sound
                
                yield return new WaitForSeconds(oneSecond);
                
                
                countdownText.gameObject.SetActive(false);
                onComplete?.Invoke();
        }

    public virtual void Init(int seconds,int playerScore,int playerMiniGameLives, Action onComplete = null)
    {
        if (playerMiniGameLives > 0)
            lives = playerMiniGameLives;

        if (playerScore >= 0)
            score = playerScore;

        StartCoroutine(CountdownTimerCoroutine(seconds, onComplete));
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

            EndGame();
        }

        livesText.text = lives.ToString();
    }

    public virtual void EndGame()
    {
        Debug.Log($"Player Lost MiniGame");
    }
}
