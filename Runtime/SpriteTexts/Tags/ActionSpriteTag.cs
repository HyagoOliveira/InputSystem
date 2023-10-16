using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Data container for Sprite Tag using an <see cref="InputActionAsset"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "ActionSpriteTag", menuName = MENU_NAME + "Action Sprite Tag", order = 110)]
    public sealed class ActionSpriteTag : AbstractSpriteTag
    {
        [SerializeField, Tooltip("The Input Asset where your action is.")]
        private InputActionAsset inputAsset;
        [SerializeField, Tooltip("The Input Action.")]
        private InputActionPopup actionPopup = new(nameof(inputAsset));

        public override string GetTag(InputDeviceType device)
        {
            var action = inputAsset.FindAction(
                actionPopup.GetPath(),
                throwIfNotFound: true
            );
            var assetName = device.ToString();
            var inputBinding = device.GetInputBinding();
            var bidingIndex = action.GetBindingIndex(inputBinding);

            if (bidingIndex < 0) return string.Empty;

            var binding = action.GetBindingDisplayString(
                bidingIndex,
                out string _,
                out string controlPath
            );
            var spriteName = controlPath ?? binding.ToString();

            return GetTagUsingName(assetName, spriteName);
        }
    }
}