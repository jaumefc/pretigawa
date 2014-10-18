using UnityEngine;
using System.Collections;


//Clase per controlar totes les variables del joc, progres, etc
public class GameState {

	private static GameState instance=null;

	//Variables
	Hashtable variables = new Hashtable();
	Hashtable stringVars = new Hashtable();
	Hashtable intVars = new Hashtable();
	Hashtable floatVars = new Hashtable();
	Hashtable booleanVars = new Hashtable();


	private GameState(){
		}

	public static GameState GetInstance() {
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
	
	public string GetString(string var){
		if(stringVars.Contains(var))
			return (string)stringVars[var];
		else
			Debug.LogError("La variable string:[" + var + "] no existeix al GameState");
		return null;
	}
	
	public void SetString(string var, string value){
		if(stringVars.Contains(var))
			stringVars[var] = value;
		else
			Debug.LogError("La variable string:[" + var + "] no existeix al GameState");
	}
	
	public int GetInt(string var){
		if(intVars.Contains(var))
			return (int)intVars[var];
		else
			Debug.LogError("La variable int:[" + var + "] no existeix al GameState");
		return int.MinValue;
	}
	
	public void SetInt(string var, int value){
		if(intVars.Contains(var))
			intVars[var] = value;
		else
			Debug.LogError("La variable int:[" + var + "] no existeix al GameState");
	}
		
	public float GetFloat(string var){
		if(floatVars.Contains(var))
			return (float)floatVars[var];
		else
			Debug.LogError("La variable float:[" + var + "] no existeix al GameState");
		return float.NaN;
	}
	
	public void SetFloat(string var, float value){
		if(floatVars.Contains(var))
			floatVars[var] = value;
		else
			Debug.LogError("La variable float:[" + var + "] no existeix al GameState");
	}
	
	public bool GetBool(string var){
		if(booleanVars.Contains(var))
			return (bool)booleanVars[var];
		else
			Debug.LogError("La variable bool:[" + var + "] no existeix al GameState");
		return false;
	}
	
	public void SetBool(string var, bool value){
		if(booleanVars.Contains(var))
			booleanVars[var] = value;
		else
			Debug.LogError("La variable bool:[" + var + "] no existeix al GameState");
	}

	public Vector3 GetVector3(string var){
		if(floatVars.Contains(var+"X")&&floatVars.Contains(var+"Y")&&floatVars.Contains(var+"Z"))
			return new Vector3(GetFloat(var+"X"),GetFloat(var+"Y"),GetFloat(var+"Z"));
		else
			Debug.LogError("La variable Vector3:[" + var + "] no existeix al GameState");
		return Vector3.zero;
	}

	public void SetVector3(string var, Vector3 value){
		if(floatVars.Contains(var+"X")&&floatVars.Contains(var+"Y")&&floatVars.Contains(var+"Z")){
			SetFloat(var+"X",value.x);
			SetFloat(var+"Y",value.y);
			SetFloat(var+"Z",value.z);
		}
		else
			Debug.LogError("La variable Vector3:[" + var + "] no existeix al GameState");
	}

    //TODO:Afegir metodes AddInt, AddString, AddFloat, AddBool
    public void AddInt(string var, int value)
    {
        intVars.Add(var, value);
    }
    public void AddFloat(string var, float value)
    {
        floatVars.Add(var, value);
    }
    public void AddString(string var, string value)
    {
        stringVars.Add(var, value);
    }
    public void AddBool(string var, bool value)
    {
        booleanVars.Add(var, value);
    }


    
    //Metodes per comprovar si ja existeix la valiable a GameState
    public bool ExistsInt(string var)
    {
        return intVars.ContainsKey(var);
    }
    public bool ExistsFloat(string var)
    {
        return floatVars.ContainsKey(var);
    }
    public bool ExistsString(string var)
    {
        return stringVars.ContainsKey(var);
    }
    public bool ExistsBool(string var)
    {
        return booleanVars.ContainsKey(var);
    }
    public bool ExistsVector3(string var)
    {
        return ExistsFloat(var + "X") && ExistsFloat(var + "Y") && ExistsFloat(var + "Z");
    }

	public void AddVector3(string var, Vector3 value){
        AddFloat(var + "X", value.x);
        AddFloat(var + "Y", value.y);
        AddFloat(var + "Z", value.z);
	}
	/*
	 * Carrega el valors per defecte d'una nova partida
	 */
	public void GameNew(){
		stringVars.Clear();
		intVars.Clear();
		floatVars.Clear();
		booleanVars.Clear();
		//AddVector3("PlayerPos",new Vector3(10.0024f,3.67965f,8.96460f));
		//AddVector3("PlayerRot",new Vector3(0.0f,0.0f,0.0f));
		//intVars.Add("scene",4);
	}

	/*
	 * Salva els valors de l'estat de la partida
	 */
	public void GameSave(){
//		string arrayKeys = ""/*+PlayerPrefs.GetString ("arrayKeys")*/;
		string intKeys = null,boolKeys = null,floatKeys = null,stringKeys = null;
		foreach(DictionaryEntry vartemp in stringVars)
		{
			PlayerPrefs.SetString(vartemp.Key.ToString(), vartemp.Value.ToString());
			stringKeys += "/"+vartemp.Key;
//			Debug.Log("Guardant: [" + vartemp.Key + "] -> "  + vartemp.Value);
		}
		
		foreach(DictionaryEntry vartemp in intVars)
		{
			PlayerPrefs.SetInt(vartemp.Key.ToString(), int.Parse(vartemp.Value.ToString()));
			intKeys += "/"+vartemp.Key;
//			Debug.Log("Guardant: [" + vartemp.Key + "] -> "  + vartemp.Value);
		}
		foreach(DictionaryEntry vartemp in floatVars)
		{
			PlayerPrefs.SetFloat(vartemp.Key.ToString(), float.Parse(vartemp.Value.ToString()));
			floatKeys += "/"+vartemp.Key;
//			Debug.Log("Guardant: [" + vartemp.Key + "] -> "  + vartemp.Value);
		}
		foreach(DictionaryEntry vartemp in booleanVars)
		{
			PlayerPrefs.SetString(vartemp.Key.ToString(), vartemp.Value.ToString());
			boolKeys += "/"+vartemp.Key;
//			Debug.Log("Guardant: [" + vartemp.Key + "] -> "  + vartemp.Value);
		}

		PlayerPrefs.SetString ("stringKeys", stringKeys);
		PlayerPrefs.SetString ("intKeys", intKeys);
		PlayerPrefs.SetString ("floatKeys", floatKeys);
		PlayerPrefs.SetString ("boolKeys", boolKeys);
		//PlayerPrefs.SetString ("stringKeys", stringKeys);
	}

	/*
	 * Carrega els valors del GameState d'una partida anterior
	 */
	public void GameLoad(){
		stringVars.Clear();
		intVars.Clear();
		floatVars.Clear();
		booleanVars.Clear();
		//string arrayKeys = ""+PlayerPrefs.GetString ("arrayKeys");
		string stringKeys = PlayerPrefs.GetString ("stringKeys");
		string intKeys = PlayerPrefs.GetString ("intKeys");
		string floatKeys = PlayerPrefs.GetString ("floatKeys");
		string boolKeys = PlayerPrefs.GetString ("boolKeys");

//		string[] akeys = arrayKeys.Split('/');
		string[] skeys = stringKeys.Split('/');
		string[] ikeys = intKeys.Split('/');
		string[] fkeys = floatKeys.Split('/');
		string[] bkeys = boolKeys.Split('/');

//		foreach (string keyToLoad in keys)
//		{
//			string value = PlayerPrefs.GetString(keyToLoad);
//			variables [keyToLoad] = value;
//			Debug.Log("Carregant: [" + keyToLoad + "] -> "  + value);
//		}
		foreach (string keyToLoad in skeys)
		{
			if(keyToLoad!=""){
				string value = PlayerPrefs.GetString(keyToLoad);
				stringVars.Add(keyToLoad,value);
//				Debug.Log("Carregant: [" + keyToLoad + "] -> "  + value);
			}
		}
		foreach (string keyToLoad in ikeys)
		{
			if(keyToLoad!=""){
				int value = PlayerPrefs.GetInt(keyToLoad);
				intVars.Add(keyToLoad,value);
//				Debug.Log("Carregant: [" + keyToLoad + "] -> "  + value);
			}
		}
		foreach (string keyToLoad in fkeys)
		{
			if(keyToLoad!=""){
				float value = PlayerPrefs.GetFloat(keyToLoad);
				floatVars.Add(keyToLoad,value);
//				Debug.Log("Carregant: [" + keyToLoad + "] -> "  + value);
			}
		}
		foreach (string keyToLoad in bkeys)
		{
			if(keyToLoad!=""){
				bool value = bool.Parse(PlayerPrefs.GetString(keyToLoad));
				booleanVars.Add(keyToLoad,value);
//				Debug.Log("Carregant: [" + keyToLoad + "] -> "  + value);
			}
		}
	}

	public string ToString(){
		string ret = "";
		foreach(DictionaryEntry vartemp in stringVars)
		{
			ret = ret+ vartemp.Key.ToString()+": " +vartemp.Value.ToString()+"\n";
		}
		
		foreach(DictionaryEntry vartemp in intVars)
		{
			ret = ret + vartemp.Key.ToString()+": " +vartemp.Value.ToString()+"\n";
		}
		foreach(DictionaryEntry vartemp in floatVars)
		{
			ret = ret + vartemp.Key.ToString()+": " +vartemp.Value.ToString()+"\n";
		}
		foreach(DictionaryEntry vartemp in booleanVars)
		{
			ret = ret + vartemp.Key.ToString()+": " +vartemp.Value.ToString()+"\n";
		}
		return ret;
	}

}
