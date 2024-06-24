using TMPro;
using UnityEngine;

namespace WhacAMole.Score
{
    public class ScoreDecorator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI nameText;

        public void Decorate(ScoreObject scoreObject)
        {
            scoreText.text = scoreObject.HighScore.ToString();
            nameText.text = scoreObject.Name.ToString();
        }
    }
}