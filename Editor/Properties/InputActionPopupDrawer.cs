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
            var mapNameProperty = property.FindPropertyRelative(nameof(InputActionPopup.mapName));
            var actionNameProperty = property.FindPropertyRelative(nameof(InputActionPopup.actionName));
            var assetName = property.FindPropertyRelative(nameof(InputActionPopup.assetName)).stringValue;
            var inputActionAssetProperty = property.serializedObject.FindProperty(assetName);
            var inputActionAsset = inputActionAssetProperty?.objectReferenceValue as InputActionAsset;
            var actionMap = inputActionAsset ? inputActionAsset.FindActionMap(mapNameProperty.stringValue) : null;
            var hasInvalidActionMap = actionMap == null || actionMap.actions.Count == 0;

            if (hasInvalidActionMap)
            {
                DrawTextField(position, property, label, actionNameProperty);
                return;
            }

            var index = -1;
            var tags = new string[actionMap.actions.Count];

            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = actionMap.actions[i].name;

                if (index < 0 && tags[i] == actionNameProperty.stringValue)
                    index = i;
            }

            if (index == -1) index = 0;

            label = EditorGUI.BeginProperty(position, label, property);
            index = EditorGUI.Popup(position, label.text, index, tags);
            EditorGUI.EndProperty();

            actionNameProperty.stringValue = tags[index];
        }

        private void DrawTextField(Rect position, SerializedProperty property, GUIContent label, SerializedProperty serializableProperty)
        {
            EditorGUI.BeginProperty(position, label, property);
            serializableProperty.stringValue = EditorGUI.TextField(position, label, serializableProperty.stringValue);
            EditorGUI.EndProperty();
        }
    }
}