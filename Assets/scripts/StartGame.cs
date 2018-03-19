using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour
{
    #region Fields
    const string ENABLED_SOUND_NAME = "IsSoundEnabled";
    const float MAX_VOLUME = 1f;
    const float PLATFORM_STEP = 0.1f;
    const float FINAL_POSITION = -1.8f;
    const int SOUND_ENABLED = 1;
    const int SOUND_DISABLED = 0;
    const string GAME_SCENE_NAME = "game";

    [SerializeField]
    GameObject platformWithCharacter;
    [SerializeField]
    GameObject background;
    [SerializeField]
    Toggle soundToggler;


    bool isGameStarting;
    Vector3 startPosition;
    Vector3 step;
    #endregion


    #region Unity lifecycle
    void Start () {
        isGameStarting = false;
        startPosition = new Vector3(0, 0, 0);
        step = new Vector3(-PLATFORM_STEP, PLATFORM_STEP, 0);
        SetSound();
    }
	

	void Update () {
        if (isGameStarting && GetPosition(platformWithCharacter) > FINAL_POSITION)
        {
            SetPosition(background,startPosition);
            Move(platformWithCharacter,step);
        }
        else if (platformWithCharacter.transform.localPosition.x <= FINAL_POSITION)
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }
    }
    #endregion


    #region Public methods
    public void StartGameButton_OnClick()
    {
        isGameStarting = true;
    }


    public void SoundButton_OnClick()
    {
        if (!soundToggler.isOn)
        {
            PlayerPrefs.SetInt(ENABLED_SOUND_NAME, SOUND_DISABLED);
            AudioListener.volume = 0f;
        }
        else
        {
            PlayerPrefs.SetInt(ENABLED_SOUND_NAME, SOUND_ENABLED);
            AudioListener.volume = MAX_VOLUME;
        }
    }
    #endregion

    #region Private methods 
    float GetPosition(GameObject unit)
    {
        return unit.transform.localPosition.x;
    }


    void SetPosition(GameObject unit, Vector3 position)
    {
        unit.transform.localPosition = position;
    }


    void Move(GameObject unit, Vector3 position)
    {
        unit.transform.localPosition += position;
    }


    void SetSound()
    {
        if (PlayerPrefs.GetInt(ENABLED_SOUND_NAME, SOUND_ENABLED) == SOUND_DISABLED)
        {
            AudioListener.volume = 0f;
            soundToggler.isOn = false;
        }
        else
        {
            AudioListener.volume = MAX_VOLUME;
            soundToggler.isOn = true;
        }
    }
    #endregion
}
