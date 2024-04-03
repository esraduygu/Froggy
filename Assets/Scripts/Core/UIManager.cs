using TMPro;
using UnityEngine;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;
        
        public void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }

        public void UpdateTimerText(int timer)
        {
            timerText.text = timer.ToString();
        }
    }
}