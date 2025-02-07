#if UNITY_LOCALIZATION
using UnityEngine;
using UnityEngine.Localization.Components;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Updates the local <see cref="SpriteTMP"/> component using 
    /// the localization update event (from Unity Localization System).
    /// </summary>
    /// <remarks>
    /// If your project uses another Localization System, use this class 
    /// as a base to create your own implementation.
    /// </remarks>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LocalizeStringEvent))]
    public sealed class LocalizedSpriteText : MonoBehaviour
    {
        [SerializeField, Tooltip("The local SpriteText component.")]
        private SpriteTMP spriteText;
        [SerializeField, Tooltip("The local Localization component.")]
        private LocalizeStringEvent localization;

        private void Reset()
        {
            spriteText = GetComponent<SpriteTMP>();
            localization = GetComponent<LocalizeStringEvent>();
        }

        private void OnEnable() => localization.OnUpdateString.AddListener(HandleLocalizationChanged);
        private void OnDisable() => localization.OnUpdateString.RemoveListener(HandleLocalizationChanged);

        private void HandleLocalizationChanged(string localizedText)
        {
            spriteText.SourceText = localizedText;
            if (InputSystem.LastDeviceType == InputDeviceType.None) return;
            spriteText.UpdateTextWithSpriteTags(InputSystem.LastDeviceType);
        }
    }
}
#endif