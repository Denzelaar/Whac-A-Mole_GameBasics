using System;
using System.Collections.Generic;
using UnityEngine;
using WhacAMole.Score;
using WhacAMole.Screens;
using WhacAMole.UI;
using WhacAMole.Utillity;

namespace WhacAMole.Controllers
{
    public class ScoreController : MonoBehaviour
    {
        public static Action<int> HighScoreUpdated;
        public static Action<ScoreCollection> ScoresUpdated;
        public static Action<int> FinalScore;
        public ScoreCollection ScoreCollection => scoreCollection;

        [SerializeField] private HoleController holeController;

        private ScoreCollection scoreCollection;

        private int currentScore = 0;
        private GameModes currentGameMode = GameModes.None;

        // Start is called before the first frame update
        void Awake()
        {
            GameController.GameStarted += OnGameStart;
            EndScreen.SaveHighScoreAction += SaveHighScore;
            GameTimer.GameEnded += OnGameEnded;
            LoadData();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < holeController.Holes.Count; i++)
            {
                holeController.Holes[i].Hit -= OnMoleHit;
            }

            GameController.GameStarted -= OnGameStart;
            EndScreen.SaveHighScoreAction -= SaveHighScore;
            GameTimer.GameEnded -= OnGameEnded;
        }

        /// <summary>
        /// Event handler for game end.
        /// </summary>
        private void OnGameEnded()
        {
            FinalScore?.Invoke(currentScore);
            Debug.Log($"Game Ended with a score of: {currentScore}");
        }

        /// <summary>
        /// Event handler for game start.
        /// </summary>
        private void OnGameStart(GameModes newGameMode)
        {
            currentScore = 0;
            currentGameMode = newGameMode;
            HighScoreUpdated?.Invoke(currentScore);
            SubscribeToHoles();
        }

        /// <summary>
        /// Subscribe to mole hit events
        /// </summary>
        private void SubscribeToHoles()
        {
            for (int i = 0; i < holeController.Holes.Count; i++)
            {
                holeController.Holes[i].Hit += OnMoleHit;
            }
        }

        /// <summary>
        /// Event handler for mole hit
        /// </summary>
        private void OnMoleHit()
        {
            currentScore++;
            HighScoreUpdated?.Invoke(currentScore);
        }

        /// <summary>
        /// Saves high score.
        /// </summary>
        /// <param name="name">The name with which the highscore should be saved.</param>
        public void SaveHighScore(string name)
        {
            //Adds new score to the correct game mode highscore list.
            scoreCollection.scoreList[((int)currentGameMode) - 1].Scores.Add(new ScoreObject(currentScore, name));
            scoreCollection = SaveAndLoadData.SortScores(scoreCollection);
            SaveAndLoadData.SaveData(scoreCollection);
            ScoresUpdated?.Invoke(scoreCollection);
        }

        /// <summary>
        /// Load score data from file
        /// </summary>
        private void LoadData()
        {
            scoreCollection = SaveAndLoadData.LoadData();
            if (scoreCollection == null)
            {
                scoreCollection = new ScoreCollection();
                scoreCollection.scoreList = new List<ScoreList>();
                for (int i = 1; i < Enum.GetNames(typeof(GameModes)).Length; i++)
                {
                    ScoreList scoreList = new ScoreList();
                    scoreList.GameMode = ((GameModes)i).ToString();
                    scoreList.Scores = new List<ScoreObject>();
                    scoreCollection.scoreList.Add(scoreList);
                }
            }

            ScoresUpdated?.Invoke(scoreCollection);
        }
    }
}