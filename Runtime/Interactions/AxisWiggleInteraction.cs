using UnityEngine;
using UnityEngine.InputSystem;
using UnityInputSystem = UnityEngine.InputSystem.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Wiggle Input Interaction for axis movement.
    /// <para>Put this interaction into an Action <b>using an Axis as Control Type</b>.</para>
    /// </summary>
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif    
    public sealed class AxisWiggleInteraction : AbstractWiggleInteraction<float>
    {
#if UNITY_EDITOR
        static AxisWiggleInteraction() => Initialize();
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() => UnityInputSystem.RegisterInteraction<AxisWiggleInteraction>();

        protected override float GetSignedInput(InputInteractionContext context)
        {
            var input = context.ReadValue<float>();
            return Mathf.Sign(input);
        }
    }
}