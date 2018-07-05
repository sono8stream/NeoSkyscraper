using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoShot
{
    public Transform Target { get; private set; }
    public bool TimerEnabled { get { return timer.Enabled; } }

    //Transform particleTransform;
    ShotInfo info;

    TimeCounter timer;
    SpriteRenderer renderer;
    Vector2 initPos;
    Vector3 initScale;
    float maxAlpha;

    public NeoShot(Vector2 pos,Transform transform,ShotInfo shotInfo)
    {
        Target = transform;
        info = shotInfo;

        initPos = pos;
        timer = new TimeCounter(info.durationSec);
        renderer = Target.GetComponent<SpriteRenderer>();
        maxAlpha = renderer.color.a;
    }

    public bool Ray(Vector2 targetPos)
    {
        if (timer.OnLimit())
        {
            Color c = renderer.color;
            c.a=maxAlpha;
            renderer.color = c;
            Target.localScale = initScale;
            return false;
        }

        if (timer.Enabled)
        {
            Color c = renderer.color;
            c.a = maxAlpha * (timer.Limit - timer.Now) / timer.Limit;
            renderer.color = c;
            float rate = (timer.Limit - timer.Now) / timer.Limit;
            Target.localScale = new Vector3(initScale.x, initScale.y * rate);
        }
        else
        {
            float length = (targetPos - initPos).magnitude;
            Target.localPosition = (initPos + targetPos) / 2;
            Vector3 scale = Target.localScale;
            scale.x = length;
            Target.localScale = scale;
        }

        return true;
    }

    public void Period()
    {
        if (timer.Enabled) return;

        timer.Start();
        initScale = Target.localScale;
    }
}