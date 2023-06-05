using UnityEngine;
using UnityInputSystem = UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Clamps a Stick input axis between a minimum and maximum value.
    /// <para>
    /// This Deazone Processor works better than <see cref="UnityInputSystem.Processors.StickDeadzoneProcessor"/>
    /// by only outputting values when they're over than the <see cref="min"/> value.
    /// </para>
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public sealed class StickDeadzoneClampedProcessor : UnityInputSystem.InputProcessor<Vector2>
    {
        [Tooltip("Minimum absolute value allowed for this stick.")]
        public float min = UnityInputSystem.InputSystem.settings.defaultDeadzoneMin;
        [Tooltip("Maximum absolute value allowed for this stick.")]
        public float max = UnityInputSystem.InputSystem.settings.defaultDeadzoneMax;

#if UNITY_EDITOR
        static StickDeadzoneClampedProcessor() => Initialize();
#endif

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize() =>
            UnityInputSystem.InputSystem.RegisterProcessor<StickDeadzoneClampedProcessor>();

        /// <summary>
        /// Clamps the given axis between a minimum and maximum value.
        /// </summary>
        /// <param name="value">Axis value.</param>
        /// <param name="_"></param>
        /// <returns></returns>
        public override Vector2 Process(Vector2 value, UnityInputSystem.InputControl _) =>
            ApplyDeadzone(value);

        public override string ToString() =>
            $"{nameof(StickDeadzoneClampedProcessor)}(min={min},max={max})";

        private Vector2 ApplyDeadzone(Vector2 input)
        {
            input.x = ApplyDeadzone(input.x);
            input.y = ApplyDeadzone(input.y);

            return input;
        }

        private float ApplyDeadzone(float value)
        {
            var absValue = Mathf.Abs(value);

            if (absValue > max) value = Mathf.Sign(value);
            else if (absValue < min) value = 0F;

            return value;
        }
    }
}