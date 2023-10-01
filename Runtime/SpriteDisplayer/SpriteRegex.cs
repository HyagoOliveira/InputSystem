using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    public static class SpriteRegex
    {
        public static IEnumerable<Match> GetMatches(string text)
        {
            // Pattern for a string contain {actionName}
            const string pattern = @"\{([^\}]+)\}";
            var matches = Regex.Matches(text, pattern);
            return matches.Cast<Match>();
        }

        public static InputBinding GetInputBinding(InputDeviceType device)
        {
            var isKeyboard = device == InputDeviceType.KeyboardAndMouse;
            var group = isKeyboard ? "Keyboard&Mouse" : "Gamepad";
            return InputBinding.MaskByGroup(group);
        }

        public static string GetSpriteTag(string assetName, string name) =>
            $"<sprite=\"{assetName}\" name=\"{name}\">";
    }
}