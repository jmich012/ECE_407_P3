using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {
	private bool paused = false;

	float orignalTimeScale;

	void Start()
    {
		orignalTimeScale = Time.timeScale;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P))
		{
			paused = !paused;
		}

		if(paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
}
