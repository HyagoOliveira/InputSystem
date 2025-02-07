# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Added
- TextMesh Pro package dependency
- InputSystem.OnDeviceInputChanged: event fired when any input from any connected device changed
- SpriteText: displays multiple Sprite Tags
- LocalizedSpriteText: updates the local SpriteText component when a localization update event (from Unity Localization System) happens
- Button sprites for Xbox 360, PlayStation 4/5, Nintendo Switch Pro Controller and Keyboard/Mouse
- Serialized Dictionary dependency
- Scriptable Objects for AnimationSpriteTag, ActionSpriteTag and CustomSpriteTag
- AxisWiggleInteraction
- Vector2WiggleInteraction

### Changed
- Update Input System package to 1.2.0
- Detect Playstation5 gamepad
- Detect Xbox360 gamepad only on WebGL
- InputActionPopup shows its Maps
- Increase Unity minimum version to >= 2020.2
- Move TMP_Sprite Assets into TMP folder
- Rename SpriteText -> SpriteTMP

### Removed
- DeviceDisplayControl
- DeviceDisplaySet
- DeviceDisplaySettings
- DeviceInputVisualizer
- FreePromptsPack Sprites
- InputActionMapPopup.FindActionMap function
- StickDeadzoneClampedProcessor

## [1.0.0-preview.6] - 2023-06-15
### Changed
- Refactor Deadzone Processor -> Stick Deadzone Clamper processor

## [1.0.0-preview.5] - 2021-07-08
### Added
-  Device Display Settings for PS5

### Fixed
- Keyboard Navigation reference path

### Changed
- Update InputDeviceType enum

## [1.0.0-preview.4] - 2021-01-14
### Added
- Truncate Stick Processor
- Deadzone Processor

## [1.0.0-preview.3] - 2021-01-13
### Changed
- Change InputVisualizer to the last device if available

## [1.0.0-preview.2] - 2021-01-04
### Added
- InputDeviceType enum
- Device Input Visualizer component
- Device Display Settings for PS3, PS4, Xbox360, Xbox360, XboxSeriesX, Keyboard&Mouse Dark/Light, WebGLGamepad and Generic Gamepads
- JSON input config for Keyboard&Mouse
- JSON input config for DualMotorGamepad
- Sprites from Xelu's FreePromptsPack
- ScriptableObject for DeviceDisplaySet
- ScriptableObject for DeviceDisplaySettings

### Changed
- Downgrade Unity Input System package to 1.0.1 

## [1.0.0-preview.1] - 2020-12-30
### Added
- Input Action Popup Property
- Input Action Map Popup Property
- Unity Input System 1.1.0-preview.2 package
- CHANGELOG
- README
- Initial commit

[Unreleased]: https://github.com/HyagoOliveira/InputSystem/compare/1.0.0-preview.6...main
[1.0.0-preview.6]: https://github.com/HyagoOliveira/InputSystem/tree/1.0.0-preview.6/
[1.0.0-preview.5]: https://github.com/HyagoOliveira/InputSystem/tree/1.0.0-preview.5/
[1.0.0-preview.4]: https://github.com/HyagoOliveira/InputSystem/tree/1.0.0-preview.4/
[1.0.0-preview.3]: https://github.com/HyagoOliveira/InputSystem/tree/1.0.0-preview.3/
[1.0.0-preview.2]: https://github.com/HyagoOliveira/InputSystem/tree/1.0.0-preview.2/
[1.0.0-preview.1]: https://github.com/HyagoOliveira/InputSystem/tree/1.0.0-preview.1/