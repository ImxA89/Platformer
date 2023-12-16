using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(GameObjectOfTypeAttribute))]
public class GameObjectOfTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (CheckFieldIsGameObject() == false)
        {
            DrawError(position);
            return;
        }

        GameObjectOfTypeAttribute gameObjectOfTypeAttribute = attribute as GameObjectOfTypeAttribute;
        Type requiredType = gameObjectOfTypeAttribute.Type;

        CheckDragAndDrop(position, requiredType);
        CheckValue(property,requiredType);

        DrawObjectField(property, position, label, gameObjectOfTypeAttribute.AllowSceneObject);
    }

    private void CheckDragAndDrop(Rect position, Type requiredType)
    {
       if (position.Contains(Event.current.mousePosition)) 
        { 
            var draggedObjectCount = DragAndDrop.objectReferences.Length;

            for (int i = 0; i < draggedObjectCount; i++)
            {
                if (ValidateObject(DragAndDrop.objectReferences[i], requiredType) == false)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Rejected; 
                    break;
                }
            }
        }
    }

    private bool ValidateObject(UnityEngine.Object objectForCheck, Type requiredType)
    {
        bool isValid = false;

        GameObject gameObject = objectForCheck as GameObject;

        if (gameObject != null)
        {
            isValid = gameObject.GetComponent(requiredType) != null;
        }

        return isValid;
    }

    private bool CheckFieldIsGameObject()
    {
        return fieldInfo.FieldType == typeof(GameObject) ||
            typeof(IEnumerable<GameObject>).IsAssignableFrom(fieldInfo.FieldType);
    }

    private void DrawError(Rect position)
    {
        string error = "need GameObject";

        EditorGUI.HelpBox(position, error, MessageType.Error);
    }

    private void DrawObjectField(SerializedProperty property, Rect position, GUIContent label, bool allowSceneObjects)
    {
        property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue,
            typeof(GameObject), allowSceneObjects);
    }

    private void CheckValue(SerializedProperty property, Type requiredType)
    {
        if(property.objectReferenceValue == null)
        {
            if(ValidateObject(property.objectReferenceValue,requiredType) == false)
                property.objectReferenceValue = null;
        }
    }
}
