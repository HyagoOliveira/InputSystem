using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Invokes <see cref="OnActionPerformed"/> event when the <see cref="actionPopup"/> is performed.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ActionPerformedListener : MonoBehaviour
    {
        [Min(0f), Tooltip("Time (in seconds) to wait before listen for the input.")]
        public float waitingTime = 1f;

        [Space]
        [SerializeField] private InputActionAsset inputAsset;
        [SerializeField] private InputActionPopup actionPopup = new(nameof(inputAsset));

        [Header("Events")]
        [Tooltip("Event fired when the serialized action is started.")]
        public UnityEvent OnActionStarted;
        [Tooltip("Event fired when the serialized action is performed.")]
        public UnityEvent OnActionPerformed;
        [Tooltip("Event fired when the serialized action is canceled.")]
        public UnityEvent OnActionCanceled;

        private InputAction action;

        private void Awake() => FindAction();

        private void OnEnable()
        {
            WaitAndEnableAction();
            action.started += HandleActionStarted;
            action.canceled += HandleActionCanceled;
            action.performed += HandleActionPerformed;
        }

        private void OnDisable()
        {
            action.started -= HandleActionStarted;
            action.canceled -= HandleActionCanceled;
            action.performed -= HandleActionPerformed;
            DisableAction();
        }

        private void HandleActionStarted(InputAction.CallbackContext _) => OnActionStarted?.Invoke();
        private void HandleActionPerformed(InputAction.CallbackContext _) => OnActionPerformed?.Invoke();
        private void HandleActionCanceled(InputAction.CallbackContext _) => OnActionCanceled?.Invoke();
        private void FindAction() => action = inputAsset.FindAction(actionPopup.GetPath(), throwIfNotFound: true);

        private void WaitAndEnableAction()
        {
            StopAllCoroutines();
            StartCoroutine(WaitAndEnableActionRoutine());
        }

        private IEnumerator WaitAndEnableActionRoutine()
        {
            yield return new WaitForSecondsRealtime(waitingTime);
            EnableAction();
        }

        private void EnableAction()
        {
            if (!action.actionMap.enabled) action.actionMap.Enable();
            if (!action.enabled) action.Enable();
        }

        private void DisableAction() => action.Disable();
    }
}