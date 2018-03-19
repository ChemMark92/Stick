using UnityEngine;


public class Game : MonoBehaviour
{
    #region Fields
    const float START_CHARACTER_POSITION = -1.6f;


    [SerializeField]
    GameObject oldStick, stick, rightPlatform, leftPlatform, hiddenPlatform, character, resetter, scoreSetter;
    [SerializeField]
    GameObject platform;


    bool isMoveStarted = false;
    bool isMoveGoing = false;
    #endregion


    #region Properties
    public GameObject RightPlatform
    {
        get
        {
            return rightPlatform;
        }
    }


    bool IsVisibleObjectsShifted
    {
        get
        {
            bool isShifted = true;
            if (GetCharacterPosition() > START_CHARACTER_POSITION)
            {
                StepShifting();
                isShifted = false;
            }
            return isShifted;
        }
    }


    bool IsHiddenPlatformShown
    {
        get
        {
            bool isShifted = true;
            if (GetHiddenPlatformPosition() > GetHiddenPlatformFinalPosition())
            {
                hiddenPlatform.GetComponent<Platform>().StepToLeft(-0.05f);
                isShifted = false;
            }
            return isShifted;
        }
    }
    #endregion


    #region Unity lifecycle
    void Start () {
        resetter.SetActive(false);
        CreateLeftPlatform();
        CreateRightPlatform();
        CreateHiddenPlatform();
    }


    void Update () {
        CheckTap();
        
        if (isMoveStarted && !stick.GetComponent<Stick>().IsReady)
        {
            stick.GetComponent<Stick>().StretchStick();
        }
        else if (isMoveGoing)
        {
            Move();
        }
    }
    #endregion


    #region Public methods
    public void ShiftObjects()
    {
        bool isVisibleObjectsReady = IsVisibleObjectsShifted;
        bool isHiddenPlatformReady = IsHiddenPlatformShown;
        if (isVisibleObjectsReady && isHiddenPlatformReady)
        {
            ResetRun();
        }
    }
    

    public void ResetGame()
    {
        ResetRun();
        resetter.SetActive(false);
        MovePlatformsToStart();
        MoveCharacterToStart();
        ResetScore();
    }
    #endregion


    #region Private methods
    void ResetRun()
    {
        isMoveGoing = false;
        ResetStick();
        ResetPlatforms();
        ResetCharacter();
    }
    #endregion


    #region Private methods
    void CreateLeftPlatform()
    {
        leftPlatform = Instantiate(platform);
        leftPlatform.GetComponent<Platform>().HideRedDot();
    }


    void CreateRightPlatform()
    {
        rightPlatform = Instantiate(platform);
        rightPlatform.GetComponent<Platform>().SetRightPosition(leftPlatform);
    }


    void CreateHiddenPlatform()
    {
        hiddenPlatform = Instantiate(platform);
        hiddenPlatform.GetComponent<Platform>().SetHiddenPosition();
    }


    void StartMoving()
    {
        isMoveStarted = false;
        isMoveGoing = true;
        stick.SetActive(true);
    }


    void FinishMoving()
    {
        stick.GetComponent<Stick>().IsRotating = true;
        if (!character.GetComponent<Character>().IsFalling)
        {
            ShiftObjects();
        }
        else
        {
            character.GetComponent<Character>().Fall();
            stick.GetComponent<Stick>().Fall();
        }
    }


    void Move()
    {
        stick.GetComponent<Stick>().Rotate();
        if (!stick.GetComponent<Stick>().IsRotating)
        {
            if (character.GetComponent<Character>().IsFinished)
            {

                FinishMoving();
            }
            else
            {
                character.GetComponent<Character>().Move();

            }
        }
    }


    void CheckTap()
    {
        if (Input.GetMouseButtonDown(0) && character.GetComponent<Character>().IsReady && !isMoveStarted && !isMoveGoing)
        {
            isMoveStarted = true;
        }
        if (Input.GetMouseButtonUp(0) && character.GetComponent<Character>().IsReady && isMoveStarted && !isMoveGoing)
        {
            StartMoving(); 
        }
    }
    

    void StepShifting()
    {
        leftPlatform.GetComponent<Platform>().StepToLeft(-0.1f);
        rightPlatform.GetComponent<Platform>().StepToLeft(-0.1f);
        stick.GetComponent<Stick>().StepToLeft(-0.1f);
        character.GetComponent<Character>().StepToLeft(-0.1f);
    }
    

    void MovePlatformsToStart()
    {
        leftPlatform.GetComponent<Platform>().SetLeftPosition();
        rightPlatform.GetComponent<Platform>().SetRightPosition(leftPlatform);
        hiddenPlatform.GetComponent<Platform>().SetHiddenPosition();
    }


    void MoveCharacterToStart()
    {
        character.GetComponent<Character>().Initialize();
        character.GetComponent<Character>().IsReady = true;
    }


    void ResetCharacter()
    {
        character.GetComponent<Character>().IsFalling = false;
        character.GetComponent<Character>().IsFinished = false;
        character.GetComponent<Character>().IsStickNotEnded = true;
        character.GetComponent<Character>().TravaledDistance = 0f;
    }


    void ResetScore()
    {
        scoreSetter.GetComponent<ScoreSetter>().ResetScore();
    }


    void ResetStick()
    {
        GameObject tempStick = oldStick;
        oldStick = stick;
        stick = tempStick;
        stick.transform.localPosition = new Vector3(-1.3f, -2.2f, 0f);
        stick.transform.localScale = new Vector3(0.05f, 0.0f, 1f);
        stick.transform.localEulerAngles = new Vector3(0, 0, 0);
        stick.GetComponent<Stick>().IsReady = false;
        Distances.StickLength = 0.0f;
    }


    void ResetPlatforms()
    {
        GameObject tempPlatform = leftPlatform;
        leftPlatform = rightPlatform;
        leftPlatform.GetComponent<Platform>().HideRedDot();
        rightPlatform = hiddenPlatform;
        rightPlatform.GetComponent<Platform>().ShowRedDot();
        rightPlatform.GetComponent<Platform>().SetDistances(leftPlatform);
        hiddenPlatform = tempPlatform;
        hiddenPlatform.GetComponent<Platform>().SetHiddenPosition();
        hiddenPlatform.GetComponent<Platform>().ShowRedDot();
    }


    float GetCharacterPosition()
    {
        return FloatRounder.RoundToCent(character.GetComponent<Character>().Position);
    }


    float GetHiddenPlatformPosition()
    {
        return FloatRounder.RoundToCent(hiddenPlatform.GetComponent<Platform>().Position);
    }


    float GetHiddenPlatformFinalPosition()
    {
        return FloatRounder.RoundToCent(hiddenPlatform.GetComponent<Platform>().PositionX);
    }
    #endregion
}
