using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lunity.AudioVis;

public class WaveParticleController : MonoBehaviour
{
    public ParticleSystem WaveParticles;
    public AudioAverageSet Audio;
    private SoundCapture _sc;
    public float speedMultiplier = 0;
    public int particleQuantityMultipier = 0;
    //where the audio is building up and turn off the particle using this 
    public float BuildUplimit = 0;

    private void Start()
    {
        WaveParticles = GetComponent<ParticleSystem>();
        _sc = FindObjectOfType<SoundCapture>();
    }

    private void Update()
    {
        ManageParticles();
        ModifyGravity();
    }

    private void ManageParticles() 
    {
        WaveParticles.startSpeed = speedMultiplier * _sc.PeakVolume;
        //var emission = WaveParticles.emission;
        //emission.rateOverTime = particleQuantityMultipier * _sc.PeakVolume;

    }

    private void ModifyGravity()
    {
        if (Audio.Momentary >= BuildUplimit)
        {
            WaveParticles.gravityModifier = Audio.Momentary * -1;
        }
        else if (Audio.Momentary < BuildUplimit)
        {
            WaveParticles.gravityModifier = Audio.Momentary;
        }
    }
}
