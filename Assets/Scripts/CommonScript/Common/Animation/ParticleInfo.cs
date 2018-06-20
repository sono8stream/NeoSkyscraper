using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleInfo
{
    public Vector2 moveVector;
    public float lengthRandRatio;
    public float refractionRadian;
    public float refractionRandRatio;
    public float refractionProbability;
    public float moveSpeed;
    public float speedRandRatio;

    public ParticleInfo(Vector2 vector, float lengthRndRatio, float refractionRad,
        float refractionRndRatio, float refractionProb,float speed,float speedRndRatio)
    {
        moveVector = vector;
        lengthRandRatio = lengthRndRatio;
        refractionRadian = refractionRad;
        refractionRandRatio = refractionRndRatio;
        refractionProbability = refractionProb;
        moveSpeed = speed;
        speedRandRatio = speedRndRatio;
    }
}
