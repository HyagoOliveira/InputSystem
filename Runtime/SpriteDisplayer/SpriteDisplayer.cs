using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Replaces all occurrences of <see cref="inputField"/> by the corresponding TextMeshPro Sprite tag.
    /// </summary>
    /// <remarks>
    /// Place this component into the same GameObject containing a <see cref="TMP_Text"/> component and set its text field to:
    /// 
    /// <code>
    /// Press {input} to move the character.
    /// </code>
    /// 
    /// Next, set the <see cref="inputAsset"/> and <see cref="actionPopup"/> fields.<br/>
    /// At runtime, all occurrences of  <b>{input}</b> will be replaced by a Sprite tag 
    /// corresponding to the <see cref="actionPopup"/> you have selected.
    /// </remarks>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public sealed class SpriteDisplayer : MonoBehaviour
    {
        [SerializeField, Tooltip("The local TextMeshPro component.")]
        private TMP_Text textMeshPro;

        [Header("Input")]
        [SerializeField, Tooltip("The Input Asset where your action is.")]
        private InputActionAsset inputAsset;
        [SerializeField, Tooltip("The Input Action.")]
        private InputActionPopup actionPopup = new InputActionPopup(nameof(inputAsset));

        [Header("Text")]
        [SerializeField, Tooltip("The input field that will be replaced by the TMP Sprite Tag.")]
        private string inputField = "{input}";

        private InputAction action;
        private string originalText;

        private void Reset() => textMeshPro = GetComponent<TMP_Text>();

        private void Awake()
        {
            originalText = textMeshPro.text;
            action = inputAsset.FindAction(actionPopup.GetPath(), throwIfNotFound: true);
        }

        private void OnEnable() => InputSystem.OnDeviceInputChanged += HandleDeviceInputChanged;
        private void OnDisable() => InputSystem.OnDeviceInputChanged -= HandleDeviceInputChanged;

        public void UpgradeText(string text, InputDeviceType device) =>
            textMeshPro.text = ReplaceTextWithSpriteTag(text, device);

        private void HandleDeviceInputChanged(InputDeviceType device) => UpgradeText(originalText, device);

        private string ReplaceTextWithSpriteTag(string text, InputDeviceType device)
        {
            var assetName = device.ToString();

            var inputBinding = SpriteRegex.GetInputBinding(device);
            var bidingIndex = action.GetBindingIndex(inputBinding);

            if (bidingIndex < 0) return text;

            var binding = action.GetBindingDisplayString(
                bidingIndex,
                out string _,
                out string controlPath
            );
            var spriteName = controlPath ?? binding.ToString();

            return ReplaceText(text, assetName, spriteName);
        }

        private string ReplaceText(string text, string assetName, string spriteName) =>
            text.Replace(inputField, GetSpriteTag(assetName, spriteName));

        private static string GetSpriteTag(string assetName, string spriteName) =>
            $"<sprite=\"{assetName}\" name=\"{spriteName}\">";
    }
}