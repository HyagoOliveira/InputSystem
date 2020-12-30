# Input System Extension

* This package is an extension for Unity new Input System.
* Unity minimum version: **2019.3**
* Current version: **1.0.0-preview.1**
* License: **MIT**
* Dependencies:
	- [com.unity.inputsystem : 1.1.0-preview.2](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.1/changelog/CHANGELOG.html#110-preview2---2020-10-23)

## How To Use

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

[input-action-popup]: /Documentation~/showcase-input-action-popup.jpg "Action Popup"
[input-action-map-popup]: /Documentation~/showcase-input-action-map-popup.jpg "Action Map Popup"