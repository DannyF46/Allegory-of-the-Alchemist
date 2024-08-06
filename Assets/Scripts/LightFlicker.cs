using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public Light _light;
    public float flickerRate = 10f;
    public float baseIntensity = 1E+06f;
    public float flickerMagnitude = 1E+05f;

    void Start()
    {
        _light = this.GetComponent<Light>();
    }

    void Update()
    {
        float flickeramount = 2f*(Mathf.PerlinNoise1D(flickerRate * Time.timeSinceLevelLoad) - 0.5f);


        _light.intensity = baseIntensity + flickerMagnitude * flickeramount;
    }

    /*
    void OnGUI()
    {
        EditorGUILayout.LabelField("Min Val:", minVal.ToString());
        EditorGUILayout.LabelField("Max Val:", maxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, minLimit, maxLimit);

    }*/
}
