using Frogger;
using UnityEngine;

namespace Core
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private HomeManager homeManager;
        [SerializeField] private SfxManager sfxManager;
        [SerializeField] private GameController gameController;
        [SerializeField] private Player player;
        [SerializeField] private Ticker ticker;
        
        private int _score;
        
        private void OnEnable()
        {
            homeManager.OnAllHomesCleared += AllHomesCleared;
            homeManager.OnHomeCleared += HomeOccupied;
            player.OnAdvancedRow += OnAdvancedRow;
        }
        
        private void Awake()
        {
            _score = 0;
            uiManager.UpdateScoreText(_score);
        }

        private void AllHomesCleared()
        {
           IncrementScore(1000);
           sfxManager.PlaySound(SfxManager.SfxType.Win);
           gameController.NewLevel();
        }
        
        private void HomeOccupied(Home home)
        {
            var remainingTime = ticker.GetTimeLeft();
            var bonusPoints = remainingTime * 20;
            IncrementScore(bonusPoints + 50);
            sfxManager.PlaySound(SfxManager.SfxType.Home);
        }

        private void OnAdvancedRow()
        {
            IncrementScore(10);
        }
        
        public void IncrementScore(int amount)
        {
            _score += amount;
            uiManager.UpdateScoreText(_score);
        }
        
        private void OnDisable()
        {
            homeManager.OnAllHomesCleared -= AllHomesCleared;
            homeManager.OnHomeCleared -= HomeOccupied;
            player.OnAdvancedRow -= OnAdvancedRow;
        }
    }
}