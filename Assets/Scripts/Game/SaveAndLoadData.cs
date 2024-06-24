using System.IO;
using System.Linq;
using UnityEngine;
using WhacAMole.Score;

namespace WhacAMole.Utillity
{
    public static class SaveAndLoadData
    {
        private static string filePath = Application.persistentDataPath + "/HighScores.data";

        /// <summary>
        /// Loads score data from a file
        /// </summary>
        /// <returns><see cref="ScoreCollection"/> object with the score that was stored.</returns>
        public static ScoreCollection LoadData()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    // Read all text from the file
                    string fileContents = File.ReadAllText(filePath);
                    // Deserialize the JSON data into a ScoreCollection object
                    ScoreCollection scoreCollection = JsonUtility.FromJson<ScoreCollection>(fileContents);
                    // Sort the scores in descending order by HighScore
                    return SortScores(scoreCollection);
                }
                catch (IOException ex)
                {
                    Debug.LogError($"Error reading file: {ex.Message}");
                    return null;
                }
            }
            // Return null if the file doesn't exist
            return null;
        }

        /// <summary>
        /// Saves a ScoreCollection object to a file
        /// </summary>
        /// <param name="scores">The scores that should be saved.</param>
        public static void SaveData(ScoreCollection scores)
        {
            try
            {
                // Serialize the ScoreCollection object into JSON format
                string jsonString = JsonUtility.ToJson(scores);

                // Write the JSON string to the file at the specified path
                File.WriteAllText(filePath, jsonString);
            }
            catch (IOException ex)
            {
                Debug.LogError($"Error writing to file: {ex.Message}");
            }
        }

        /// <summary>
        /// Sorts the scores in a ScoreCollection in descending order by HighScore.
        /// </summary>
        /// <param name="scoreCollection">The collection that should be sorted.</param>
        /// <returns>The sorted <see cref="ScoreCollection"/></returns>
        public static ScoreCollection SortScores(ScoreCollection scoreCollection)
        {
            if (scoreCollection != null)
            {
                for (int i = 0; i < scoreCollection.scoreList.Count; i++)
                {
                    // Sort the Scores list in descending order based on HighScore
                    scoreCollection.scoreList[i].Scores = scoreCollection.scoreList[i].Scores.OrderByDescending(score => score.HighScore).ToList();
                }
            }
            return scoreCollection;
        }
    }
}