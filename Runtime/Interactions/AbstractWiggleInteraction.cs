using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Abstract Input Interaction for Stick wiggle movement.
    /// <para>Use any of its implementations as interactions.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractWiggleInteraction<T> : IInputInteraction<T> where T : struct
    {
        [Tooltip("The total wiggles.")]
        public int wiggles = 2;
        [Tooltip("The time (in seconds) for each wiggle.")]
        public float duration = 0.2f;
        [Tooltip("The axis threshold that must be reached to the wiggle be actuated.")]
        public float threshold = 1f;

        private bool nextSide;
        private bool initialSide;
        private int currentWiggle;

        public void Reset() => currentWiggle = 0;

        public void Process(ref InputInteractionContext context)
        {
            if (context.timerHasExpired)
            {
                context.Canceled();
                return;
            }

            switch (context.phase)
            {
                case InputActionPhase.Waiting:
                    if (!HasInput(context)) return;

                    initialSide = IsPositiveSide(context);
                    nextSide = !initialSide;
                    context.Started();
                    context.SetTimeout(duration * wiggles);

                    break;

                case InputActionPhase.Started:
                    if (!HasWiggledInput(context)) return;

                    currentWiggle++;

                    var hasCompletedWiggles = currentWiggle == wiggles;
                    if (hasCompletedWiggles) context.Performed();
                    break;
            }
        }

        protected abstract float GetSignedInput(InputInteractionContext context);

        private bool HasWiggledInput(InputInteractionContext context)
        {
            if (!HasInput(context)) return false;

            var currentSide = IsPositiveSide(context);
            var isNextSide = currentSide == nextSide;

            if (isNextSide) nextSide = !currentSide;

            return isNextSide && currentSide != initialSide;
        }

        private bool HasInput(InputInteractionContext context) => context.ControlIsActuated(threshold);

        private bool IsPositiveSide(InputInteractionContext context)
        {
            var input = GetSignedInput(context);
            return Mathf.Sign(input) > 0F;
        }
    }
}