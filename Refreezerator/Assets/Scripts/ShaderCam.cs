using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderCam : MonoBehaviour
{
	public Material postProcessingMat;
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		postProcessingMat.SetFloat("_OrthoSize", Camera.main.orthographicSize);
		Graphics.Blit(source, destination, postProcessingMat);
	}
}
