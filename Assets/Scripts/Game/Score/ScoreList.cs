using System;
using System.Collections.Generic;
namespace WhacAMole.Score
{
    [Serializable]
    public class ScoreList
    {
        public string GameMode;
        public List<ScoreObject> Scores;
    }
}