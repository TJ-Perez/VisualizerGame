using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicToWorld : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource song;

    //public GameObject cube;

    public int cubeNum;

    public float[] samples = new float[512];

    public float bpm;

    private float secBeat;

    private float[,] cutoffs = new float[,] { {0, 3},
                                              {4, 8},
                                              {9, 17},
                                              {18, 40},
                                              {31, 60},
                                              {61, 100},
                                              {101, 150}, 
                                              {151, 200}, 
                                              {201, 260}, 
                                              {261, 330}, 
                                              {331, 410}, 
                                              {411, 511}};

    private float[] cutoffs2 = new float[] { 3, 8, 17, 30, 60, 100, 150, 200, 260, 330, 410, 511 };
    private float[] multipliers = new float[] { 1, 1.3f, 1.3f, 1.5f, 1, 2, 2, 2.5f, 2.7f, 7, 13, 18 };


    public float[] avgSamples = new float[12];


    void Start()
    {
        song = GetComponent<AudioSource>();

        secBeat = 1 / (bpm / 60);

        StartCoroutine(DoSomething());


    }

    // Update is called once per frame
    void Update()
    {
        song.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        //Debug.Log(samples);
        takeSampleAvg();




        //resetSamples();

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
    IEnumerator DoSomething()
    {

        while (true)
        {
            CreateCube();
            GameObject createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            createdCube.transform.position = new Vector3(0, 8.5f, 0);
            Rigidbody cubeRB = createdCube.AddComponent<Rigidbody>();
            cubeRB.mass = 1;
            setBoxColor(createdCube);
            resetSamples();
            yield return new WaitForSeconds(secBeat);

        }
    }

    void CreateCube()
    {
        //GameObject createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(0, 2.5f, 0);


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