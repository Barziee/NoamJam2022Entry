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

        public IEnumerator CountdownTimerCoroutine(int seconds)
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
        }

        public virtual void Init(int seconds)
        {

                StartCoroutine(CountdownTimerCoroutine(seconds));
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
                }
                
                livesText.text = lives.ToString();
        }
}
