#if UNITY_LOCALIZATION
using UnityEngine;
using UnityEngine.Localization.Components;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Updates any implementation of a local <see cref="AbstractSpriteText"/> 
    /// component when a localization update event (from Unity Localization System) happens.
    /// </summary>
    /// <remarks>
    /// if your project uses another Localization System, use this class as a base to create 
    /// your own implementation.
    /// </remarks>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LocalizeStringEvent))]
    public sealed class LocalizedSpriteText : MonoBehaviour
    {
        [SerializeField, Tooltip("The local SpriteText component.")]
        private AbstractSpriteText spriteText;
        [SerializeField, Tooltip("The local Localization component.")]
        private LocalizeStringEvent localization;

        private void Reset()
        {
            spriteText = GetComponent<AbstractSpriteText>();
            localization = GetComponent<LocalizeStringEvent>();
        }

        private void OnEnable() => localization.OnUpdateString.AddListener(HandleLocalizationChanged);
        private void OnDisable() => localization.OnUpdateString.RemoveListener(HandleLocalizationChanged);

        private void HandleLocalizationChanged(string localizedText)
        {
            spriteText.OriginalText = localizedText;
            spriteText.UpdateTextWithSpriteTag(InputSystem.LastDeviceType);
        }
    }
}
#endif