using UnityEngine;
using System.Collections;


//Clase per controlar totes les variables del joc, progres, etc
public class GameState {

	private GameState instance=null;

	//Variables
	Hashtable variables=new Hashtable();

	private GameState(){
		}


	public GameState GetInstance() {
		if (instance == null)
						instance = new GameState ();
		return instance;
	}

	public object GetVariable(string var){
		if (variables.Contains (var))
						return variables [var];
				else
						Debug.LogError ("La variable:[" + var + "] no existeix al GameState");
		return null;
	}

	public void SetVariable(string var, object value){
		if (variables.Contains (var))
						variables [var] = value;
		else
			Debug.LogError ("La variable:[" + var + "] no existeix al GameState");
	}


}
