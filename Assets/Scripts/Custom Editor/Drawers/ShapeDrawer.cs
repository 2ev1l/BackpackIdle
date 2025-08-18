using EditorCustom.Attributes;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Universal.Core;

namespace EditorCustom
{
    [CustomPropertyDrawer(typeof(Shape))]
    public class ShapeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty widthProp = property.FindPropertyRelative("width");
            SerializedProperty heightProp = property.FindPropertyRelative("height");
            SerializedProperty gridProp = property.FindPropertyRelative("shapeGrid");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.BeginChangeCheck();

            float fieldWidth = 50f;
            float spacing = 5f;

            Rect widthRect = new Rect(position.x, position.y, fieldWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(widthRect, widthProp, GUIContent.none);

            Rect heightRect = new Rect(position.x + fieldWidth + spacing, position.y, fieldWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(heightRect, heightProp, GUIContent.none);

            if (EditorGUI.EndChangeCheck())
            {
                if (widthProp.intValue < 1) widthProp.intValue = 1;
                if (heightProp.intValue < 1) heightProp.intValue = 1;
                gridProp.arraySize = widthProp.intValue * heightProp.intValue;
            }

            float checkboxSize = 20f;
            Rect gridRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + spacing,
                                     widthProp.intValue * checkboxSize, heightProp.intValue * checkboxSize);

            for (int y = 0; y < heightProp.intValue; y++)
            {
                for (int x = 0; x < widthProp.intValue; x++)
                {
                    int index = y * widthProp.intValue + x;
                    if (index < gridProp.arraySize)
                    {
                        Rect checkboxRect = new Rect(gridRect.x + x * checkboxSize, gridRect.y + y * checkboxSize, checkboxSize, checkboxSize);
                        SerializedProperty boolProp = gridProp.GetArrayElementAtIndex(index);
                        boolProp.boolValue = EditorGUI.Toggle(checkboxRect, "", boolProp.boolValue);
                    }
                }
            }

            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty heightProp = property.FindPropertyRelative("height");
            float baseHeight = EditorGUIUtility.singleLineHeight;
            float gridHeight = heightProp.intValue * 20f;
            float spacing = 5f;

            return baseHeight + gridHeight + spacing;
        }

    }
}