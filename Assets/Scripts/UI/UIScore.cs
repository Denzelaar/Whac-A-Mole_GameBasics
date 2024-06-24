using TMPro;
using UnityEngine;
using WhacAMole.Controllers;

namespace WhacAMole.UI
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        // Start is called before the first frame update
        void Start()
        {
            ScoreController.HighScoreUpdated += OnScoreUpdated;
        }

        private void OnDestroy()
        {
            ScoreController.HighScoreUpdated -= OnScoreUpdated;
        }

        /// <summary>
        /// Event handler method invoked when the HighScoreUpdated event is triggered.
        /// </summary>
        /// <param name="newScore"></param>
        private void OnScoreUpdated(int newScore)
        {
            scoreText.text = $"Score: {newScore}";
        }

        /// <summary>
        ///  Resets the score display.
        /// </summary>
        private void Reset()
        {
            scoreText.text = $"Score: {0}";
        }
    }
}