# Input System Extension

* This package is an extension for Unity new Input System.
* Unity minimum version: **2019.3**
* Current version: **1.0.0-preview.2**
* License: **MIT**
* Dependencies:
	- Unity.Input System : [1.0.1](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/changelog/CHANGELOG.html#101---2020-11-20)

## How To Use

### Input Sprites Pack
![Free Prompts Pack Preview][prompts-pack-preview]

Input buttons sprites from the amazing **Free Prompts Pack** created by **Nicolae (Xelu) Berbece**.

You can use all these assets in any project you want to (be it commercial or not).

Includes button prompts for:

* Xbox 360 controller
* Xbox One controller
* Play Station 3 controller*
* Play Station 4 controller*
* Play Station Move*
* PS Vita*
* Vive Controller
* Oculus Controllers & Remote
* Wii Controller
* Wii U Controller
* Nintentdo Switch
* Steam Controller(Updated to commercial version)
* Ouya
* Keyboard and mouse buttons(Both in black and white including blanks)
* Directional arrows for thumb sticks and movement keys
* Touch Screen Gestures

All in 100x100 px .png format.

You can use those sprites on UI when the input device changes:

![Input Device Change Showcase][prompts-showcase]

### How to Use a Device Input Visualizer

You going to need a container asset grouping all the sprites from all the input devices buttons your game will use. This asset is a ScriptableObject called **DeviceDisplaySet** grouping an array of **DeviceDisplaySettings** ScriptableObjects, witch contains the sprites assets with the input path information.

To use them into your game, you must:

* Create a **DeviceDisplaySet** by going to Asset > Create > ActionCode > Input System > Device Display Set;
* Link the Device Display Settings assets to the **Settings** array attribute;
    * There are some  Device Display Settings assets already create at Sprites/FreePromptsPack.
* Create a UI GameObject with an **Image** component;
* Attach a **DeviceInputVisualizer** component to it;
* Set the **Device Set** attribute with the one you create on the first step;
* Set the **Input Path** or **Input Reference** attribute and Play the game.

### Processors
* **StickDeadzoneProcessor**: clamps an input axis between a minimum and maximum value.
* **TruncateStickProcessor**: truncates an input axis using an absolute value.

### Properties

* **InputActionMapPopup**: displays the action maps from a given **InputActionAsset** as a Popup field.
![Action Map Popup Showcase][input-action-map-popup]
    
* **InputActionPopup**: displays the actions from a given **InputActionMap** as a Popup field.
![Action Popup Showcase][input-action-popup]

## Installation

### Using the Package Registry Server

Open the **manifest.json** file inside your Unity project's **Packages** folder and add this code-block before `dependencies` attribute:

```json
"scopedRegistries": [ 
	{ 
		"name": "Action Code", 
		"url": "http://34.83.179.179:4873/", 
		"scopes": [ "com.actioncode" ] 
	} 
],
```

The package **ActionCode-Input System** will be available for you to install using the **Package Manager** windows.

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set. 

Use the **Package Manager** "Add package from git URL..." feature or add manually this line inside `dependencies` attribute: 

```json
"com.actioncode.input-system":"https://bitbucket.org/nostgameteam/input-system.git"
```

---

**Hyago Oliveira**

[BitBucket](https://bitbucket.org/HyagoGow/) -
[Unity Connect](https://connect.unity.com/u/hyago-oliveira) -
<hyagogow@gmail.com>

[prompts-pack-preview]: /Documentation~/prompts-pack-preview.gif "Free Prompts Pack Preview"
[prompts-showcase]: /Documentation~/prompts-showcase.gif "Free Prompts Pack Showcase"
[input-action-popup]: /Documentation~/showcase-input-action-popup.jpg "Action Popup"
[input-action-map-popup]: /Documentation~/showcase-input-action-map-popup.jpg "Action Map Popup"