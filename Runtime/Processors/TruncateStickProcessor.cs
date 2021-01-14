using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Truncates an input axis using an absolute value.
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public sealed class TruncateStickProcessor : InputProcessor<Vector2>
    {
        [Tooltip("The absolute minimum value allowed for truncate this stick.")]
        public float value = 0.8f;

#if UNITY_EDITOR
        static TruncateStickProcessor()
        {
            Initialize();
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            UnityEngine.InputSystem.InputSystem.RegisterProcessor<TruncateStickProcessor>();
        }

        /// <summary>
        /// Truncates an axis using a value.
        /// </summary>
        /// <param name="input">The axis to truncate.</param>
        /// <param name="_"></param>
        /// <returns></returns>
        public override Vector2 Process(Vector2 input, InputControl _)
            => Truncate(input, value);

        /// <summary>
        /// Truncates the given input.
        /// </summary>
        /// <param name="input">The axis to truncate.</param>
        /// <param name="value">The absolute minimum value allowed for truncate this stick.</param>
        /// <returns>The truncate input.</returns>
        public static Vector2 Truncate(Vector2 input, float value)
        {
            var absInput = new Vector2(Mathf.Abs(input.x), Mathf.Abs(input.y));

            input.x = absInput.x > value ? Mathf.Sign(input.x) : 0f;
            input.y = absInput.y > value ? Mathf.Sign(input.y) : 0f;

            return input;
        }
    }
}