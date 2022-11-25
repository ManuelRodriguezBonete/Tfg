using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeVariables : MonoBehaviour
{
    // Start is called before the first frame update
    public Material ShaderMaterial1;
    public float elapsedTime = 0;

    void Start()
    {
        //ShaderMaterial1 = new Material(Shader.Find("Shader Graphs/try1"));
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        ShaderMaterial1.SetFloat("_Movement", elapsedTime);
    }
}
