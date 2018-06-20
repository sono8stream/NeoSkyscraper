using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleInfoクラスを用いて処理
/// NeoParticleSystemクラスで一括制御するため、MonoBehaviourを継承していない
/// </summary>
public class NeoParticle
{
    Transform targetTransform;
    ParticleInfo particleInfo;

    Counter moveCounter;
    Vector2 moveVelocity;

    public NeoParticle(Transform transform,ParticleInfo info)
    {
        targetTransform = transform;
        particleInfo = info;

        InitializeMovement();
    }

    public void Move()
    {
        if (moveCounter.Count())
        {
            InitializeMovement();
        }
        else
        {
            targetTransform.localPosition += (Vector3)moveVelocity;
        }
    }

    public void InitializeMovement()
    {
        Vector2 vector = RandVector(particleInfo.moveVector, particleInfo.lengthRandRatio);
        float speed = RandValue(particleInfo.moveSpeed, particleInfo.speedRandRatio);
        moveCounter = new Counter((int)(vector.magnitude / speed));
        moveVelocity = vector / moveCounter.Limit;
    }

    public void Refract()
    {
        float prob = Random.value;
        if (prob > particleInfo.refractionProbability) return;

        float rad = RandValue(particleInfo.refractionRadian, particleInfo.refractionRandRatio);
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);
        float radius = particleInfo.moveVector.magnitude;
    }

    Vector2 RandVector(Vector2 baseVector,float randRatio)
    {
        float rate = Random.Range(-randRatio, randRatio);
        return baseVector * rate;
    }

    float RandValue(float baseValue, float randRatio)
    {
        float amplitude = baseValue * Random.Range(-randRatio, randRatio);
        return baseValue + amplitude;
    }
}