using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
	private void OnSceneGUI()
	{
		var fow = (FieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);

		Handles.color = Color.red;
		if (fow.Target != null)
			Handles.DrawLine(fow.transform.position, fow.Target.position);
	}

}