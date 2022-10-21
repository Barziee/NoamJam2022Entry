using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
        private const int oneSecond = 1;
        private string startString = "Start!";
        
        private int score;

        [SerializeField] private Image backgroundImage;
        [SerializeField] private TextMeshPro countdownText;

        public IEnumerator CountdownTimerCoroutine(int seconds)
        {
                countdownText.gameObject.SetActive(true);
                
                countdownText.text = seconds.ToString();
                for (int i = 0; i < seconds; i++)
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
}
