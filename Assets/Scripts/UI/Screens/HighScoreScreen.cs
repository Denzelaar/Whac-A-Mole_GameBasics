using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WhacAMole.Controllers;
using WhacAMole.Interfaces;
using WhacAMole.Score;

namespace WhacAMole.Screens
{
    public class HighScoreScreen : MonoBehaviour, IMenuScreen
    {
        public bool IsActive => content.activeSelf;

        [SerializeField] private ScrollRect highScoreScroll;
        [SerializeField] private GameObject scorePrefab;
        [SerializeField] private GameObject content;
        [SerializeField] private List<TextMeshProUGUI> scoresModesButtonTexts = new List<TextMeshProUGUI>();
        [SerializeField] private ScoreController scoreController;
        [SerializeField] private GameObject noHighScores;

        private List<GameObject> scoreGameObjects = new List<GameObject>();
        private GameModes currentShowingGameMode = GameModes.None;
        private Color scoresModeStartColor;

        private void Start()
        {
            scoresModeStartColor = scoresModesButtonTexts[0].color;
        }

        public void Show()
        {
            content.SetActive(true);
            ScoreButtonClicked(1);
        }

        public void Hide()
        {
            content.SetActive(false);
        }

        private void DestroyScoreObjects()
        {
            for (int i = scoreGameObjects.Count - 1; i >= 0; i--)
            {
                Destroy(scoreGameObjects[i]);
            }
        }

        /// <summary>
        /// Creates the score objects.
        /// </summary>
        /// <param name="scores"></param>
        private void CreateScoreItems(List<ScoreObject> scores)
        {
            for (int i = 0; i < scores.Count; i++)
            {
                ScoreDecorator newScoreObject = Instantiate(scorePrefab, highScoreScroll.content).GetComponent<ScoreDecorator>();
                newScoreObject.Decorate(scores[i]);
                scoreGameObjects.Add(newScoreObject.gameObject);
            }
        }

        /// <summary>
        /// Set the right score objects and changes the selected highscore modes button.
        /// </summary>
        /// <param name="gameMode"></param>
        public void ScoreButtonClicked(int gameMode)
        {
            if (gameMode == (int)currentShowingGameMode)
            {
                return;
            }

            DestroyScoreObjects();

            if (scoreController.ScoreCollection.scoreList[gameMode - 1].Scores.Count == 0)
            {
                noHighScores.SetActive(true);
            }
            else
            {
                noHighScores.SetActive(false);
                CreateScoreItems(scoreController.ScoreCollection.scoreList[gameMode - 1].Scores);
            }

            if (currentShowingGameMode != GameModes.None)
            {
                scoresModesButtonTexts[((int)currentShowingGameMode) - 1].color = scoresModeStartColor;
            }

            currentShowingGameMode = (GameModes)gameMode;
            scoresModesButtonTexts[gameMode - 1].color = Color.red;
        }
    }
}