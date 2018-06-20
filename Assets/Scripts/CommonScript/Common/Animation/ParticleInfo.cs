using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleInfo
{
    public float moveLength;
    public float lengthRandRatio;
    public float defaultRadian;
    public float defaultRadianRatio;

    public float moveSpeed;
    public float speedRandRatio;


    public float refractionRadian;
    public float refractionRandRatio;
    public float refractionProbability;
    public bool canRefractFirst;
    public bool canUpdateRad;


    public ParticleInfo(float length, float lengthRndRatio,
        float defaultRad,float defaultRadRatio,
        float speed, float speedRndRatio,
        float refractionRad, float refractionRndRatio, float refractionProb,
        bool firstRefract, bool radUpdatable)
    {
        moveLength = length;
        lengthRandRatio = lengthRndRatio;
        defaultRadian = defaultRad;
        defaultRadianRatio = defaultRadRatio;

        moveSpeed = speed;
        speedRandRatio = speedRndRatio;

        refractionRadian = refractionRad;
        refractionRandRatio = refractionRndRatio;
        refractionProbability = refractionProb;

        canRefractFirst = firstRefract;
        canUpdateRad = radUpdatable;
    }
}