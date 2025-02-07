namespace ActionCode.InputSystem
{
    /// <summary>
    /// Interface used on objects able to be a Sprite Text.
    /// </summary>
    public interface ISpriteText
    {
        /// <summary>
        /// The source text content.
        /// </summary>
        string SourceText { get; }

        /// <summary>
        /// Updates the text using Sprites Tags according with the given device.
        /// </summary>
        /// <param name="device">The input device to update the Sprite Tags.</param>
        void UpdateTextWithSpriteTags(InputDeviceType device);
    }
}
