using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lunity.AudioVis;

public class Planet : MonoBehaviour {

    [Range(2,256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    
    [Header("Sound Capture")]
    // implement SoundCapture
    public AudioAverageSet Audio;
    private SoundCapture _sc;
    [SerializeField] private float mutiplier = 0;
    [SerializeField] private float volumeMutiplier = 0;

    public float strengthLimiter = 0f;
    private void Awake()
    {
        _sc = FindObjectOfType<SoundCapture>();
    }

    private void Update()
    {
        Initialize();
        GenerateMesh();
        SyncNoiseValue();
        RotateCentre();

    }


    private void SyncNoiseValue() 
    {
        shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.persistence = _sc.AverageVolume * volumeMutiplier;
        shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.strength =  Audio.Pulse * mutiplier;
        shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.roughness = Audio.Flicker * mutiplier;
        if (shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.strength >= strengthLimiter ) 
        {
            shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.strength = strengthLimiter;
        }
        if (shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.strength >= -strengthLimiter)
        {
            shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.strength = -strengthLimiter;
        }
    }

    private void RotateCentre() 
    {
        shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.x = shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.x + Audio.Momentary;
        shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.z = shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.z + Audio.Momentary;
        shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.y = shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.y + Audio.Momentary;
        if (shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.x >= 360) 
        {
            shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.x = 0;

        }
        if (shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.y >= 360)
        {
            shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.y = 0;

        }
        if (shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.z >= 360)
        {
            shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.centre.z = 0;

        }
    }


    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
        Debug.Log("Current noise is " + shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.persistence);
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }
    }

    void GenerateColours()
    {
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colourSettings.planetColour;
        }
    }
}
