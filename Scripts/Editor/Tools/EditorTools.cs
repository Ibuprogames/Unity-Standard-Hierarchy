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
/// Useful tools for editor.
/// </summary>
public static class EditorTools
{
  /// <summary>
  /// Build the standard hierarchy of a scene.
  /// </summary>
  public static void BuildStandardHierarchy()
  {
    if (EditorUtility.DisplayDialog("Build standard hierarchy",
                                    "A standard hierarchy will be created in the current scene." +
                                    "It should only be made in new scenes.\r\n\r\n" + 
                                    "Do you want to continue?",
                                    "OK", "Cancel") == true)
    {
      var folders = new string[] { "Behaviours", "Cameras", "Effects", "Environment" };
      for (int i = 0; i < folders.Length; ++i)
      {
        GameObject folder = GameObject.Find(folders[i]);
        if (folder == null)
        {
          folder = new GameObject(folders[i]);
          folder.transform.position = Vector3.zero;
        }

        // Move "Main Camera" to "Cameras" folder.
        if (folders[i] == "Cameras")
        {
          GameObject camera = GameObject.Find("Main Camera");
          if (camera != null && camera.transform.parent == null)
            camera.transform.parent = folder.transform;
        }
      }
    }
  }
}
