///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Unity Standard Hierarchy.
//
// The MIT License (MIT)
//
// Copyright (c) 2014 Ibuprogames
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//    
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor custom de Transform.
/// </summary>
[CustomEditor(typeof(Transform))]
public class TransformEditor : Editor
{
  private Transform thisTarget;

  private void OnEnable()
  {
    thisTarget = target as Transform;
  }

  public override void OnInspectorGUI()
  {
    if (thisTarget.transform.parent == null &&
       (string.CompareOrdinal("Behaviours", thisTarget.gameObject.name) == 0 ||
       string.CompareOrdinal("Cameras", thisTarget.gameObject.name) == 0 ||
       string.CompareOrdinal("Effects", thisTarget.gameObject.name) == 0 ||
       string.CompareOrdinal("Environment", thisTarget.gameObject.name) == 0))
    {
      EditorGUILayout.HelpBox("This special object CANNOT be transformed.", MessageType.Info);
    }
    else
    {
      EditorGUIUtility.LookLikeControls();
      EditorGUI.indentLevel = 0;

      Vector3 position = EditorGUILayout.Vector3Field("Position", thisTarget.localPosition);
      Vector3 eulerAngles = EditorGUILayout.Vector3Field("Rotation", thisTarget.localEulerAngles);
      Vector3 scale = EditorGUILayout.Vector3Field("Scale", thisTarget.localScale);

      EditorGUIUtility.LookLikeInspector();
      
      if (GUI.changed == true)
      {
        Undo.RegisterUndo(thisTarget, "Transform Change");
        
        thisTarget.localPosition = FixNaN(position);
        thisTarget.localEulerAngles = FixNaN(eulerAngles);
        thisTarget.localScale = FixNaN(scale);
      }
    }
  }

  private Vector3 FixNaN(Vector3 v)
  {
    if (float.IsNaN(v.x) == true)
      v.x = 0.0f;

    if (float.IsNaN(v.y) == true)
      v.y = 0.0f;

    if (float.IsNaN(v.z) == true)
      v.z = 0.0f;

    return v;
  }
}
