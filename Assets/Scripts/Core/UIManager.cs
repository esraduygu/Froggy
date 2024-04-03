using TMPro;
using UnityEngine;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        
        public void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}