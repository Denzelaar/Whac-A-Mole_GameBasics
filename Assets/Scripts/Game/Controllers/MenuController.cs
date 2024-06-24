using UnityEngine;
using WhacAMole.Screens;

namespace WhacAMole.Controllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private StartScreen startScreen;
        [SerializeField] private HighScoreScreen highScoreScreen;
        [SerializeField] private GameController gameScreen;
        [SerializeField] private GameSettings settingsScreen;

        /// <summary>
        /// Starts the game after validating the input.
        /// </summary>
        public void StartGame()
        {
            if (settingsScreen.ValidateInput() != -1)
            {
                gameScreen.StartGame(settingsScreen.ValidateInput(), (GameModes)settingsScreen.GetSelectedTimer());
                settingsScreen.Hide();
            }
            else
            {
                Debug.LogError("Invalid input given.");
            }
        }

        /// <summary>
        /// Toggles the active state of the settings screen.
        /// </summary>
        public void ToggleSettingsScreen()
        {
            if (settingsScreen.IsActive)
            {
                settingsScreen.Hide();
                return;
            }

            settingsScreen.Show();
        }

        /// <summary>
        /// Toggles the active state of the startscreen.
        /// </summary>
        public void ToggleStartScreen()
        {
            if (startScreen.IsActive)
            {
                startScreen.Hide();
                return;
            }

            startScreen.Show();
        }

        /// <summary>
        /// Toggles the active state of the high score screen.
        /// </summary>
        public void ToggleHighScoreScreen()
        {
            if (highScoreScreen.IsActive)
            {
                highScoreScreen.Hide();
                return;
            }

            highScoreScreen.Show();
        }

        /// <summary>
        /// Toggles the active state of the game screen.
        /// </summary>
        public void ToggleGameScreen()
        {
            if (gameScreen.IsActive)
            {
                gameScreen.Hide();
                return;
            }

            gameScreen.Show();
        }
    }
}