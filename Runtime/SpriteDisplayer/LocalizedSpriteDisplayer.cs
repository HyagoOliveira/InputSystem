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
    public sealed class LocalizedSpriteDisplayer : SpriteDisplayer
    {
        [Space]
        [SerializeField, Tooltip("The local Localization component.")]
        private LocalizeStringEvent localization;

        protected override void Reset()
        {
            base.Reset();
            localization = GetComponent<LocalizeStringEvent>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            localization.OnUpdateString.AddListener(HandleLocalizationChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            localization.OnUpdateString.RemoveListener(HandleLocalizationChanged);
        }

        private void HandleLocalizationChanged(string localizedText)
        {
            originalText = localizedText;
            HandleDeviceInputChanged(InputSystem.LastDeviceType);
        }
    }
}
#endif