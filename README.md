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

You can show your game button inputs using sprites from the amazing [Free Prompts Pack, created by Nicolae (Xelu) Berbece](https://thoseawesomeguys.com/prompts/).

You can use all these sprites in any project you want to (be it commercial or not).

Includes buttons prompts for:

* Xbox 360/One
* PlayStation 4/5
* Nintendo Switch Pro Controller
* Keyboard and Mouse

All are in 100x100 pixels, in .png format.

You can use those sprites on UI when the input device changes:

![Input Device Change Showcase][MenuShowcase]

## How to Display Button Sprites

Input sprites are displayed using TextMesh Pro (TMP) Sprite Tags. So, to do it, it's necessary to configure TMP Sprite Assets.

### Configuring TMP Settings

Locate **TMP Settings** asset on your project (generally at ```Assets/TextMesh Pro/Resources/TMP Settings.asset```).

Go to the Default Sprite Asset section and check for the path where your Sprites Assets will be loaded.

![TMPSettings SpriteAsset][TMPSettings_SpriteAsset]

You need to place there all Sprite Assets your project is going to use. Also, you need to configure them. Use the [official TMP Sprite Asset section](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/Sprites.html) to know how.

If you wish to use the Sprites from Free Prompts Pack, just copy and paste any file from the [Sprite Assets folder](/Sprite Assets) to your project path where your Sprites Assets will be loaded. **You don't need to configure any of them**. They are already all set to use and its sprites are on the [Sprites Sheets folder](/Sprites/) (you don't need to copy any file from this folder for your project).

### Show Input Sprites using Text Mesh Pro

Place an [ActionSpriteText](/Runtime/SpriteText/ActionSpriteText.cs) component into the same GameObject containing a **TextMeshPro** and edit its text field, placing a ```{input}``` where the Sprite Tag will be placed. Next, set the **Input Asset** and **Action Popup** fields, like so:

![Action Sprite Text][ActionSpriteText_Inspector]

 At runtime, when any button or gamepad axis changes, all occurrences of ```{input}``` will be replaced by a Sprite tag corresponding to the provided Action.

![Action Sprite Text Runtime][ActionSpriteText_Runtime]

### Using Unity Localization System

If your project uses the Localization System provided by Unity, you can also attach the [LocalizedSpriteText](/Runtime/SpriteText/LocalizedSpriteText.cs) component in the same GameObject.

### Processors

* **StickDeadzoneClampedProcessor**: clamps a Stick input axis between a minimum and maximum value.
* **TruncateStickProcessor**: truncates an input axis using an absolute value.

### Properties

* **InputActionMapPopup**: displays the action maps from a given **InputActionAsset** as a Popup field.
![Action Map Popup Showcase][InputActionMapPopup]
    
* **InputActionPopup**: displays the actions from a given **InputActionMap** as a Popup field.
![Action Popup Showcase][InputActionPopup]

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

[ActionSpriteText_Inspector]: /Documentation~/ActionSpriteText_Inspector.png "Inspector for ActionSpriteText component"
[ActionSpriteText_Runtime]: /Documentation~/ActionSpriteText_Runtime.gif "Action Sprite Text at Runtime"
[InputActionMapPopup]: /Documentation~/InputActionMapPopup.jpg "Using Action Map Popup"
[InputActionPopup]: /Documentation~/InputActionPopup.jpg "Using Input Action Popup"
[MenuShowcase]: /Documentation~/MenuShowcase.gif "Using Input Sprites in Main Menu"
[TMPSettings_SpriteAsset]: /Documentation~/TMPSettings_SpriteAsset.png "TMP Settings, Sprite Asset section"