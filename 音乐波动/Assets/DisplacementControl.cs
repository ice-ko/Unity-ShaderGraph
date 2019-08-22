using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacementControl : MonoBehaviour
{

    public float displacementAmount;
    public ParticleSystem explosionParticles;
    MeshRenderer meshRender;
    public AudioSource audio;
    private int m_NumSamples = 1024;
    private float[] m_Samples;
    private float max, sum, rms;
    private Vector3 scale;
    private float volume = 30.0f;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        m_Samples = new float[m_NumSamples];
    }

    // Update is called once per frame
    void Update()
    {

        audio.GetOutputData(m_Samples, 0);
        for (int i = 0; i < m_NumSamples; i++)
        {
            sum = m_Samples[i] * m_Samples[i];
        }
        rms = Mathf.Sqrt(sum / m_NumSamples);
        scale.y = Mathf.Clamp01(rms * volume);

        //displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime);
        //meshRender.material.SetFloat("_Amount", displacementAmount);
        meshRender.material.SetFloat("_Amount", scale.y);
        meshRender.material.SetColor("_Color", GetVolumeColor(scale.y));


    }

    Color GetVolumeColor(float volume)
    {
        //Debug.Log(volume);
        if (volume > 0.3f)
        {
            explosionParticles.Play();
            return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        else
        {
           return Color.blue;
        }
    }
}
