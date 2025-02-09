#if UI_TOOLKIT
using UnityEngine;
using ActionCode.SerializedDictionaries;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections.Generic;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Displays multiple Sprite Tags into Labels inside a local <see cref="UIDocument"/> by replacing all 
    /// occurrences of the keys from <see cref="inputSpriteTags"/> dictionary by its Sprite Tag.
    /// </summary>
    /// <remarks>
    /// Place this component into the same GameObject containing a UIDocument and set any Label from the Source Asset as:
    /// 
    /// <code>Press {jump} to jump.</code>
    /// 
    /// Next, set the <see cref="inputSpriteTags"/>, adding an entry with <c>{jump}</c> as key and an appropriated Sprite Tag asset.
    /// At runtime, all occurrences of <c>{jump}</c> will be replaced by this Sprite Tag.
    /// <br/>
    /// You can create any Sprite Tag asset using the creating menu, ActionCode/Input System/.
    /// 
    /// <para> 
    /// <b>Tip</b>: if your project uses the Localization System provided by Unity, also attach 
    /// the LocalizedSpriteUITK component in the same GameObject.
    /// </para>
    /// </remarks>
    [RequireComponent(typeof(UIDocument))]
    [DefaultExecutionOrder(EXECUTION_ORDER)]
    public sealed class SpriteUITK : MonoBehaviour
    {
        [SerializeField, Tooltip("The local UI Document component.")]
        private UIDocument document;
        [SerializeField, Tooltip("The input sprite tags dictionary.")]
        private SerializedDictionary<string, AbstractSpriteTag> inputSpriteTags = new();
        [SerializeField]
        private string className = "sprite-label";

        /// <summary>
        /// The document Root Visual Element.
        /// </summary>
        public VisualElement Root => document.rootVisualElement;

        /// <summary>
        /// The Default Sprite Asset Path for this UI Document.
        /// </summary>
        public string SpriteAssetPath => document.panelSettings.textSettings.defaultSpriteAssetPath;

        /// <summary>
        /// The current labels source text without any Sprite Tag.
        /// </summary>
        public Dictionary<Label, string> SourceTexts { get; private set; }

        /// <summary>
        /// The execution order from this component.
        /// </summary>
        public const int EXECUTION_ORDER = 0;

        private void Reset() => document = GetComponent<UIDocument>();
        private void Awake() => SpriteAssetFinder.UITKDefaultSpriteAssetPath = SpriteAssetPath;

        private void OnEnable()
        {
            FindSourceTexts();
            InputSystem.OnDeviceInputChanged += HandleDeviceInputChanged;
        }

        private void OnDisable()
        {
            SourceTexts.Clear();
            InputSystem.OnDeviceInputChanged -= HandleDeviceInputChanged;
        }

        /// <summary>
        /// Sets the source text from the given label.
        /// </summary>
        /// <param name="label">The source text label.</param>
        /// <param name="text">A text without any Sprite Tag.</param>
        public void SetSourceText(Label label, string text)
        {
            SourceTexts[label] = text;
            if (InputSystem.LastDeviceType != InputDeviceType.None)
                UpdateTextsWithSpriteTags(InputSystem.LastDeviceType);
        }

        /// <summary>
        /// Sets all Labels visibility,
        /// </summary>
        /// <param name="visible">Whether is visible.</param>
        public void SetLabelsVisibility(bool visible)
        {
            foreach (var label in SourceTexts.Keys)
            {
                label.visible = visible;
            }
        }

        private void UpdateTextsWithSpriteTags(InputDeviceType device)
        {
            SpriteAssetFinder.TryUpdateToAvailableDevice(ref device);

            foreach (var (label, sourceText) in SourceTexts)
            {
                label.text = GetTextWithSpriteTags(sourceText, device);
            }
        }

        private string GetTextWithSpriteTags(string text, InputDeviceType device)
        {
            foreach (var (tag, sprite) in inputSpriteTags)
            {
                text = text.Replace(tag, sprite.GetTag(device));
            }

            return text;
        }

        private void FindSourceTexts()
        {
            SourceTexts = Root.Query<Label>(className: className)
                .ToList()
                .ToDictionary(label => label, label => label.text);
        }

        private void HandleDeviceInputChanged(InputDeviceType device) => UpdateTextsWithSpriteTags(device);
    }
}
#endif