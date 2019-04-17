using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DuplicateAndSpaceObject))]
public class DuplicateAndSpace : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DuplicateAndSpaceObject DandS = (DuplicateAndSpaceObject)target;

        if (GUILayout.Button("Duplicate " + DandS.numberOfDuplications + " Times"))
        {
            Object prefabRoot = PrefabUtility.GetCorrespondingObjectFromOriginalSource(Selection.activeGameObject);

            if (prefabRoot != null)
                PrefabUtility.InstantiatePrefab(prefabRoot);
            else
                for (int i = 0; i < DandS.numberOfDuplications; i++)
                {
                    DandS.clones.Add(Instantiate(Selection.activeGameObject));
                }
        }

        if (GUILayout.Button("Delete all Clones"))
        {

            DandS.deleteClones();

        }

        if (GUILayout.Button("Stack Objects"))
        {
            DandS.stackObjects();

        }
        if (GUILayout.Button("Arrange Objects Sideways"))
        {
            DandS.arrangeSideways();

        }
        if (GUILayout.Button("Pattern"))
        {
            DandS.pattern();

        }

        if (GUILayout.Button("Spiral Pattern"))
        {
            DandS.spiralPattern();

        }
    }

}
