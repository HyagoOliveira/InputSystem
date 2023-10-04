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
            var mapNameProperty = property.FindPropertyRelative(nameof(InputActionMapPopup.mapName));
            var assetName = property.FindPropertyRelative(nameof(InputActionMapPopup.assetName)).stringValue;
            var inputActionAssetProperty = property.serializedObject.FindProperty(assetName);
            var inputActionAsset = inputActionAssetProperty?.objectReferenceValue as InputActionAsset;

            var hasInvalidActionMaps = inputActionAsset == null || inputActionAsset.actionMaps.Count == 0;
            if (hasInvalidActionMaps)
            {
                DrawTextField(position, property, label, mapNameProperty);
                return;
            }

            var index = -1;
            var tags = new string[inputActionAsset.actionMaps.Count];

            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = inputActionAsset.actionMaps[i].name;

                if (index < 0 && tags[i].Equals(mapNameProperty.stringValue))
                    index = i;
            }

            if (index == -1) index = 0;

            label = EditorGUI.BeginProperty(position, label, property);
            index = EditorGUI.Popup(position, label.text, index, tags);
            EditorGUI.EndProperty();

            mapNameProperty.stringValue = tags[index];
        }

        private void DrawTextField(Rect position, SerializedProperty property, GUIContent label, SerializedProperty serializableProperty)
        {
            EditorGUI.BeginProperty(position, label, property);
            serializableProperty.stringValue = EditorGUI.TextField(position, label, serializableProperty.stringValue);
            EditorGUI.EndProperty();
        }
    }
}