# Input System Extension

* This package is an extension for Unity new Input System.
* Unity minimum version: **2019.3**
* Current version: **1.0.0-preview.6**
* License: **MIT**
* Dependencies:
	- Unity.Input System : [1.2.0](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.2/changelog/CHANGELOG.html)
	- Unity.TextMesh Pro : [1.0.0](https://docs.unity3d.com/Packages/com.unity.textmeshpro@1.0/changelog/CHANGELOG.html)
	- Serialized Dictionary : [1.1.0](https://github.com/HyagoOliveira/SerializedDictionary/tree/1.1.0)

## How To Use

### Input Sprites Pack
![Free Prompts Pack Preview][prompts-pack-preview]

Input buttons sprites from the amazing **Free Prompts Pack** created by **Nicolae (Xelu) Berbece**.

You can use all these assets in any project you want to (be it commercial or not).

Includes buttons prompts for:

* Xbox 360
* Xbox One
* PlayStation 4
* PlayStation 5
* Nintentdo Switch Pro Controller
* Keyboard and Mouse

All in 100x100 px .png format.

You can use those sprites on UI when the input device changes:

![Input Device Change Showcase][prompts-showcase]

### How to

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

[prompts-pack-preview]: /Documentation~/prompts-pack-preview.gif "Free Prompts Pack Preview"
[prompts-showcase]: /Documentation~/prompts-showcase.gif "Free Prompts Pack Showcase"
[input-action-popup]: /Documentation~/showcase-input-action-popup.jpg "Action Popup"
[input-action-map-popup]: /Documentation~/showcase-input-action-map-popup.jpg "Action Map Popup"