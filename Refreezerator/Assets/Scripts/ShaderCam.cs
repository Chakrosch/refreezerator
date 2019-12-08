using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderCam : MonoBehaviour
{
	public Material postProcessingMat;
	private string dayNightButton = "Day Night Switch";
	private GameObject torch;
	private GameObject sun;
	public float sunNightIntensity = 0.25f;
	
    // Start is called before the first frame update
    void Start()
    {
		torch = GameObject.Find("Torch");
		sun = GameObject.Find("Sun");
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
    private void getInput()
    {
        if(Input.GetButton(dayNightButton))
		{
			Debug.Log("Button Pressed");
		}
    }
	
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		postProcessingMat.SetFloat("_OrthoSize", Camera.main.orthographicSize);
		Graphics.Blit(source, destination, postProcessingMat);
	}
}
