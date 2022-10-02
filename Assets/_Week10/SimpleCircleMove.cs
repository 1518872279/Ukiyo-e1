using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lunity.AudioVis;

public class SimpleCircleMove : MonoBehaviour
{
    public GameObject objRotate;
    public GameObject centreObj;
    public float Speed = 0f;

    [Header("Sound Capture")]
    // implement SoundCapture
    public AudioAverageSet Audio;
    private SoundCapture _sc;
    //[SerializeField] private float mutiplier = 0;
    public float volumeMutiplier = 0;

    private void Awake()
    {
        _sc = FindObjectOfType<SoundCapture>();
    }
    private void Update()
    {
        Speed = _sc.AverageVolume * volumeMutiplier;
        //objRotate.transform.RotateAround(centreObj.transform.position, centreObj.transform.up, Speed * Time.deltaTime);
        objRotate.transform.RotateAround(centreObj.transform.position, centreObj.transform.right, Speed * Time.deltaTime);
        if (_sc.PeakVolume >= 0.1)
        {
            Speed = Speed + 5;
        }
        if (_sc.PeakVolume >= 0.5)
        {
            Speed = Speed + 10;
        }
    }
}
