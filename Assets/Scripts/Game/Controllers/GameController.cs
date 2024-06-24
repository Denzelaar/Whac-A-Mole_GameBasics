using System;
using System.Collections;
using UnityEngine;
using WhacAMole.Interfaces;
using WhacAMole.UI;

namespace WhacAMole.Controllers
{
    public class GameController : MonoBehaviour, IMenuScreen
    {
        public static Action<GameModes> GameStarted;
        public bool IsActive => gameContent.activeSelf;

        [SerializeField] private HoleController holeController;
        [SerializeField] private GameTimer timer;
        [SerializeField] private GameObject gameContent;

        private Coroutine playGameCoroutine = null;

        private const float moleInterval = 0.5f;
        private const float moleMinimumActiveTime = 1.5f;
        private const float moleActiveSteps = 0.1f;
        private const float moleActiveTimeInitial = 2.5f;
        private float moleActiveTime = moleActiveTimeInitial;

        /// <summary>
        /// Method to start the game with specified mole amount and timer seconds
        /// </summary>
        /// <param name="moleAmount">The amount of moles that should be created.</param>
        /// <param name="timerSeconds">Amount of second the game should be played in/</param>
        public void StartGame(int moleAmount, GameModes SelectedGameMode)
        {
            holeController.PlaceNewHoles(moleAmount);
            timer.StartTimer(GetTimerSecondsByGameMode(SelectedGameMode));
            playGameCoroutine = StartCoroutine(PlayGame());
            GameTimer.GameEnded += EndGame;
            ScoreController.HighScoreUpdated += OnScoreUpdate;
            gameContent.SetActive(true);
            GameStarted?.Invoke(SelectedGameMode);
        }

        public void Show()
        {
            gameContent.SetActive(true);
        }

        public void Hide()
        {
            gameContent.SetActive(false);
        }

        /// <summary>
        /// Method called when the score updates
        /// </summary>
        /// <param name="score">The new score.</param>
        private void OnScoreUpdate(int score)
        {
            // Check if the score is a multiple of 5
            if (score % 5 == 0)
            {
                DecreaseActiveTime();
            }
        }

        /// <summary>
        /// Method to decrease mole active time
        /// </summary>
        private void DecreaseActiveTime()
        {
            if (moleActiveTime > moleMinimumActiveTime)
            {
                moleActiveTime -= moleActiveSteps;
            }
        }

        /// <summary>
        /// Coroutine to play the game loop
        /// </summary>
        private IEnumerator PlayGame()
        {
            while (timer.IsGameOngoing)
            {
                holeController.ActivateNewHole(moleActiveTime);
                yield return new WaitForSeconds(moleInterval);
            }
        }

        /// <summary>
        /// Called when the game ends.
        /// </summary>
        private void EndGame()
        {
            StopCoroutine(playGameCoroutine);
            playGameCoroutine = null;
            holeController.Reset();
            GameTimer.GameEnded -= EndGame;
            ScoreController.HighScoreUpdated -= OnScoreUpdate;
            moleActiveTime = moleActiveTimeInitial;
        }

        /// <summary>
        /// Calculates the timer duration.
        /// </summary>
        private int GetTimerSecondsByGameMode(GameModes gameMode)
        {
            switch (gameMode)
            {
                default:
                case GameModes.None:
                    Debug.LogError("Game mode has not been set correctly.");
                    return 0;
                case GameModes.ThirtySec:
                    return 30;
                case GameModes.SixtySec:
                    return 60;
                case GameModes.NinetySec:
                    return 90;
            }
        }
    }
}