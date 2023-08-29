using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceGetSpectrumDataExample : MonoBehaviour
{
    public float[] spectrum;

    public List<Transform> cubes;

    public float StepCount;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cubes.Add(transform.GetChild(i));
        }
    }

    public float TimeCount;
    public Material m1;
    public Material m2;
    public Material m3;

    void Update()
    {
        spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        TimeCount -= Time.deltaTime;
        if (TimeCount <= 0)
        {
            for (int i = 0; i < cubes.Count; i++)
            {
                float s = spectrum[i] * 10;
                if (s > 0.01 && s < 0.2)
                {
                    cubes[i].GetComponent<Renderer>().material = m1;

                }
                else if (s >= 0.2)
                {
                    cubes[i].GetComponent<Renderer>().material = m2;

                }
                else
                {
                    cubes[i].GetComponent<Renderer>().material = m3;
                }

                cubes[i].transform.localScale = new Vector3(cubes[i].localScale.x, spectrum[i] * StepCount, cubes[i].localScale.z);
            }
            TimeCount = 0.1f;
        }


        for (int i = 0; i < spectrum.Length - 1; i++)
        {
            Debug.Log(spectrum[i]);
        }
        
    }
}
