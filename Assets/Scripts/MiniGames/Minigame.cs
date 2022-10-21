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
                Destroy(gameObject);
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
                
                yield return new WaitForSeconds(oneSecond);
                // play sound
                
                countdownText.gameObject.SetActive(false);
                onComplete?.Invoke();
        }

        public virtual void Init(int seconds, Action onComplete = null)
        {
                StartCoroutine(CountdownTimerCoroutine(seconds, onComplete));
        }

        public void IncreaseScore(int amount)
        {
                score += amount;
                scoreText.text = score.ToString();
        }

        public void ReduceLife()
        {
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
                
        }
}
