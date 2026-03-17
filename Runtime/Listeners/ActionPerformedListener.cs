using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ActionCode.AwaitableSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Invokes <see cref="OnActionPerformed"/> event when the serialized action is performed.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ActionPerformedListener : MonoBehaviour
    {
        [Min(0f), Tooltip("Time (in seconds) to wait before listen for the input.")]
        public float waitingTime = 1f;

        [Space]
        [SerializeField] private InputActionAsset inputAsset;
        [SerializeField] private InputActionPopup actionPopup = new(nameof(inputAsset));

        [Space]
        [Tooltip("Event fired when the serialized action is performed.")]
        public UnityEvent OnActionPerformed;

        private InputAction action;

        private void Awake() => FindAction();

        private void OnEnable()
        {
            WaitAndEnableAction();
            action.performed += HandleActionPerformed;
        }

        private void OnDisable()
        {
            action.performed -= HandleActionPerformed;
            DisableAction();
        }

        private void HandleActionPerformed(InputAction.CallbackContext _) => OnActionPerformed?.Invoke();
        private void FindAction() => action = inputAsset.FindAction(actionPopup.GetPath(), throwIfNotFound: true);

        private async void WaitAndEnableAction()
        {
            await AwaitableUtility.WaitForSecondsRealtimeAsync(waitingTime);

            if (!action.actionMap.enabled) action.actionMap.Enable();
            if (!action.enabled) action.Enable();
        }

        private void DisableAction() => action.Disable();
    }
}