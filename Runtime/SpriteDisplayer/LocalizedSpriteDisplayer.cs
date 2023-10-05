#if UNITY_LOCALIZATION
using UnityEngine;
using UnityEngine.Localization.Components;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Updates the local <see cref="SpriteDisplayer"/> component when a 
    /// localization update event (from Unity Localization System) happens.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteDisplayer))]
    [RequireComponent(typeof(LocalizeStringEvent))]
    public sealed class LocalizedSpriteDisplayer : MonoBehaviour
    {
        [SerializeField, Tooltip("The local SpriteDisplayer component.")]
        private SpriteDisplayer displayer;
        [SerializeField, Tooltip("The local Localization component.")]
        private LocalizeStringEvent localization;

        private void Reset()
        {
            displayer = GetComponent<SpriteDisplayer>();
            localization = GetComponent<LocalizeStringEvent>();
        }

        private void OnEnable() => localization.OnUpdateString.AddListener(HandleLocalizationChanged);
        private void OnDisable() => localization.OnUpdateString.RemoveListener(HandleLocalizationChanged);

        private void HandleLocalizationChanged(string localizedText) =>
            displayer.UpgradeText(localizedText, InputSystem.LastDeviceType);
    }
}
#endif