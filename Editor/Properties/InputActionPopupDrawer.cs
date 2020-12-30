using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem.Editor
{
    [CustomPropertyDrawer(typeof(InputActionPopup))]
    public sealed class InputActionPopupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty mapNameProperty = property.FindPropertyRelative("mapName");
            SerializedProperty assetNameProperty = property.FindPropertyRelative("assetName");
            SerializedProperty actionNameProperty = property.FindPropertyRelative("actionName");
            SerializedProperty inputActionAssetProperty = property.serializedObject.FindProperty(assetNameProperty.stringValue);
            InputActionAsset inputActionAsset = inputActionAssetProperty?.objectReferenceValue as InputActionAsset;
            InputActionMap actionMap = inputActionAsset ?
                inputActionAsset.FindActionMap(mapNameProperty.stringValue) : null;

            if (actionMap != null && actionMap.actions.Count > 0)
            {
                int index = -1;
                string[] tags = new string[actionMap.actions.Count];

                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = actionMap.actions[i].name;

                    if (index < 0 && tags[i] == actionNameProperty.stringValue)
                    {
                        index = i;
                    }
                }

                if (index > -1 && index < actionMap.actions.Count)
                {
                    label = EditorGUI.BeginProperty(position, label, property);
                    index = EditorGUI.Popup(position, label.text, index, tags);
                    EditorGUI.EndProperty();

                    actionNameProperty.stringValue = tags[index];
                }
                else DrawTextField(position, property, label, actionNameProperty);
            }
            else DrawTextField(position, property, label, actionNameProperty);
        }

        private void DrawTextField(Rect position, SerializedProperty property, GUIContent label, SerializedProperty serializableProperty)
        {
            EditorGUI.BeginProperty(position, label, property);
            serializableProperty.stringValue = EditorGUI.TextField(position, label, serializableProperty.stringValue);
            EditorGUI.EndProperty();
        }
    }
}