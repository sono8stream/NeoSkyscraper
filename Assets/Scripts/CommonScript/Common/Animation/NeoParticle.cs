using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleInfoクラスを用いて処理
/// NeoParticleProcessorクラスで一括制御するため、MonoBehaviourを継承していない
/// </summary>
public class NeoParticle
{
    public Transform target { get; private set; }
    ParticleInfo info;

    Counter moveCounter;
    Vector2 moveVector;
    Vector2 basePos;
    NeoShot shot;
    List<NeoShot> shots;
    float angle;
    TimeCounter lifeTimer;

    public NeoParticle(Transform target, ParticleInfo info)
    {
        this.target = target;
        this.info = info;

        float angle = RandDegree(info.defaultAngle, info.defaultAngleAmplitude);
        target.localEulerAngles = Vector3.forward * angle;
        if (info.canRefractFirst)
        {
            Refract();
        }

        moveCounter = new Counter(1, true);
        shots = new List<NeoShot>();
        lifeTimer = new TimeCounter(info.durationSec);
        lifeTimer.Start();
    }

    public bool Move(NeoParticleProcessor processor)
    {
        if (lifeTimer.OnLimit())
        {
            processor.AddRemainShots(shots);
            for (int i = 0; i < shots.Count; i++)
            {
                shots[i].Period();
            }
            return false;
        }

        if (moveCounter.Count())
        {
            Refract();
            InitializeMovement();
            if (shot != null)
            {
                shot.Period();
            }
            shot = processor.GenerateShot(target);
            shots.Add(shot);
        }

        var newShots = new List<NeoShot>();
        for(int i = 0; i < shots.Count; i++)
        {
            if (shots[i].Ray(target.localPosition))
            {
                newShots.Add(shots[i]);
            }
            else
            {
                processor.PoolShot(shots[i]);
            }
        }
        shots = newShots;

        float rate = processor.Easing.Easing(moveCounter.Limit, moveCounter.Now, info.easingID);
        target.localPosition = basePos + moveVector * rate;

        return true;
    }

    void InitializeMovement()
    {
        float length = RandValue(info.moveLength, info.lengthRandRatio);
        angle = target.localEulerAngles.z * Mathf.Deg2Rad;
        moveVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * length;

        float speed = RandValue(info.moveSpeed, info.speedRandRatio);
        moveCounter = new Counter((int)Mathf.Abs(length / speed));
        basePos = target.localPosition;
    }

    void Refract()
    {
        float prob = Random.value;
        float deltaAngle;
        if (prob < info.refractionProbability)
        {
            deltaAngle = RandDegree(info.refractionAngle,
                info.refractionAngleAmplitude);
        }
        else
        {
            deltaAngle = 0;
        }

        if (info.canUpdateRad)
        {
            target.localEulerAngles += Vector3.forward * deltaAngle;
        }
        else
        {
            target.localEulerAngles = Vector3.forward * (deltaAngle + info.defaultAngle);
        }
    }

    float RandValue(float baseValue, float randRatio)
    {
        float amplitude = baseValue * Random.Range(-randRatio, randRatio);
        return baseValue + amplitude;
    }

    float RandDegree(float baseAngle, float amplitude)
    {
        float amp = Random.Range(-amplitude, amplitude);
        return (baseAngle + amp);
    }
}