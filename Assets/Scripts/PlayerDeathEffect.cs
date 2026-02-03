using System;
using UnityEngine;

public class PlayerDeathEffect : MonoBehaviour
{
    [SerializeField] public ParticleSystem deathEffect;
    //[SerializeField] private string targetTag = "Spike";

    public void PlayDeathEffect()
    {
        deathEffect.Play();
    }
}
