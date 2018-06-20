using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleInfoクラスを用いて処理
/// NeoParticleProcessorクラスで一括制御するため、MonoBehaviourを継承していない
/// </summary>
public class NeoParticle
{
    Transform targetTransform;
    ParticleInfo particleInfo;

    Counter moveCounter;
    Vector2 moveVelocity;
    float angleRad;

    public NeoParticle(Transform transform,ParticleInfo info)
    {
        targetTransform = transform;
        particleInfo = info;

        if (particleInfo.canRefractFirst)
        {
            Refract();
        }
        else
        {
            angleRad = RandValue(particleInfo.defaultRadian, particleInfo.defaultRadianRatio);
        }
        InitializeMovement();
    }

    public void Move()
    {
        targetTransform.localPosition += (Vector3)moveVelocity;

        if (moveCounter.Count())
        {
            CheckRefraction();
            InitializeMovement();
        }
    }

    void InitializeMovement()
    {
        float length = RandValue(particleInfo.moveLength, particleInfo.lengthRandRatio);
        var vector = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * length;

        float speed = RandValue(particleInfo.moveSpeed, particleInfo.speedRandRatio);
        moveCounter = new Counter((int)Mathf.Abs(length / speed));
        moveVelocity = vector / moveCounter.Limit;
    }

    void CheckRefraction()
    {
        float prob = Random.value;
        if (prob < particleInfo.refractionProbability)
        {
            Refract();
        }
    }

    void Refract()
    {
        float deltaRadian 
            = RandValue(particleInfo.refractionRadian, particleInfo.refractionRandRatio);

        if (particleInfo.canUpdateRad)
        {
            angleRad += deltaRadian;
        }
        else
        {
            angleRad = particleInfo.defaultRadian + deltaRadian;
        }
    }

    /*Vector2 RandVector(Vector2 baseVector,float randRatio)
    {
        float rate = Random.Range(-randRatio, randRatio);
        return baseVector * rate;
    }*/

    float RandValue(float baseValue, float randRatio)
    {
        float amplitude = baseValue * Random.Range(-randRatio, randRatio);
        return baseValue + amplitude;
    }
}