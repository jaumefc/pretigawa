#pragma strict

private var cams : GameObject[];

function Start () {}

function Update () {}

public function changeSaturation(saturationFactor:float){
	
	cams = GameObject.FindGameObjectsWithTag("GameCameras");
	
	for(var i=0; i<cams.length-1; i++){
		cams[i].transform.Find("Camera").GetComponent(ColorCorrectionCurves).saturation += saturationFactor;
	}
}