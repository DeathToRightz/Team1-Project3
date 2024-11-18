using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particleEffect; // Assign your particle effect in the Inspector

    public void StartParticleEffect()
    {
        if (particleEffect != null)
        {
            particleEffect.Play();
        }
    }
}
