using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : UnitObject
{
    #region Fields
    const float MIN_POSITION_X = -0.2f;
    const float MAX_POSITION_X = 2f;
    const float POSITION_Y = -3.7f;
    const float MIN_PLATFORM_WIDTH = 0.3f;
    const float MAX_PLATFORM_WIDTH = 1.2f;
    const float PLATFORM_HEIGHT = 3f;
    const float RIGHT_MARGIN = 0.3f;
    const float HIDDEN_POSITION_X = 4f;
    const float ZERO = 0f;
    const float ONE = 1f;
    const float START_POSITION_X = -1.8f;
    const float MOVE_EXTRA_SCORE_Y = 0.05f;
    const float EXTRA_SCORE_Y = 1.8f;
    const float EXTRA_SCORE_X = -0.2f;
    const float EXTRA_SCORE_Z = -2f;
    const int EXTRA_SCORE_FRAMES = 10;
    const string RISE_SCORE_NAME = "riseScore";
    const string RED_DOT_NAME = "redDot";
    const string PLATFORM_CHILD_NAME = "platform";

    
    bool extraScore = false;
    int extraScoreFrameCount;
    float width;
    Vector3 moveExtraScore, extraScoreStartPosition;
    GameObject extraScoreUnit;
    #endregion


    #region Properties
    public float PositionX { get; set; }
    #endregion


    #region Unity lifecycle
    void Start()
    {
        extraScoreUnit = transform.Find(RISE_SCORE_NAME).gameObject;
        moveExtraScore = new Vector3(ZERO, MOVE_EXTRA_SCORE_Y, ZERO);
        extraScoreStartPosition = new Vector3(EXTRA_SCORE_X, EXTRA_SCORE_Y, EXTRA_SCORE_Z);
        extraScoreFrameCount = 0;
    }


    void Update()
    {

        if (extraScore && extraScoreFrameCount < EXTRA_SCORE_FRAMES)
        {
            MoveExtraScore();
        }
        else
        {
            SetExtraScoreToStartPosition();
        }
    }
    #endregion


    #region Public methods
    public void SetLeftPosition()
    {
        transform.localPosition = new Vector3(START_POSITION_X, POSITION_Y, ONE);
        PositionX = START_POSITION_X;
        width = ONE;
        GetChildPlatform().transform.localScale = new Vector3(width, PLATFORM_HEIGHT, ONE);
    }


    public void SetRightPosition(GameObject leftPlatform)
    {
        SetDimencions();
        GetChildPlatform().transform.localScale = new Vector3(width, PLATFORM_HEIGHT, ONE);
        transform.localPosition = new Vector3(PositionX, POSITION_Y, ONE);
        SetDistances(leftPlatform);
    }


    public void SetHiddenPosition()
    {
        SetDimencions();
        GetChildPlatform().transform.localScale = new Vector3(width, PLATFORM_HEIGHT, ONE);
        transform.localPosition = new Vector3(HIDDEN_POSITION_X, POSITION_Y, ZERO);
    }


    public void SetDistances(GameObject leftPlatform)
    {
        Distances.Space = -FloatRounder.Round(GetSpace(leftPlatform));
        Distances.CharacterRunDistance = Distances.Space + width;
        Distances.CharacterRunOnStickDistance = RIGHT_MARGIN;
        Distances.RightPlatformWidth = width;
        Distances.LeftPlatformWidth = leftPlatform.transform.localScale.x;
    }


    public void HideRedDot()
    {
        transform.Find(RED_DOT_NAME).gameObject.SetActive(false);
    }


    public void ShowRedDot()
    {
        transform.Find(RED_DOT_NAME).gameObject.SetActive(true);
    }

   
    public void AnimateExtraScore()
    {
        extraScore = true;
    }
    #endregion


    #region Private methods
    void MoveExtraScore()
    {
        extraScoreUnit.SetActive(true);
        extraScoreUnit.transform.localPosition += moveExtraScore;
        extraScoreFrameCount++;
    }


    void SetExtraScoreToStartPosition()
    {
        extraScore = false;
        extraScoreFrameCount = 0;
        extraScoreUnit.SetActive(false);
        extraScoreUnit.transform.localPosition = extraScoreStartPosition;
    }


    void SetDimencions()
    {
        width = FloatRounder.Round(GetWidth());
        PositionX = FloatRounder.Round(GetPositionX());
    }


    float GetWidth()
    {
        return Random.Range(MIN_PLATFORM_WIDTH, MAX_PLATFORM_WIDTH);
    }


    float GetPositionX()
    {
        return Random.Range(MIN_POSITION_X, MAX_POSITION_X);
    }


    Transform GetChildPlatform()
    {
        return transform.Find(PLATFORM_CHILD_NAME);
    }


    Transform GetChildPlatform(GameObject platform)
    {
        return platform.transform.Find(PLATFORM_CHILD_NAME);
    }


    float GetPlatfromSize(GameObject platform)
    {
        return GetChildPlatform(platform).transform.localScale.x;
    }


    float GetPlatfromAverageSize(GameObject leftPlatform)
    {
        return (GetPlatfromSize(leftPlatform) + GetPlatfromSize(gameObject)) / 2;
    }

    float GetPlatformPosition(GameObject platform)
    {
        return platform.transform.localPosition.x;
    }

    float GetDistanceBetweenPlatforms(GameObject platform)
    {
        return GetPlatformPosition(platform) - PositionX;
    }


    float GetSpace(GameObject leftPlatform)
    {
        return GetDistanceBetweenPlatforms(leftPlatform) + GetPlatfromAverageSize(leftPlatform);
    }
    #endregion
}
