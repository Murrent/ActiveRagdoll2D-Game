using UnityEngine;

public class ParticleSelfDestroyer : MonoBehaviour
{
    ParticleSystem particle;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (particle.isStopped)
            Destroy(gameObject);
    }
}
