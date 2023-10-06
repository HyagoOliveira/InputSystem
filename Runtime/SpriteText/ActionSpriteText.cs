using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Replaces all occurrences of <c>inputField</c> by the corresponding TextMeshPro Sprite tag.
    /// </summary>
    /// <remarks>
    /// Place this component into the same GameObject containing a <c>TextMeshPro</c> component and set its text field to:
    /// 
    /// <code>
    /// Press {input} to jump.
    /// </code>
    /// 
    /// Next, set the <see cref="inputAsset"/> and <see cref="actionPopup"/> fields.<br/>
    /// At runtime, all occurrences of  <c>{input}</c> will be replaced by a Sprite tag 
    /// corresponding to the <see cref="actionPopup"/> you have selected.
    /// 
    /// <para> 
    /// <b>Tip</b>: if your project uses the Localization System provided by Unity, also 
    /// attach the <see cref="LocalizedSpriteText"/> component in the same GameObject.
    /// </para>
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class ActionSpriteText : AbstractSpriteText
    {
        [Header("Inputs")]
        [SerializeField, Tooltip("The Input Asset where your action is.")]
        private InputActionAsset inputAsset;
        [SerializeField, Tooltip("The Input Action.")]
        private InputActionPopup actionPopup = new(nameof(inputAsset));

        private InputAction action;

        protected override void Awake()
        {
            base.Awake();
            action = inputAsset.FindAction(actionPopup.GetPath(), throwIfNotFound: true);
        }

        protected override string GetTextWithSpriteTag(InputDeviceType device)
        {
            var assetName = device.ToString();
            var inputBinding = device.GetInputBinding();
            var bidingIndex = action.GetBindingIndex(inputBinding);

            if (bidingIndex < 0) return OriginalText;

            var binding = action.GetBindingDisplayString(
                bidingIndex,
                out string _,
                out string controlPath
            );
            var spriteName = controlPath ?? binding.ToString();
            var spriteTag = SpriteTag.GetSpriteTagUsingName(assetName, spriteName);

            return OriginalText.Replace(inputField, spriteTag);
        }
    }
}