using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof (PlayerProperties))]

public class PlayerPropertiesInspector : Editor
{
	PlayerProperties playerProps;

	bool foldout1 = true;

	

	
	void OnInspectorGUI() 
	{ 
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("-----Content-----");
		EditorGUILayout.EndHorizontal();
	}

	
	
	void OnInspectorUpdate() 
	{
		this.Repaint();
	}
}
