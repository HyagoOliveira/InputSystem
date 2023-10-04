using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Replaces any occurrences of {inputName} by the corresponding TextMeshPro Sprite tag.
    /// <para>
    /// Place this component into the same GameObject containing a TextMeshPro and set its text field to:
    /// <br/><br/>
    /// <c>Press <b>{move}</b> to move the character.</c>
    /// <br/><br/>
    /// The <b>move</b> is the name of the Input Action on the <see cref="inputAsset"/> field.
    /// This name can be lower, upper or camel case.<br/>
    /// It's only important that this name matches the one into your action inside your InputAsset.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public class SpriteDisplayer : MonoBehaviour
    {
        [SerializeField, Tooltip("The local TextMeshPro component.")]
        private TMP_Text textMeshPro;
        [SerializeField, Tooltip("The input asset where your input name is.")]
        private InputActionAsset inputAsset;
        [SerializeField, Tooltip("The input action map inside your InputAsset.")]
        private InputActionMapPopup actionMapPopup = new InputActionMapPopup(nameof(inputAsset));

        protected string originalText;
        private InputActionMap actionMap;

        protected virtual void Reset() => textMeshPro = GetComponent<TMP_Text>();

        private void Awake()
        {
            originalText = textMeshPro.text;
            actionMap = inputAsset.FindActionMap(actionMapPopup.mapName, throwIfNotFound: true);
        }

        protected virtual void OnEnable() => InputSystem.OnDeviceInputChanged += HandleDeviceInputChanged;
        protected virtual void OnDisable() => InputSystem.OnDeviceInputChanged -= HandleDeviceInputChanged;

        protected void HandleDeviceInputChanged(InputDeviceType device) =>
            textMeshPro.text = GetTextWithSpriteTags(device);

        private string GetTextWithSpriteTags(InputDeviceType device)
        {
            var text = originalText;
            var assetName = device.ToString();
            var matches = SpriteRegex.GetMatches(originalText);
            var inputBinding = SpriteRegex.GetInputBinding(device);

            foreach (var match in matches)
            {
                var actionTag = match.Groups[0].Value;
                var actionName = match.Groups[1].Value;
                var hasAction = actionMap.TryFindAction(actionName, out InputAction action);

                if (!hasAction) continue;

                var bidingIndex = action.GetBindingIndex(inputBinding);
                if (bidingIndex < 0) continue;

                var binding = action.GetBindingDisplayString(
                    bidingIndex,
                    out string _,
                    out string controlPath
                );
                var spriteName = controlPath ?? binding.ToString();
                var spriteTag = SpriteRegex.GetSpriteTag(assetName, spriteName);

                text = text.Replace(actionTag, spriteTag);
            }

            return text;
        }
    }
}