using System;

namespace WhacAMole.Score
{
    [Serializable]
    public class ScoreObject
    {
        public int HighScore;
        public string Name;

        public ScoreObject(int highScore, string name)
        {
            HighScore = highScore;
            Name = name;
        }
    }
}