#if UI_TOOLKIT
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Localization;

namespace ActionCode.InputSystem
{
    [RequireComponent(typeof(SpriteUITK))]
    [DefaultExecutionOrder(SpriteUITK.EXECUTION_ORDER + 1)]
    public sealed class LocalizedSpriteUITK : MonoBehaviour
    {
        [SerializeField, Tooltip("The local SpriteUITK component.")]
        private SpriteUITK spriteText;

        private void Reset() => spriteText = GetComponent<SpriteUITK>();
        private void OnEnable() => BindLabelStringChangedEvents();

        private void BindLabelStringChangedEvents()
        {
            foreach (var label in spriteText.SourceTexts.Keys)
            {
                BindStringChangedEvent(label);
            }
        }

        private void BindStringChangedEvent(Label label)
        {
            var hasBinding = TryGetLocalizedStringBinding(label, out var binding);
            if (!hasBinding) return;

            binding.StringChanged += value => SetSourceText(label, value);
        }

        private async void SetSourceText(Label label, string text)
        {
            spriteText.SetLabelsVisibility(false);

            // For some reason, we must wait one frame before update the Label Text
            await Awaitable.NextFrameAsync();
            spriteText.SetSourceText(label, text);

            spriteText.SetLabelsVisibility(true);
        }

        private static bool TryGetLocalizedStringBinding(Label label, out LocalizedString localizedBinding)
        {
            localizedBinding = null;
            var hasBinding = label.TryGetBinding("text", out var binding);
            if (!hasBinding) return false;

            localizedBinding = binding as LocalizedString;
            return localizedBinding != null;
        }
    }
}
#endif