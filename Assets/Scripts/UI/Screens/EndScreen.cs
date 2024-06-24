using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WhacAMole.Controllers;
using WhacAMole.Score;
using WhacAMole.UI;

namespace WhacAMole.Screens
{
    public class EndScreen : MonoBehaviour
    {
        public static Action<string> SaveHighScoreAction;

        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject highscoreScreen;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button saveButton;
        [SerializeField] private TextMeshProUGUI playerScoreText;
        public ScoreList gg;
        private TextMeshProUGUI inputFieldPlaceHolder;
        private const string scoreTextString = "Your Score is: ";

        private void Start()
        {
            GameTimer.GameEnded += ToggleGameOverScreen;
            GameController.GameStarted += Reset;
            ScoreController.FinalScore += OnFinalScore;
            inputFieldPlaceHolder = inputField.placeholder.GetComponent<TextMeshProUGUI>();
        }

        private void OnDestroy()
        {
            GameTimer.GameEnded -= ToggleGameOverScreen;
            GameController.GameStarted -= Reset;
            ScoreController.FinalScore -= OnFinalScore;
        }

        /// <summary>
        /// Toggles the active state of the game over screen.
        /// </summary>
        public void ToggleGameOverScreen()
        {
            gameOverScreen.SetActive(!gameOverScreen.activeSelf);
        }

        /// <summary>
        /// Toggles the active state of the game over screen.
        /// </summary>
        public void ToggleHighscoreScreen()
        {
            highscoreScreen.SetActive(!highscoreScreen.activeSelf);
        }

        /// <summary>
        /// Toggles the active state of the Save HighScore screen.
        /// </summary>
        public void SaveHighScore()
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                SetPlaceHolderEmptyError();
                return;
            }

            SaveHighScoreAction?.Invoke(inputField.text);
            saveButton.interactable = false;
            inputField.interactable = false;
        }

        /// <summary>
        /// Sets the inputfield placeholder text to red.
        /// </summary>
        private void SetPlaceHolderEmptyError()
        {
            inputFieldPlaceHolder.color = Color.red;
        }

        /// <summary>
        /// Resets UI elements to their default states.
        /// </summary>
        private void Reset(GameModes gameModes)
        {
            saveButton.interactable = true;
            inputField.interactable = true;
            inputFieldPlaceHolder.color = Color.black;
            inputField.text = string.Empty;
        }

        /// <summary>
        /// Updates the UI to display the player's final score.
        /// </summary>
        /// <param name="score"></param>
        private void OnFinalScore(int score)
        {
            playerScoreText.text = scoreTextString + score;
        }
    }
}