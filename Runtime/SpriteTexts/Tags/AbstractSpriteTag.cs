using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Abstract data container for a Sprite Tag.
    /// </summary>
    public abstract class AbstractSpriteTag : ScriptableObject, IDisposable
    {
        [SerializeField, Tooltip("The Input Asset where your action is.")]
        protected InputActionAsset inputAsset;
        [SerializeField, Tooltip("The Input Action.")]
        protected InputActionPopup actionPopup = new(nameof(inputAsset));

        /// <summary>
        /// Event fired when the action has been started.
        /// </summary>
        public event Action OnActionStarted;

        /// <summary>
        /// Event fired when the action has been fully performed.
        /// </summary>
        public event Action OnActionPerformed;

        /// <summary>
        /// Event fired when the action has been started but then canceled before being fully performed.
        /// </summary>
        public event Action OnActionCanceled;

        /// <summary>
        /// The Input Action associated with this Sprite Tag.
        /// </summary>
        public InputAction Action { get; private set; }

        protected const string MENU_NAME = "ActionCode/Input System/";

        /// <summary>
        /// Initializes the Action by enabling it and subscribing to its events.
        /// </summary>
        public void Initialize()
        {
            Action = GetAction();

            Action.started += HandleActionStarted;
            Action.performed += HandleActionPerformed;
            Action.canceled += HandleActionCanceled;

            Action.Enable();
        }

        /// <summary>
        /// Disposes the Action by disabling it and unsubscribing from all events.
        /// </summary>
        public void Dispose()
        {
            if (Action == null) return;

            Action.Disable();

            Action.started -= HandleActionStarted;
            Action.performed -= HandleActionPerformed;
            Action.canceled -= HandleActionCanceled;

            Action = null;
        }

        /// <summary>
        /// Returns the Sprite Tag for the given device.
        /// </summary>
        /// <param name="device">The device used to find the appropriate Sprite Tag.</param>
        /// <returns>A string containing a TMP Sprite Tag</returns>
        public abstract string GetTag(InputDeviceType device);

        /// <summary>
        /// Gets the action from the Input Action.
        /// </summary>
        /// <returns></returns>
        public InputAction GetAction() => inputAsset.FindAction(actionPopup.GetPath(), throwIfNotFound: true);

        private void HandleActionStarted(InputAction.CallbackContext _) => OnActionStarted?.Invoke();
        private void HandleActionPerformed(InputAction.CallbackContext _) => OnActionPerformed?.Invoke();
        private void HandleActionCanceled(InputAction.CallbackContext _) => OnActionCanceled?.Invoke();

        protected static string GetTagUsingName(string assetName, string spriteName) =>
            $"<sprite=\"{assetName}\" name=\"{spriteName}\">";
    }
}