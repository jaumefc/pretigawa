using UnityEngine;
using System.Collections;


//Clase per controlar totes les variables del joc, progres, etc
public class GameState {

	private GameState instance=null;

	//Variables
	Hashtable variables = new Hashtable();


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

	public void GameSave(){
		string arrayKeys = ""+PlayerPrefs.GetString ("arrayKeys");

		foreach(DictionaryEntry vartemp in variables)
		{
			PlayerPrefs.SetString(vartemp.Key.ToString(), vartemp.Value.ToString());
			arrayKeys += "/"+vartemp.Key;
			Debug.Log("Guardant: [" + vartemp.Key + "] -> "  + vartemp.Value);
		}

		PlayerPrefs.SetString ("arrayKeys", arrayKeys);
	}

	public void GameLoad(){
		string arrayKeys = ""+PlayerPrefs.GetString ("arrayKeys");

		string[] keys = arrayKeys.Split('/');

		foreach (string keyToLoad in keys)
		{
			string value = PlayerPrefs.GetString(keyToLoad);
			variables [keyToLoad] = value;
			Debug.Log("Carregant: [" + keyToLoad + "] -> "  + value);
		}
	}

}
