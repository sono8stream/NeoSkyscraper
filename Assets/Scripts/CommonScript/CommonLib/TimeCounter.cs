using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter
{
    public bool Enabled { get; private set; }
    public float Limit { get; private set; }
    public float Now { get {
            return Enabled ? Time.fixedTime - startTime : stopTime - startTime;
        } }

    float startTime;
    float stopTime;

    public TimeCounter(float lim)
    {
        Limit = lim;
    }

    public bool OnLimit()
    {
        if (!Enabled) return false;
        if (Now >= Limit)
        {
            Enabled = false;
            return true;
        }
        return false;
    }

    public void Start(float newLim = -1)
    {
        Limit = newLim == -1 ? Limit : newLim;
        startTime = Time.fixedTime;
        Enabled = true;
    }

    public void Pause()
    {
        if (!Enabled) return;
        Enabled = false;
        stopTime = Time.fixedTime;
    }

    public void Restart()
    {
        if (Enabled) return;
        startTime += Time.fixedTime - stopTime;//停止時間だけ進める
        Enabled = true;
    }
}