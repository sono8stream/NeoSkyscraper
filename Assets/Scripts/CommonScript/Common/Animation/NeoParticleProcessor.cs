using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoParticleProcessor : MonoBehaviour
{
    [SerializeField]
    ParticleInfo particleInfo;
    [SerializeField]
    GameObject particleOrigin;

    List<NeoParticle> particles;

    // Use this for initialization
    void Start()
    {
        particles = new List<NeoParticle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GenerateParticles(1);

        for(int i = 0; i < particles.Count; i++)
        {
            particles[i].Move();
        }
    }

    void GenerateParticles(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject g = Instantiate(particleOrigin, transform);
            particles.Add(new NeoParticle(g.transform, particleInfo));
        }
    }
}