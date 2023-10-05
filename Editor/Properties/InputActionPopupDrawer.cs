using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
            var actionMaps = inputActionAsset != null ? inputActionAsset.actionMaps : default;

            if (actionMaps.Count == 0)
            {
                DrawTextField(position, property, label, mapNameProperty, actionNameProperty);
                return;
            }

            var index = -1;
            var selectedIndex = -1;
            var actionMapPaths = new List<string>();
            var actionMapNames = new List<ActionMapName>();
            var currentActionPath = InputActionPopup.BuildPath(
                mapNameProperty.stringValue,
                actionNameProperty.stringValue
            );

            foreach (var map in actionMaps)
            {
                foreach (var action in map.actions)
                {
                    var actionMapName = new ActionMapName(map.name, action.name);
                    var actionMapNamePath = actionMapName.GetPath();

                    actionMapNames.Add(actionMapName);
                    actionMapPaths.Add(actionMapNamePath);

                    index++;
                    if (selectedIndex < 0 && actionMapNamePath.Equals(currentActionPath))
                        selectedIndex = index;
                }
            }

            if (selectedIndex == -1) selectedIndex = 0;

            label = EditorGUI.BeginProperty(position, label, property);
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, actionMapPaths.ToArray());
            EditorGUI.EndProperty();

            mapNameProperty.stringValue = actionMapNames[selectedIndex].mapName;
            actionNameProperty.stringValue = actionMapNames[selectedIndex].actionName;
        }

        private void DrawTextField(
            Rect position,
            SerializedProperty property,
            GUIContent label,
            SerializedProperty mapNameProperty,
            SerializedProperty actionNameProperty
        )
        {
            var currentActionPath = InputActionPopup.BuildPath(
                mapNameProperty.stringValue,
                actionNameProperty.stringValue
            );

            EditorGUI.BeginProperty(position, label, property);
            var text = EditorGUI.TextField(position, label, currentActionPath);
            EditorGUI.EndProperty();

            var texts = text.Split("/");
            mapNameProperty.stringValue = texts[0];
            actionNameProperty.stringValue = texts[1];
        }

        private readonly struct ActionMapName
        {
            public readonly string mapName;
            public readonly string actionName;

            public ActionMapName(string mapName, string actionName)
            {
                this.mapName = mapName;
                this.actionName = actionName;
            }

            public string GetPath() => InputActionPopup.BuildPath(mapName, actionName);
        }
    }
}