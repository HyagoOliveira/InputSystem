#if LOCALIZATION
using UnityEngine;
using UnityEngine.Localization.Components;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Replaces any occurrences of {inputName} by the corresponding TextMeshPro Sprite tag in a Localization String.
    /// <para>
    /// This components works exactly as <see cref="SpriteDisplayer"/> but using the Unity localization system.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(LocalizeStringEvent))]
    public sealed class LocalizedSpriteDisplayer : MonoBehaviour
    {
        [Space]
        [SerializeField, Tooltip("The local Localization component.")]
        private LocalizeStringEvent localization;

        private void Reset() => localization = GetComponent<LocalizeStringEvent>();
        private void OnEnable() => localization.OnUpdateString.AddListener(HandleLocalizationChanged);
        private void OnDisable() => localization.OnUpdateString.RemoveListener(HandleLocalizationChanged);

        private void HandleLocalizationChanged(string localizedText) { }
    }
}
#endif