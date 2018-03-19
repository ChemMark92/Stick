using UnityEngine;


public class UnitObject : MonoBehaviour
{
    #region Public methods
    public void StepToLeft(float distance)
    {
        transform.localPosition += new Vector3(distance, 0, 0);
    }


    public float Position
    {
        get
        {
            return transform.localPosition.x;
        }
    }
    #endregion
}
