using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoParticleProcessor : MonoBehaviour
{
    [SerializeField]
    int particleCapacity;
    [SerializeField]
    float interval;
    [SerializeField]
    ParticleInfo particleInfo;
    [SerializeField]
    GameObject particleOrigin;
    [SerializeField]
    ShotInfo shotInfo;
    [SerializeField]
    GameObject shotOrigin;
    [SerializeField]
    int shotCount;

    public EasingFunctions Easing { get; private set; }

    List<NeoParticle> particles;
    List<NeoShot> remainShots;
    bool haveShot;
    ObjectPool particlePool, shotPool;
    TimeCounter particleTimer;

    // Use this for initialization
    void Start()
    {
        particles = new List<NeoParticle>();
        remainShots = new List<NeoShot>();
        haveShot = shotOrigin && shotInfo.durationSec > 0;
        Easing = new EasingFunctions();
        particlePool = new ObjectPool(particleCapacity);
        shotPool = new ObjectPool(10000);
        particleTimer = new TimeCounter(interval);
        particleTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (particleTimer.OnLimit())
        {
            particleTimer.Start();
            GenerateParticles(1);
        }

        var nextParticles = new List<NeoParticle>();
        for (int i = 0; i < particles.Count; i++)
        {
            if (particles[i].Move(this))
            {
                nextParticles.Add(particles[i]);
            }
            else
            {
                particlePool.Push(particles[i].target.gameObject);
            }
        }
        particles = nextParticles;

        var nextShots = new List<NeoShot>();
        for (int i = 0; i < remainShots.Count; i++)
        {
            if (remainShots[i].Ray(Vector2.zero))
            {
                nextShots.Add(remainShots[i]);
            }
            else
            {
                PoolShot(remainShots[i]);
            }
        }
        remainShots = nextShots;

        shotCount = shotPool.Count + remainShots.Count;
    }

    void GenerateParticles(int count)
    {
        for (int i = 0; i < count; i++)
        {

            GameObject g = particlePool.Pop();
            if (!g)
            {
                if (particles.Count < particleCapacity)
                {
                    g = Instantiate(particleOrigin, transform);
                }
                else
                {
                    return;
                }
            }
            g.transform.localPosition = Vector3.zero;
            particles.Add(new NeoParticle(g.transform, particleInfo));
        }
    }

    public NeoShot GenerateShot(Transform t)
    {
        if (!haveShot) return null;

        GameObject g = shotPool.Pop();
        if (!g) g=Instantiate(shotOrigin, transform);
        g.transform.position = t.position;
        g.transform.eulerAngles = t.eulerAngles;
        NeoShot shot = new NeoShot(t.localPosition, g.transform, shotInfo);
        return shot;
    }

    public void PoolShot(NeoShot shot)
    {
        shotPool.Push(shot.Target.gameObject);
    }

    public void AddRemainShots(List<NeoShot> newShots)
    {
        remainShots.AddRange(newShots);
    }
}