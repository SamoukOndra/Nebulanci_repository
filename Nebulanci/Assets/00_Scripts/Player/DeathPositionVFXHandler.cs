using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPositionVFXHandler : MonoBehaviour
{
    [SerializeField] new ParticleSystem particleSystem;

    public void PlayDeathVFX()
    {
        particleSystem.Play();
    }
}
