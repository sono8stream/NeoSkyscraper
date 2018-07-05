using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleInfo
{
    public float durationSec;

    public float moveLength;
    public float lengthRandRatio;
    public float defaultAngle;
    public float defaultAngleAmplitude;

    public float moveSpeed;
    public float speedRandRatio;
    public EasingID easingID;

    public float refractionAngle;
    public float refractionAngleAmplitude;
    public float refractionProbability;
    public bool canRefractFirst;
    public bool canUpdateRad;


    public ParticleInfo(float duration,
        float length, float lengthRndRatio,
        float defaultAng,float defaultAngleAmp,
        float speed, float speedRndRatio,EasingID id,
        float refractionAng, float refractAngleAmp, float refractionProb,
        bool firstRefract, bool radUpdatable)
    {
        durationSec = duration;
        moveLength = length;
        lengthRandRatio = lengthRndRatio;
        defaultAngle = defaultAng;
        defaultAngleAmplitude = defaultAngleAmp;

        moveSpeed = speed;
        speedRandRatio = speedRndRatio;
        easingID = id;

        refractionAngle = refractionAng;
        refractionAngleAmplitude = refractAngleAmp;
        refractionProbability = refractionProb;

        canRefractFirst = firstRefract;
        canUpdateRad = radUpdatable;
    }
}