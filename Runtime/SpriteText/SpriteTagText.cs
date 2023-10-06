using UnityEngine;
using ActionCode.SerializedDictionaries;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Replaces all occurrences of <c>inputField</c> by the corresponding TextMeshPro Sprite tag found at <see cref="tags"/>.
    /// </summary>
    /// <remarks>
    /// Place this component into the same GameObject containing a <c>TextMeshPro</c> component and set its text field to:
    /// 
    /// <code>
    /// Press {input} to move the character.
    /// </code>
    /// 
    /// Next, set the <see cref="tags"/>, specifying a Sprite tag by each input device you're going to use. 
    /// You can set this tag by name or animation.
    /// <br/><br/>
    /// At runtime, all occurrences of  <c>{input}</c> will be replaced by a Sprite tag 
    /// corresponding to the input device you have selected.
    /// 
    /// <para> 
    /// <b>Tip</b>: if your project uses the Localization System provided by Unity, also 
    /// attach the <see cref="LocalizedSpriteText"/> component in the same GameObject.
    /// </para>
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class SpriteTagText : AbstractSpriteText
    {
        [SerializeField, Tooltip("The Sprite Tags by each Input Device.")]
        private SerializedDictionary<InputDeviceType, SpriteTag> tags = new();

        protected override string GetTextWithSpriteTag(InputDeviceType device)
        {
            var hasTag = tags.TryGetValue(device, out var tag);
            if (!hasTag) return OriginalText;

            var assetName = device.ToString();
            var spriteTag = tag.GetSpriteTag(assetName);

            return OriginalText.Replace(inputField, spriteTag);
        }
    }
}