using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Applies a better Dead Zone to a stick input by clamping each axis individually
    /// and truncating the values to two decimal places.
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public sealed class ImprovedStickDeadzoneProcessor : InputProcessor<Vector2>
    {
        [Tooltip("The minimum value allowed for this stick.")]
        public float min = 0.255f;

        [Tooltip("The maximum value allowed for this stick. Values above it will be clamped to 1.")]
        public float max = 0.925f;

#if UNITY_EDITOR
        static ImprovedStickDeadzoneProcessor() => Initialize();
#endif

        [RuntimeInitializeOnLoadMethod]
        static void Initialize() => UnityEngine.InputSystem.
            InputSystem.RegisterProcessor<TruncateStickProcessor>("Stick Deadzone (Improved)");

        public override Vector2 Process(Vector2 value, InputControl _) => new(
            Clamp(value.x, min, max),
            Clamp(value.y, min, max)
        );

        /// <summary>
        /// Clamps the given value between min and max.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value allowed for this stick.</param>
        /// <param name="max">The maximum value allowed for this stick. Values above it will be clamped to 1.</param>
        /// <returns>A clamped Vector2.</returns>
        public static float Clamp(float value, float min, float max)
        {
            var absValue = Mathf.Abs(value);
            if (absValue < min) return 0f;
            if (absValue > max) return Mathf.Sign(value);
            return Mathf.Sign(value) * TruncateBy2(absValue);
        }

        /// <summary>
        /// Truncates the given analog input value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float TruncateAnalogInput(float value)
        {
            const float analogTopRightInput = 0.98f;
            if (value > analogTopRightInput) return 1f;
            return Mathf.Clamp01(value);
        }

        private static float TruncateBy2(float value) => (float)System.Math.Truncate(value * 100) / 100;
    }
}