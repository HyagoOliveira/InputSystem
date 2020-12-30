using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem.Editor
{
    [CustomPropertyDrawer(typeof(InputActionMapPopup))]
    public sealed class InputActionMapPopupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty mapNameProperty = property.FindPropertyRelative("mapName");
            SerializedProperty assetNameProperty = property.FindPropertyRelative("assetName");
            SerializedProperty inputActionAssetProperty = property.serializedObject.FindProperty(assetNameProperty.stringValue);
            InputActionAsset inputActionAsset = inputActionAssetProperty?.objectReferenceValue as InputActionAsset;

            if (inputActionAsset && inputActionAsset.actionMaps.Count > 0)
            {
                int index = -1;
                string[] tags = new string[inputActionAsset.actionMaps.Count];

                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = inputActionAsset.actionMaps[i].name;

                    if (index < 0 && tags[i] == mapNameProperty.stringValue)
                    {
                        index = i;
                    }
                }

                if (index > -1 && index < inputActionAsset.actionMaps.Count)
                {
                    label = EditorGUI.BeginProperty(position, label, property);
                    index = EditorGUI.Popup(position, label.text, index, tags);
                    EditorGUI.EndProperty();

                    mapNameProperty.stringValue = tags[index];
                }
                else DrawTextField(position, property, label, mapNameProperty);
            }
            else DrawTextField(position, property, label, mapNameProperty);
        }

        private void DrawTextField(Rect position, SerializedProperty property, GUIContent label, SerializedProperty serializableProperty)
        {
            EditorGUI.BeginProperty(position, label, property);
            serializableProperty.stringValue = EditorGUI.TextField(position, label, serializableProperty.stringValue);
            EditorGUI.EndProperty();
        }
    }
}