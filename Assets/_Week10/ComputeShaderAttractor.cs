using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lunity.AudioVis;

public class ComputeShaderAttractor : MonoBehaviour
{
    //This is just a simple class to populate the attractor position/strength
    //using the mouse.
    
    public float MaxForce = 10f;
    public float Distance = 5f;
    public ComputeShaderSystem ShaderSystem;
    public GameObject Object_Follow;
    public SimpleCircleMove simpleCircleMove;

    private Camera _camera;

    private SoundCapture _sc;

    public float speedIncrementLimit = 0f;
    //[SerializeField] private float volumeMutiplier = 0;

    public void Awake()
    {
        _camera = Camera.main;
        _sc = FindObjectOfType<SoundCapture>();
    }
    
    public void Update()
    {
        //MoveWithMouse();
        MoveWithObject();
    }

    private void MoveWithMouse() 
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Distance;
        var position = _camera.ScreenToWorldPoint(mousePos);
        ShaderSystem.AttractorPosition = position;
        ShaderSystem.AttractorStrength = Input.GetMouseButton(0) ? MaxForce : 0f;
    }

    private void MoveWithObject() 
    {

        //MaxForce = simpleCircleMove.Speed;
        MaxForce = _sc.PeakVolume * simpleCircleMove.volumeMutiplier;
        ShaderSystem.AttractorPosition = Object_Follow.transform.position;
        ShaderSystem.AttractorStrength = MaxForce;
        if (_sc.PeakVolume >= speedIncrementLimit)
        {
            //MaxForce = MaxForce + 20;
        }

    }
}
