using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Utils;
using UnityEngine;


/*
 *     This Class Makes Prefabs from all the gameobject that 
 *     have been selected
 */


public class MakePrefabs : EditorWindow
{


    //  Adds the 'Make Prefab' menu command to the 'Project Tools' menu item
    [MenuItem("Project Tools/Make Prefab(s)")]
    //  NOTE -- The first static method that follows a menu item is the default command

    static void GeneratePrefabs()
    {
        PrefabsFromSelectedGameObjects();
    }



    //Prints a test message to the console
    // -- used for testing that a method was called inside of Unity
    static void TestCommand()
    {
        Debug.Log("Test Message");
    }



    static void PrefabsFromSelectedGameObjects()
    {
        foreach (GameObject targetObj in Selection.gameObjects) //loop through selected GameObjects
        {
            string name = targetObj.name;
            string localPath = "Assets/" + name + ".prefab"; //create the path for the prefab

            //check if a prefab exists with the same name
            if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
            {
                //check if user wants to replace the prefab
                if (EditorUtility.DisplayDialog("Prefab already exists",
                                                "What do you want to do?",
                                                        "Replace Current Prefab",
                                                        "Cancel"))
                {
                    //create new prefab that overwrites the current prefab
                    createPrefab(targetObj, localPath);
                }

            }
            else
            {
                createPrefab(targetObj, localPath);
            }





            //Prints the name and location of the new prefab in the console
            Debug.Log("New prefab: " + name + "\n" + "At: " + localPath);

        }



    }

    //makes a prefab from the GameObject "inputForPrefab" at the location "localPath"
    static void createPrefab(GameObject inputForPrefab, string localPath)
    {
        //Makes the new prefab
        GameObject prefab = PrefabUtility.CreatePrefab(localPath, inputForPrefab);

        //Instantiates the new prefab object in the scene
        prefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        //Deletes the Selected Object
        DestroyImmediate(inputForPrefab);


        AssetDatabase.Refresh();  //referesh the database
    }

    //test function to check that the selection works
    static void PrintSelectionToConsole()
    {
        foreach (GameObject targetObj in Selection.gameObjects)
        {
            Debug.Log(targetObj.name);
        }

    }



}
