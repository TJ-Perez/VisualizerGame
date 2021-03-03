using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicToWorld : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource audioSource;

    public UIDriver uiDriver;

    public AudioClip songClip;

    public int test = 0;


    public string songName;

    //public GameObject cube;
    public GameObject course;

    public int cubeNum = 0;

    public float[] samples = new float[512];

    public float bpm;

    private float secBeat;

    private float[] cutoffs2 = new float[] { 3, 8, 17, 30, 60, 100, 150, 200, 260, 330, 410, 511 };
    private float[] multipliers = new float[] { 1, 1.3f, 1.3f, 1.5f, 1, 2, 2, 2.5f, 2.7f, 7, 13, 18 };


    public float[] avgSamples = new float[12];


    private Vector3 cubeStart = new Vector3(0, 0, -8.5f);
    private Vector3 cubePos;


    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;



    void Start()
    {
        cubePos = cubeStart;
        course = new GameObject("Course");

        audioSource = gameObject.GetComponent<AudioSource>();
        UpdateSong();
        audioSource.PlayOneShot(songClip);


        secBeat = 1 / (bpm / 60);
        StartCoroutine(MainLoop());


    }

    void FixedUpdate()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        takeSampleAvg();

    }

    void takeSampleAvg()
    {

        int cutoffMark = 0;

        for (int i = 0; i < 512; i++)
        {
            if (i > cutoffs2[cutoffMark])
            {
                cutoffMark++;
            }

            avgSamples[cutoffMark] += samples[i] * multipliers[cutoffMark];
        }
    }

    void resetSamples()
    {
        Array.Clear(samples, 0, samples.Length);
        Array.Clear(avgSamples, 0, avgSamples.Length);

    }
    IEnumerator MainLoop()
    {

        while (true)
        {
            CreateCube();


            
            resetSamples();

            yield return new WaitForSeconds(secBeat);

        }
    }

    void CreateCube()
    {
        cubeNum++;

        GameObject createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        setBoxColor(createdCube);
        createdCube.layer = 7;
        createdCube.name = cubeNum.ToString();
        createdCube.transform.SetParent(course.transform);



        Light cubeLight = createdCube.AddComponent<Light>();
        cubeLight.color = createdCube.GetComponent<MeshRenderer>().material.color;

        SetLightsColor(cubeLight);

        PlaceBox(createdCube);


    }

    void PlaceBox(GameObject cube)
    {
        Color32 cubeColor;
        cubeColor = cube.GetComponent<MeshRenderer>().material.color;

        float addX = 1 - cubeColor.r / 128f;
        float addY = 1 - cubeColor.g / 128f;
        float addZ = 2 + cubeColor.b / 128f;

        cubePos += new Vector3(addX, addY, addZ);

        cube.transform.position = cubePos;

    }

    public void ResetCourse()
    {

        Debug.Log("1");
        foreach (Transform obj in course.transform)
        {
            Destroy(obj.gameObject);
            Debug.Log("2");

        }
        cubeNum = 0;
        cubePos = cubeStart;
        audioSource.Stop();
        UpdateSong();
        audioSource.PlayOneShot(songClip);
    }

    void UpdateSong()
    {
        songName = uiDriver.dropName;
        songClip = Resources.Load<AudioClip>("Audio/" + songName);
    }

    void SetLightsColor(Light objectLight)
    {

        light1.color= objectLight.color;
        light2.color = objectLight.color;
        light3.color = objectLight.color;
        light4.color = objectLight.color;
    }


    void setBoxColor(GameObject cube)
    {
        int R;
        int G;
        int B;

        switch (Array.IndexOf(avgSamples, avgSamples.Max()))
        {
            case 0:
                R = 255;
                G = 0;
                B = 0;
                break;
            case 1:
                R = 255;
                G = 0;
                B = 128;
                break;
            case 2:
                R = 255;
                G = 0;
                B = 255;
                break;
            case 3:
                R = 128;
                G = 0;
                B = 255;
                break;
            case 4:
                R = 0;
                G = 0;
                B = 255;
                break;
            case 5:
                R = 0;
                G = 128;
                B = 255;
                break;
            case 6:
                R = 0;
                G = 255;
                B = 255;
                break;
            case 7:
                R = 0;
                G = 255;
                B = 128;
                break;
            case 8:
                R = 0;
                G = 255;
                B = 0;
                break;
            case 9:
                R = 128;
                G = 255;
                B = 0;
                break;
            case 10:
                R = 255;
                G = 255;
                B = 0;
                break;
            case 11:
                R = 255;
                G = 128;
                B = 0;
                break;
            default:
                R = 0;
                G = 0;
                B = 0;
                Debug.Log("white");
                break;

        }

        cube.GetComponent<Renderer>().material.color = new Color(R / 255f, G / 255f, B / 255f);
    }
}