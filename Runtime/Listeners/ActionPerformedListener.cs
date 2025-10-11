using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

        private void Awake()
        {
            FindAction();
            Invoke(nameof(EnableAction), waitingTime);
        }

        private void OnEnable() => action.performed += HandleActionPerformed;
        private void OnDisable() => action.performed -= HandleActionPerformed;

        private void HandleActionPerformed(InputAction.CallbackContext _) => OnActionPerformed?.Invoke();
        private void FindAction() => action = inputAsset.FindAction(actionPopup.GetPath(), throwIfNotFound: true);

        private void EnableAction()
        {
            action.actionMap.Enable();
            action.Enable();
        }
    }
}