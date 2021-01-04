using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Replaces an image using the <see cref="inputPath"/> or <see cref="inputReference"/>.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public sealed class DeviceInputVisualizer : MonoBehaviour
    {
        [SerializeField, Tooltip("Local Image component.")]
        private Image image;
        [Tooltip("Device display set.")]
        public DeviceDisplaySet deviceSet;
        [Tooltip("Reference to the input path.")]
        public string inputPath;
        [Tooltip("Reference to the Input action. This will be used if Input Path is empty.")]
        public InputActionReference inputReference;

        private InputDeviceType lastDeviceType = InputDeviceType.None;

        private void Reset()
        {
            image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            UnityEngine.InputSystem.InputSystem.onActionChange += OnActionChange;
        }

        private void OnDisable()
        {
            UnityEngine.InputSystem.InputSystem.onActionChange -= OnActionChange;
        }

        private void OnActionChange(object obj, InputActionChange change)
        {
            if (change == InputActionChange.ActionPerformed)
            {
                var inputAction = (InputAction)obj;
                var device = inputAction?.activeControl?.device;
                if (device != null) OnDeviceChange(device);
            }
        }

        private void OnDeviceChange(InputDevice device)
        {
            var type = device.GetInputDeviceType();
            var sameDevice = lastDeviceType == type;
            if (sameDevice) return;

            var settings = deviceSet ? deviceSet.GetSettings(type) : null;
            if (settings == null) return;

            var hasInputPath = !string.IsNullOrEmpty(inputPath);
            var sprite = hasInputPath ?
                settings.GetSprite(inputPath) :
                settings.GetSprite(inputReference);
            if (sprite)
            {
                image.sprite = sprite;
                lastDeviceType = type;
            }
        }
    }
}