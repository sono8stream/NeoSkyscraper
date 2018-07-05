using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingFunctions
{
    public float Easing(float lim, float x,EasingID id)
    {
        float y = 0;

        switch (id)
        {
            case EasingID.Linear:
                y = Linear(lim, x);
                break;
            case EasingID.EaseInSine:
                y = EaseInSine(lim, x);
                break;
            case EasingID.EaseOutSine:
                y = EaseOutSine(lim, x);
                break;
            case EasingID.EaseInOutSine:
                y = EaseInOutSine(lim, x);
                break;
        }

        return y;
    }

    float Linear(float lim, float x)
    {
        return x / lim;
    }

    float EaseInSine(float lim, float x)
    {
        return 1 - Mathf.Cos(0.5f * Mathf.PI * x / lim);
    }

    float EaseOutSine(float lim, float x)
    {
        return Mathf.Sin(0.5f * Mathf.PI * x / lim);
    }

    float EaseInOutSine(float lim, float x)
    {
        return 0.5f - 0.5f * Mathf.Cos(Mathf.PI * x / lim);
    }
}

[System.Serializable]
public enum EasingID
{
    Linear = 0, EaseInSine, EaseOutSine, EaseInOutSine
}
