using UnityEngine;
using UnityEngine.UI;


public class Stick : UnitObject
{
    #region Fields
    const float START_LENGTH = 0.0f;
    const float WIDTH = 0.05f;
    const float ROTATION_Z = 10f;
    const float MAX_LENGTH = 5f;
    const int MAX_ROTATION = 270;
    const int DOWN_ROTATION = 190;
    const float RISING_SPEED = 0.04f;
    const float ONE = 1f;
    const string BEST_SCORE_NAME = "bestScore";


    [SerializeField]
    Text score;
    [SerializeField]
    Text finalScore;
    [SerializeField]
    Text bestScore;
    [SerializeField]
    GameObject scoreSetter;
    [SerializeField]
    GameObject resetter;
    [SerializeField]
    GameObject pointSound;
  

    Vector3 stickRisingSpeed, stickRotationSpeed;
    AudioSource stickSound, stickFinishSound;
    #endregion


    #region Properties
    public bool IsReady { get; set; }
    public bool IsRotating { get; set; }
    public bool IsStickRotationFinished { get; set; }
    #endregion


    #region Unity lifecycle
    void Start()
    {
        stickRisingSpeed = new Vector3(0, RISING_SPEED, 0);
        stickRotationSpeed = new Vector3(0, 0, ROTATION_Z);
        Distances.StickLength = START_LENGTH;
        IsReady = false;
        IsStickRotationFinished = false;
        IsRotating = true;
        stickSound = GetComponent<AudioSource>();
        stickFinishSound = pointSound.GetComponent<AudioSource>();
    }
    #endregion


    #region Public methods
    public void StretchStick()
    {
        if (transform.localScale.y < MAX_LENGTH)
        {
            IsStickRotationFinished = false;
            PlaySound();
            StepStretching();
        }
        else
        {
            IsRotating = true;
        }
    }

    

    public void Fall()
    {
        if (transform.localEulerAngles.z > DOWN_ROTATION)
        {
            transform.localEulerAngles -= stickRotationSpeed;
        }
        else
        {
            FinishFall();
        }
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void Rotate()
    {
        IsReady = true;
        IsRotating = true;
        stickSound.Stop();
        StepRotating();
    }
    #endregion


    #region Private methods
    void StepStretching()
    {
        Distances.StickLength += RISING_SPEED;
        Distances.CharacterRunOnStickDistance += RISING_SPEED;
        transform.localScale += stickRisingSpeed;
    }


    void StepRotating()
    {
        if (transform.localEulerAngles.z > MAX_ROTATION || transform.localEulerAngles.z == 0)
        {
            transform.localEulerAngles -= stickRotationSpeed;
        }
        else
        {
            IsRotating = false;
            FinishRotating();
        }
    }


    void PlaySound()
    {
        if (!stickSound.isPlaying)
        {
            stickSound.Play();
        }
    }


    void FinishFall()
    {
        finalScore.text = score.text;
        bestScore.text = PlayerPrefs.GetInt(BEST_SCORE_NAME, 0).ToString();
        score.gameObject.SetActive(false);
        resetter.SetActive(true);
        transform.localScale = new Vector3(WIDTH, START_LENGTH, ONE);
    }


    void FinishRotating()
    {
        if (!stickFinishSound.isPlaying && !IsStickRotationFinished)
        {
            IsStickRotationFinished = true;
            stickFinishSound.Play();
            CheckDoubleScore();
        }
    }

    void CheckDoubleScore()
    {
        float centerOfRightPlatform = Distances.Space + Distances.RightPlatformWidth / 2;
        if (Distances.StickLength > centerOfRightPlatform - Distances.DeltaCenter && Distances.StickLength < centerOfRightPlatform + Distances.DeltaCenter)
        {
            scoreSetter.GetComponent<ScoreSetter>().DoubleScore();
        }
    }
    #endregion
}

