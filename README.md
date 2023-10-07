# Input System Extension

* This package is an extension for Unity new Input System.
* Unity minimum version: **2020.2**
* Current version: **1.0.0-preview.6**
* License: **MIT**
* Dependencies:
	- Unity.Input System : [1.2.0](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.2/changelog/CHANGELOG.html)
	- Unity.TextMesh Pro : [1.0.0](https://docs.unity3d.com/Packages/com.unity.textmeshpro@1.0/changelog/CHANGELOG.html)
	- Serialized Dictionary : [1.1.0](https://github.com/HyagoOliveira/SerializedDictionary/tree/1.1.0)
	- Attributes : [3.0.0](https://github.com/HyagoOliveira/attributes/tree/3.0.0)

## Input Sprites Pack

Input buttons sprites from the amazing **Free Prompts Pack**, created by **Nicolae (Xelu) Berbece**.

You can use all these assets in any project you want to (be it commercial or not).

Includes buttons prompts for:

* Xbox 360/One
* PlayStation 4/5
* Nintentdo Switch Pro Controller
* Keyboard and Mouse

All in 100x100 px .png format.

You can use those sprites on UI when the input device changes:

![Input Device Change Showcase][prompts-showcase]

[Sprites Sheets](/Sprites/) and [TextMesh Pro Sprite Assets](/Sprite Assets) are available to use.

## How To Use

### Show Input Sprite using Text Mesh Pro

Place an [ActionSpriteText](/Runtime/SpriteText/ActionSpriteText.cs) component into the same GameObject containing a **TextMeshPro** and edit its text field, placing a ```{input}``` where the Sprite Tag will be placed. Next, set the **Input Asset** and **Action Popup** fields, like so:

![Action Sprite Text][action-sprite-text-inspector]

 At runtime, when any button or gamepad axis changes, all occurrences of ```{input}``` will be replaced by a Sprite tag corresponding to the provided Action.

![Action Sprite Text Runtime][action-sprite-text-runtime]

### Processors

* **StickDeadzoneClampedProcessor**: clamps a Stick input axis between a minimum and maximum value.
* **TruncateStickProcessor**: truncates an input axis using an absolute value.

### Properties

* **InputActionMapPopup**: displays the action maps from a given **InputActionAsset** as a Popup field.
![Action Map Popup Showcase][input-action-map-popup]
    
* **InputActionPopup**: displays the actions from a given **InputActionMap** as a Popup field.
![Action Popup Showcase][input-action-popup]

## Installation

### Using the Package Registry Server

Follow the instructions inside [here](https://cutt.ly/ukvj1c8) and the package **ActionCode-Input System** 
will be available for you to install using the **Package Manager** windows.

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set. 

- Use the **Package Manager** "Add package from git URL..." feature and paste this URL: `https://github.com/HyagoOliveira/InputSystem.git`

- You can also manually modify you `Packages/manifest.json` file and add this line inside `dependencies` attribute: 

```json
"com.actioncode.input-system":"https://github.com/HyagoOliveira/InputSystem.git"
```

---

**Hyago Oliveira**

[GitHub](https://github.com/HyagoOliveira) -
[BitBucket](https://bitbucket.org/HyagoGow/) -
[LinkedIn](https://www.linkedin.com/in/hyago-oliveira/) -
<hyagogow@gmail.com>

[prompts-showcase]: /Documentation~/prompts-showcase.gif "Free Prompts Pack Showcase"
[action-sprite-text-inspector]: /Documentation~/ActionSpriteText.png "Action Sprite Text"
[action-sprite-text-runtime]: /Documentation~/ActionSpriteText.gif "Action Sprite Text Runtime"
[input-action-popup]: /Documentation~/showcase-input-action-popup.jpg "Action Popup"
[input-action-map-popup]: /Documentation~/showcase-input-action-map-popup.jpg "Action Map Popup"