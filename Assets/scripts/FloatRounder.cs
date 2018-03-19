using UnityEngine;


public static class FloatRounder
{
    #region Fields
    const int digits = 10;
    const float evenDigit = 0.1f;
    #endregion


    #region Public methods
    public static float Round(float value)
    {
        float evenValue = Mathf.Round(value * digits) / digits;
        if (evenValue * digits % 2 > 0.01f) evenValue += evenDigit;
        return evenValue;
    }


    public static float RoundToCent(float value)
    {
        return Mathf.Round(value * digits*digits) / digits/digits;
    }
    #endregion
}