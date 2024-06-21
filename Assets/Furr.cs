using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class Furr : MonoBehaviour {
    public Shader sShader;
    public Scrollbar scrollbar;
    [Range(1, 256)]
    public int sCount;
    public float sLength;
    public float density;
    public float noiseMin;
    public float noiseMax;
    public float thickness;
    public Color shellColor;
    private Material sMaterial;
    public float distAtt;
    public float occAtt;
	public Mesh sMesh;
    private GameObject[] shells;

    void OnEnable() {
        sMaterial = new Material(sShader);
        shells = new GameObject[sCount];

        for (int i = 0; i < sCount; ++i) {
            shells[i] = new GameObject();
            shells[i].AddComponent<MeshFilter>();
            shells[i].AddComponent<MeshRenderer>();
            shells[i].GetComponent<MeshRenderer>().material = sMaterial;
            shells[i].GetComponent<MeshFilter>().mesh = sMesh;
            shells[i].transform.SetParent(this.transform, false);
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_shIndex", i);
            if(scrollbar){
                    shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", (int)(scrollbar.value*sCount)+3);
            }else{
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", sCount);
            }
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shDensity", density);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shLength", sLength);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_minNoise", noiseMin);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_maxNoise", noiseMax);
            shells[i].GetComponent<MeshRenderer>().material.SetColor("_shColor", shellColor);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shAttenuation", occAtt);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shDistanceAttenuation", distAtt);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shThickness", thickness);
        }
        
    }

    void Update() {
        for (int i = 0; i < sCount; ++i) {
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_shIndex", i);
            if(scrollbar){
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", (int)(scrollbar.value*sCount)+3);
            }else{
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", sCount);
            }
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shDensity", density);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shLength", sLength);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_minNoise", noiseMin);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_maxNoise", noiseMax);
            shells[i].GetComponent<MeshRenderer>().material.SetColor("_shColor", shellColor);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shAttenuation", occAtt);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shDistanceAttenuation", distAtt);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shThickness", thickness);
        }
    }
}
