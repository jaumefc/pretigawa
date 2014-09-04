/* **************************************************************************
 * FPS COUNTER
 * **************************************************************************
 * Written by: Annop "Nargus" Prapasapong
 * Created: 7 June 2012
 * *************************************************************************/

using UnityEngine;
using System.Collections;

/* **************************************************************************
 * CLASS: FPS COUNTER
 * *************************************************************************/ 
[RequireComponent(typeof(GUIText))]
public class FPSCounter : MonoBehaviour {
	/* Public Variables */
	public float frequency = 0.5f;
	
	/* **********************************************************************
	 * PROPERTIES
	 * *********************************************************************/
	public int FramesPerSec { get; protected set; }
	
	/* **********************************************************************
	 * EVENT HANDLERS
	 * *********************************************************************/
	/*
	 * EVENT: Start
	 */
	private void Start() {
		//StartCoroutine(FPS());
	}

	int frameCount = 0;
	float dt = 0.0f;
	float fps = 0.0f;
	float updateRate = 2.0f;  // 4 updates per sec.
	
	void Update()
	{
		frameCount++;
		dt += Time.deltaTime;
		if (dt > 1.0f/updateRate)
		{
			fps = frameCount / dt ;
			frameCount = 0;
			dt -= 1.0f/updateRate;
		}
		gameObject.guiText.text = fps.ToString("F2") + " fps";
	}
	
	/*
	 * EVENT: FPS
	 */
//	private IEnumerator FPS() {
//		for(;;){
//			// Capture frame-per-second
//			int lastFrameCount = Time.frameCount;
//			float lastTime = Time.realtimeSinceStartup;
//			yield return new WaitForSeconds(frequency);
//			float timeSpan = Time.realtimeSinceStartup - lastTime;
//			int frameCount = Time.frameCount - lastFrameCount;
//			
//			// Display it
//			FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
//			gameObject.guiText.text = FramesPerSec.ToString() + " fps";
//		}
//	}
}