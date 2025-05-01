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
        [SerializeField] private InputActionAsset inputAsset;
        [SerializeField] private InputActionPopup actionPopup = new(nameof(inputAsset));

        [Space]
        /// <summary>
        /// Event fired when the serialized action is performed.
        /// </summary>
        public UnityEvent OnActionPerformed;

        private void OnEnable() => GetAction().performed += HandleActionPerformed;
        private void OnDisable() => GetAction().performed -= HandleActionPerformed;

        private void HandleActionPerformed(InputAction.CallbackContext _) => OnActionPerformed?.Invoke();

        private InputAction GetAction() => inputAsset.FindAction(actionPopup.GetPath());
    }
}