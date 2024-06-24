using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace WhacAMole.UI
{
    public class GameTimer : MonoBehaviour
    {
        public static Action GameEnded;
        public bool IsGameOngoing { get; private set; }

        [SerializeField] private TextMeshProUGUI timerText;
        private Stopwatch stopwatch;
        private float gameTimeTotal;

        /// <summary>
        /// Starts the game timer with the specified duration.
        /// </summary>
        /// <param name="gameTime">How long should the timer run.</param>
        public void StartTimer(float gameTime)
        {
            stopwatch = new Stopwatch();
            gameTimeTotal = gameTime;
            stopwatch.Start();
            IsGameOngoing = true;
        }

        /// <summary>
        /// Stops the game timer and releases resources by setting the stopwatch to null.
        /// </summary>
        private void StopTimer()
        {
            stopwatch.Stop();
            stopwatch = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (stopwatch != null)
            {
                float timeLeft = gameTimeTotal - (float)stopwatch.Elapsed.TotalSeconds;

                if (timeLeft <= 0)
                {
                    GameEnded?.Invoke();
                    StopTimer();
                    IsGameOngoing = false;
                    timerText.text = "Time left: 0"; // Ensure timer text displays 0 when time's up
                }
                else
                {
                    timerText.text = $"Time left: {Mathf.Round(timeLeft)}";
                }
            }
        }
    }
}