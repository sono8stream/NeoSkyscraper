using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShotInfo
{
    public float durationSec;
    public float maxLength;

    public ShotInfo(float duration,float length)
    {
        durationSec = duration;
        maxLength = length;
    }
}
