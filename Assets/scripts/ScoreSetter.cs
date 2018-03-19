using UnityEngine;
using UnityEngine.UI;


public class ScoreSetter : MonoBehaviour
{
    #region Fields
    const int START_SCORE = 0;
    const string BEST_SCORE_NAME = "bestScore";

    [SerializeField]
    Text scoreText;
    [SerializeField]
    GameObject Game;


    int score;
    #endregion


    #region Unity lifecycle
    void Start()
    {
        score = 0;
    }
    #endregion


    #region Public methods
    public void DoubleScore()
    {
        score++;
        Game.GetComponent<Game>().RightPlatform.GetComponent<Platform>().AnimateExtraScore();
    }


    public void RiseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (PlayerPrefs.GetInt(BEST_SCORE_NAME,START_SCORE) < score)
        {
            PlayerPrefs.SetInt(BEST_SCORE_NAME, score);
        }
    }


    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
        scoreText.gameObject.SetActive(true);
    }
    #endregion
}
