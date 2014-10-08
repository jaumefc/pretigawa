using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(ConversationNodeClass))]
[CanEditMultipleObjects]
public class ConversationNodeClassEditor : Editor 
{
	private ConversationNodeClass cnc;
	void Awake(){
		cnc = (ConversationNodeClass)target;
	}


	public override void OnInspectorGUI ()
	{
		//GUI.changed = false;
		cnc.ShowNode = (ConversationNodeClass.show)EditorGUILayout.EnumPopup("Show Node",cnc.ShowNode);
		switch (cnc.ShowNode)
		{
			case ConversationNodeClass.show.ALWAYS:
				//cnc.var = GUILayout.Toggle(false,"Variable");
				break;
			case ConversationNodeClass.show.IF:
			case ConversationNodeClass.show.IF_NOT:
				//cnc.var = GUILayout.Toggle(true,"Variable");
				cnc.var = EditorGUILayout.TextField("Variable",cnc.var);
				break;
		}
		cnc.sFirstOption = EditorGUILayout.TextField("sFirstOption",cnc.sFirstOption);
		cnc.sSecondOption = EditorGUILayout.TextField("sSecondOption",cnc.sSecondOption);
		cnc.cCharacter = (ConversationNodeClass.costume)EditorGUILayout.EnumPopup ("cCharacter", cnc.cCharacter);
		cnc.sSpeaker = (ConversationNodeClass.speaker)EditorGUILayout.EnumPopup ("sSpeaker", cnc.sSpeaker);
		cnc.fSeconds = EditorGUILayout.FloatField ("fSeconds", cnc.fSeconds);

		cnc.preAction = EditorGUILayout.TextField("PreAction",cnc.preAction);
		cnc.postAction = EditorGUILayout.TextField("PostAction",cnc.postAction);
		cnc.scriptAnimacio = (playAnimation)EditorGUILayout.ObjectField("Animacio",cnc.scriptAnimacio,typeof(playAnimation),true);
		cnc.clipAudio = (AudioClip)EditorGUILayout.ObjectField("Audio",cnc.clipAudio,typeof(AudioClip),true);
	
		cnc.showNodes = EditorGUILayout.Foldout (cnc.showNodes, "Nodes");
		

		if(cnc.showNodes)
		{
			int size = EditorGUILayout.IntField("Size",cnc.cncArray.Length);
			ConversationNodeClass[] aux = new ConversationNodeClass[size];
			for (int i = 0 ; i < size;i++)
			{
				if(i<cnc.cncArray.Length)
					aux[i] = (ConversationNodeClass)EditorGUILayout.ObjectField("Node "+i, cnc.cncArray[i], typeof(ConversationNodeClass), true);
				else
					aux[i] = (ConversationNodeClass)EditorGUILayout.ObjectField("Node "+i, null, typeof(ConversationNodeClass), true);
			}
			cnc.cncArray = aux;
		}
		if(GUI.changed)
			EditorUtility.SetDirty(cnc);
		//cnc.fSeconds = EditorGUILayout.FloatField ("fSeconds", cnc.fSeconds);
		//cnc.cncArray = EditorGUILayout.ObjectField("cncArray",cnc.cncArray,typeof(ConversationNodeClass[]),true);
	}
}