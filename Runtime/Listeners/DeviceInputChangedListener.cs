using UnityEngine;
using UnityEngine.Events;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Listener for when a device input changes.
    /// </summary>
    /// <remarks>
    /// Specify the <see cref="device"/> field and <see cref="OnDeviceChanged"/> event will be invoked using 
    /// a boolean parameter indicating whether the current device matches the specified device.<br/><br/>
    /// This can be useful for updating UI elements or triggering specific actions based on the active input device.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class DeviceInputChangedListener : MonoBehaviour
    {
        [Tooltip("The input device to listen for changes.")]
        public InputDeviceType device;

        [Space]
        [Tooltip("Invoked when the specified device is changed. The boolean parameter indicates whether the current device matches the specified device.")]
        public UnityEvent<bool> OnDeviceChanged;

        private void OnEnable() => InputSystem.OnDeviceInputChanged += HandleDeviceInputChanged;
        private void OnDisable() => InputSystem.OnDeviceInputChanged -= HandleDeviceInputChanged;

        private void HandleDeviceInputChanged(InputDeviceType device)
        {
            var isDevice = this.device == device;
            OnDeviceChanged?.Invoke(isDevice);
        }
    }
}