using UnityEngine;
using UnityInputSystem = UnityEngine.InputSystem.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Wiggle Input Interaction for a vector movement.
    /// <para>Put this interaction into an Action <b>using an Vector 2 as Control Type</b>.</para>
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public sealed class Vector2WiggleInteraction : AbstractWiggleInteraction<Vector2>
    {
#if UNITY_EDITOR
        static Vector2WiggleInteraction() => Initialize();
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() => UnityInputSystem.RegisterInteraction<Vector2WiggleInteraction>();

        protected override float GetSignedInput(UnityEngine.InputSystem.InputInteractionContext context)
        {
            var input = context.ReadValue<Vector2>().normalized;
            var simpleMagnitude = input.x + input.y;
            return Mathf.Sign(simpleMagnitude);
        }
    }
}