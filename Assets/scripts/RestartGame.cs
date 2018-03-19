using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RestartGame : MonoBehaviour
{
    #region Fields
    const string MENU_SCENE_NAME = "menu";


    [SerializeField]
    Button restart;
    [SerializeField]
    Button mainMenu;
    [SerializeField]
    GameObject game;
    #endregion


    #region Unity lifecycle
    void Start () {
        restart.onClick.AddListener(RestartButton_OnClick);
        mainMenu.onClick.AddListener(MainMenuButton_OnClick);
    }
    #endregion


    #region Public methods
    void RestartButton_OnClick()
    {
        game.GetComponent<Game>().ResetGame();
    }


    void MainMenuButton_OnClick()
    {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }
    #endregion
}
