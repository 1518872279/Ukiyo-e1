using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lunity.AudioVis;

public class SakuraParticle : MonoBehaviour
{
    public ParticleSystem SakuraParticles;
    public AudioAverageSet Audio;
    private SoundCapture _sc;
    public float speedMultiplier = 0;
    public int particleQuantityMultipier = 0;
    //where the audio is building up and turn off the particle using this 
    public float BuildUplimit = 0;

    private void Start()
    {
        SakuraParticles = GetComponent<ParticleSystem>();
        _sc = FindObjectOfType<SoundCapture>();
    }

    private void Update()
    {
        ManageParticles();
        ModifyGravity();
    }

    private void ManageParticles()
    {
        SakuraParticles.startSpeed = speedMultiplier * _sc.PeakVolume;
        var emission = SakuraParticles.emission;
        emission.rateOverTime = particleQuantityMultipier * _sc.PeakVolume;
        if (_sc.PeakVolume >= 0.8f)
        {
            SakuraParticles.Emit(500);
        }

    }

    private void ModifyGravity()
    {
        if (Audio.Momentary >= BuildUplimit)
        {
            SakuraParticles.gravityModifier = Audio.Momentary * -1;
        }
        else if (Audio.Momentary < BuildUplimit)
        {
            SakuraParticles.gravityModifier = Audio.Momentary;
        }
    }
}
