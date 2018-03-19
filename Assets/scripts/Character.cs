using UnityEngine;


public class Character : UnitObject
{
    #region Fields
    const float SPEED_X = 0.05f;
    const float SPEED_Y = 0.3f;
    const float POSITION_X = -1.6f;
    const float POSITION_Y = -1.95f;
    const float ZERO = 0f;

    
    [SerializeField]
    GameObject scoreSetter;


    Vector3 rightSpeed = new Vector3(SPEED_X, ZERO, ZERO);
    #endregion


    #region Properties
    public Vector3 DownSpeed { get; set; }
    public bool IsFalling { get; set; }
    public bool IsReady { get; set; }
    public bool IsStickNotEnded { get; set; }
    public bool IsFinished { get; set; }
    public float TravaledDistance { get; set; }
    #endregion


    #region Unity lifecycle
    void Start()
    {
        DownSpeed = new Vector3(ZERO, SPEED_Y, ZERO);
        IsFalling = false;
        IsFinished = false;
        IsReady = true;
        IsStickNotEnded = true;
        TravaledDistance = 0.0f;
    }
    #endregion


    #region Public methods
    public void Initialize()
    {
        transform.localPosition = new Vector3(POSITION_X, POSITION_Y, ZERO);
    }


    public void Move()
    {
        IsReady = false;
        if ((TravaledDistance > Distances.CharacterRunOnStickDistance && Distances.CharacterRunOnStickDistance < Distances.Space + 0.3f))
        {
            SetFalling();
        }
        else if (FloatRounder.RoundToCent(TravaledDistance) < FloatRounder.RoundToCent(Distances.CharacterRunDistance))
        {
        }
        else if (FloatRounder.RoundToCent(Distances.StickLength) < FloatRounder.RoundToCent(Distances.CharacterRunDistance))
        {
            SetReady();
            scoreSetter.GetComponent<ScoreSetter>().RiseScore();
        }
        else if (TravaledDistance > Distances.CharacterRunOnStickDistance)
        {
            SetFalling();
        }
        Step();

    }


    public void Fall()
    {
        if (transform.localPosition.y > -10)
        {
            transform.localPosition -= DownSpeed;
        }
    }
    #endregion


    #region Private methods 
    void Step()
    {
        if (!IsFinished)
        {
            TravaledDistance += SPEED_X;
            transform.localPosition += rightSpeed;
        }
    }


    void SetFalling()
    {
        IsFalling = true;
        SetReady();
    }


    void SetReady()
    {
        IsFinished = true;
        IsReady = true;
    }
    #endregion
}

