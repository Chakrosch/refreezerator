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
	public float sunDayIntensity = 1f;
	
    // Start is called before the first frame update
    void Start()
    {
		torch = GameObject.Find("Torch");
		sun = GameObject.Find("Sun");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(dayNightButton))
		{
			Debug.Log("Button Pressed");
			torch.GetComponent<Light>().enabled = !torch.GetComponent<Light>().enabled;
			float intensity = sun.GetComponent<Light>().intensity;
			if(intensity > sunDayIntensity - 0.1f)
			{
				sun.GetComponent<Light>().intensity = sunNightIntensity;
			}
			else
			{
				sun.GetComponent<Light>().intensity = sunDayIntensity;
			}
		}
    }
	
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		postProcessingMat.SetFloat("_OrthoSize", Camera.main.orthographicSize);
		Graphics.Blit(source, destination, postProcessingMat);
	}
}
