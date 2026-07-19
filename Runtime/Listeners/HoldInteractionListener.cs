using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Invokes events when a input with a Hold Interaction is executing.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class HoldInteractionListener : MonoBehaviour
    {
        [Space]
        [SerializeField] private InputActionAsset inputAsset;
        [SerializeField] private InputActionPopup actionPopup = new(nameof(inputAsset));

        [Header("Events")]
        [Tooltip("Event fired when the Hold Interaction is executing. The param is the normalized value.")]
        public UnityEvent<float> OnHolding;
        [Tooltip("Event fired when the Hold Interaction is performed.")]
        public UnityEvent OnPerformed;

        private InputAction action;

        private void Awake() => FindAction();

        private void OnEnable()
        {
            EnableAction();
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
            DisableAction();
        }

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        private void SubscribeEvents()
        {
            action.started += HandleActionStarted;
            action.canceled += HandleActionCanceled;    // Executed when button is up
            action.performed += HandleActionPerformed;  // Executed after Hold time duration
        }

        private void UnsubscribeEvents()
        {
            action.started -= HandleActionStarted;
            action.canceled -= HandleActionCanceled;
            action.performed -= HandleActionPerformed;
        }

        private void HandleActionStarted(CallbackContext ctx)
        {
            if (ctx.interaction is HoldInteraction hold)
                StartHolding(hold.duration);
            else Debug.LogWarning($"{action.name} must contains a Hold Interaction.");
        }

        private void HandleActionCanceled(CallbackContext _) => StopHolding();
        private void HandleActionPerformed(CallbackContext _) => Perform();

        private void FindAction() => action = inputAsset.FindAction(actionPopup.GetPath(), throwIfNotFound: true);

        private void EnableAction()
        {
            if (!action.actionMap.enabled) action.actionMap.Enable();
            if (!action.enabled) action.Enable();
        }

        private void DisableAction() => action.Disable();

        private void StartHolding(float duration)
        {
            StopAllCoroutines();
            StartCoroutine(HoldingRoutine(duration));
        }

        private void StopHolding()
        {
            StopAllCoroutines();
            Hold(0f);
        }

        private void Perform()
        {
            Hold(1F);
            OnPerformed?.Invoke();
        }

        private void Hold(float normalizedTime) => OnHolding?.Invoke(normalizedTime);

        private IEnumerator HoldingRoutine(float duration)
        {
            var current = 0f;
            do
            {
                var normalized = current / duration;
                Hold(normalized);

                current += Time.unscaledDeltaTime;
                yield return null;
            } while (current < duration);
        }
    }
}