using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Serializable struct holding rumble data for haptic feedback.
    /// </summary>
    [System.Serializable]
    public struct RumbleData
    {
        [Tooltip("The speed of the low-frequency motor (left). Controls heavy, bass-like impacts."), Range(0f, 1f)]
        public float lowFrequency;
        [Tooltip("The speed of the high-frequency motor (right). Controls sharp, treble-like textures."), Range(0f, 1f)]
        public float highFrequency;
        [Tooltip("The total duration of the vibration effect in seconds."), Min(0f)]
        public float duration;

        public RumbleData(float lowFrequency, float highFrequency, float duration = 0.1F)
        {
            this.lowFrequency = lowFrequency;
            this.highFrequency = highFrequency;
            this.duration = duration;
        }
    }
}