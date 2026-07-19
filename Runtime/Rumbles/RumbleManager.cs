using ActionCode.AwaitableSystem;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Global manager handling Gamepad rumble effects using the Unity Input System.
    /// </summary>
    public static class RumbleManager
    {
        /// <summary>
        /// Whether rumble effects enabled.
        /// </summary>
        public static bool IsEnabled { get; set; } = true;

        private static CancellationTokenSource cancelationSource;

        /// <summary>
        /// Checks if rumble effects are disabled.
        /// </summary>
        /// <returns>True if rumble is disabled; otherwise, false.</returns>
        public static bool IsDisabled() => !IsEnabled;

        /// <summary>
        /// Starts a rumble effect.
        /// </summary>
        /// <param name="data">The frequency and duration configuration for the rumble.</param>
        public static async void StartRumble(RumbleData data) => await StartRumbleAsync(data);

        /// <summary>
        /// Starts a rumble effect and awaits its completion.
        /// </summary>
        /// <param name="data">The frequency and duration configuration for the rumble.</param>
        /// <returns>An Awaitable task that completes when the rumble finishes.</returns>
        public static async Awaitable StartRumbleAsync(RumbleData data)
        {
            if (IsDisabled() || InputSystem.IsUsingKeyboardOrMouse()) return;

            var gamepad = Gamepad.current;
            if (gamepad == null) return;

            StopRumble();
            gamepad.SetMotorSpeeds(data.lowFrequency, data.highFrequency);

            cancelationSource = new CancellationTokenSource();
            var token = cancelationSource.Token;
            await AwaitableUtility.WaitForSecondsRealtimeAsync(data.duration, token);

            if (!token.IsCancellationRequested) StopRumble();
        }

        /// <summary>
        /// Stops the any rumble effect.
        /// </summary>
        public static void StopRumble()
        {
            cancelationSource?.Cancel();
            cancelationSource?.Dispose();
            cancelationSource = null;

            // Gamepad can be disconnected
            Gamepad.current?.SetMotorSpeeds(0f, 0f);
        }
    }
}