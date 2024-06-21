using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class SimpleShell : MonoBehaviour {
    public Mesh shellMesh;
    public Shader shellShader;

    public Scrollbar scrollbar;

    public bool updateStatics = true;
    [Range(1, 256)]
    public int shellCount = 16;

    public float shellLength = 0.15f;

    public float distanceAttenuation = 1.0f;

    public float density = 100.0f;

    public float noiseMin = 0.0f;
    public float noiseMax = 1.0f;

    public float thickness = 1.0f;

    public Color shellColor;

    public float occlusionAttenuation = 1.0f;
	
    private Material shellMaterial;
    private GameObject[] shells;
    private Vector3 displacementDirection = new Vector3(0, 0, 0);

    void OnEnable() {
        shellMaterial = new Material(shellShader);
        shells = new GameObject[shellCount];

        for (int i = 0; i < shellCount; ++i) {
            shells[i] = new GameObject("Shell " + i.ToString());
            shells[i].AddComponent<MeshFilter>();
            shells[i].AddComponent<MeshRenderer>();
            
            shells[i].GetComponent<MeshFilter>().mesh = shellMesh;
            shells[i].GetComponent<MeshRenderer>().material = shellMaterial;
            shells[i].transform.SetParent(this.transform, false);

            if(scrollbar){
                    shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", (int)(scrollbar.value*shellCount)+3);
            }else{
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", shellCount);
            }
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_shIndex", i);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shDensity", density);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shAttenuation", occlusionAttenuation);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shDistanceAttenuation", distanceAttenuation);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shThickness", thickness);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shLength", shellLength);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_minNoise", noiseMin);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_maxNoise", noiseMax);
            shells[i].GetComponent<MeshRenderer>().material.SetColor("_shColor", shellColor);
            // shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", 1);
        }
        
    }

    void Update() {
        

        if (updateStatics) {
            for (int i = 0; i < shellCount; ++i) {
                if(scrollbar){
                    shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", (int)(scrollbar.value*shellCount)+3);
                }else{
                    shells[i].GetComponent<MeshRenderer>().material.SetInt("_shCount", shellCount);
                }
                
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_shIndex", i);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shDensity", density);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shAttenuation", occlusionAttenuation);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shDistanceAttenuation", distanceAttenuation);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shThickness", thickness);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_shLength", shellLength);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_minNoise", noiseMin);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_maxNoise", noiseMax);
                shells[i].GetComponent<MeshRenderer>().material.SetColor("_shColor", shellColor);
            }
        }
    }
}
