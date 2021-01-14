using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Clamps an input axis between a minimum and maximum value.
    /// <para>
    /// This Deazone Processor works better than 
    /// <see cref="UnityEngine.InputSystem.Processors.StickDeadzoneProcessor"/>
    /// </para>
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    [Preserve]
    public sealed class DeadzoneProcessor : InputProcessor<Vector2>
    {
        [Tooltip("Minimum absolute value allowed for this stick.")]
        public float min = 0.25f;
        [Tooltip("Maximum absolute value allowed for this stick.")]
        public float max = 0.80f;

#if UNITY_EDITOR
        static DeadzoneProcessor()
        {
            Initialize();
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            UnityEngine.InputSystem.InputSystem.RegisterProcessor<DeadzoneProcessor>();
        }

        /// <summary>
        /// Clamps the given axis between a minimum and maximum value.
        /// </summary>
        /// <param name="value">Axis value.</param>
        /// <param name="_"></param>
        /// <returns></returns>
        public override Vector2 Process(Vector2 value, InputControl _)
            => ApplyDeadzone(value, min, max);

        /// <summary>
        /// Applies the deadzone in the given input.
        /// </summary>
        /// <param name="input">Axis input.</param>
        /// <param name="min">Minimum absolute value allowed for this stick.</param>
        /// <param name="max">Maximum absolute value allowed for this stick.</param>
        /// <returns>The input with deadzone applied.</returns>
        public static Vector2 ApplyDeadzone(Vector2 input, float min, float max)
        {
            var absValue = new Vector2(Mathf.Abs(input.x), Mathf.Abs(input.y));

            if (absValue.x > max) input.x = Mathf.Sign(input.x);
            else if (absValue.x < min) input.x = 0F;

            if (absValue.y > max) input.y = Mathf.Sign(input.y);
            else if (absValue.y < min) input.y = 0F;

            return input;
        }
    }
}