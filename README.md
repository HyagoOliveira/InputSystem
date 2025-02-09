# Input System Extension

* This package is an extension for Unity new Input System.
* Unity minimum version: **2020.2**
* Current version: **1.0.0-preview.6**
* License: **MIT**
* Dependencies:
	- Unity.Input System : [1.2.0](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.2/changelog/CHANGELOG.html)
	- Unity.TextMesh Pro : [1.0.0](https://docs.unity3d.com/Packages/com.unity.textmeshpro@1.0/changelog/CHANGELOG.html)
	- Serialized Dictionary : [1.1.0](https://github.com/HyagoOliveira/SerializedDictionary/tree/1.1.0)

## Input Sprites Pack

Show your game button inputs using sprites from the amazing [Free Prompts Pack, created by Nicolae (Xelu) Berbece](https://thoseawesomeguys.com/prompts/). The sprites are free to use in any project (be it commercial or not).

Includes buttons sprites in 100x100 pixels (png format) for:

* Xbox 360/One
* PlayStation 4/5
* Nintendo Switch Pro Controller
* Keyboard and Mouse

For example, you can use those sprites on UI when the input device changes in a Main Menu:

![Menu Showcase](/Documentation~/MenuShowcase.gif)

Input sprites are displayed using TextMesh Pro (TMP) or UI Toolkit (UITK) Sprite Tags. 

Before that, it's necessary to configure TMP or UITK Sprite Assets. If yor project uses both TMP and UITK, check where you need to use the buttons sprites and configure the assets accordingly. 
You can also use both TMP and UITK as well.

## Configuring UITK Text Settings

Locate **UITK Text Settings** asset on your project (generally at ```Assets/UI Toolkit/UITK Text Settings```). If you don't find this asset, create one following this [instructions](https://docs.unity3d.com/6000.0/Documentation/Manual/UIE-text-setting-asset.html).

Go to the Default Sprite Asset section and check for the path where your Sprites Assets will be loaded:

![UITKTextSettings Sprite Asset](/Documentation~/UITKTextSettings_SpriteAsset.png)

You need to place there all the Sprite Assets your project is going to use. Don't forget to configure them. Use the [official Include sprites in text section](https://docs.unity3d.com/Manual/UIE-sprite.html) to know how.

## Configuring TMP Settings

Locate the **TMP Settings** asset on your project (generally at ```Assets/TextMesh Pro/Resources/TMP Settings```).

Go to the Default Sprite Asset section and check for the path where your Sprites Assets will be loaded:

![TMPSettings Sprite Asset](/Documentation~/TMPSettings_SpriteAsset.png)

You need to place there all the Sprite Assets your project is going to use. Don't forget to configure them. Use the [official TMP Sprite Asset section](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/Sprites.html) to know how. 

## Placing Sprite Assets to Folder

All Sprite Assets present on Default Sprite Asset folder should be named following the [InputDeviceType](/Runtime/InputDeviceType.cs) enum. You don't need to create a Sprite Assets for all elements on this enum since the code will search and use the nearest available on this folder.

If you wish to use the Sprites from **Free Prompts Pack**, just copy and paste to your project Default Sprite Assets path any file from the folder [UITK Sprite Assets](/SpriteAssets/UITK) (if using UITK) or [TMP Sprite Assets](/SpriteAssets/TMP) (if using TMP).

 **You don't need to configure any of Free Prompts Pack Sprite Assets**. They are already all set to use and its sprites are on the [Sprites folder](/Sprites/) (you don't need to copy any file from this folder to your project).

 > **Note**: when using Sprite Assets inside a Ressource folder, you don't need to place there the Sprite Atlas Texture, just the Sprite Asset.

## Showing Input Sprites 

### Using UITK

Place a [SpriteUITK](/Runtime/SpriteTexts/SpriteUITK.cs) component into the same GameObject containing a **UI Document**. In the Source Asset, put one or more Labels using one or more key worlds (like ```{input}```) where the Sprite Tags will be placed. Next, set the **Input Sprite Tags** dictionary (section after next), using the key worlds as its keys.
At runtime, all key worlds will be replaced by the Sprite Tag associated to it.

### Using TMP

Place a [SpriteTMP](/Runtime/SpriteTexts/TMP/SpriteTMP.cs) component into the same GameObject containing a **TextMeshPro** and edit its text field, placing one or more key worlds (like ```{input}```) where the Sprite Tags will be placed. Next, set the **Input Sprite Tags** dictionary (next section), using the key worlds as its keys.
At runtime, all key worlds will be replaced by the Sprite Tag associated to it.

### Show Inputs from Input Action Assets

Create an **ActionSpriteTag** asset using the create menu, ActionCode > Input System > Action Sprite Tag and set its fields:

![Attack Action](/Documentation~/AttackAction.png)

Next, use this asset as a reference in **Input Sprite Tags** dictionary:

![Attack Action SpriteText](/Documentation~/AttackAction_SpriteText.png)

At runtime, when any keyboard/gamepad button is pressed or gamepad axis/mouse movement, all occurrences of ```{attack}``` will be replaced by the Sprite Tag corresponding to the fields you have set into the **ActionSpriteTag** asset:

![Attack Action Runtime](/Documentation~/AttackAction_Runtime.gif)

This approach is useful when showing an input that player can change on the game input settings.

#### How to name Sprites?

If you created a new action and want to know how to name the Sprite for it, find your Input Actions asset, select your Action and click on the **T Button** next to Path field. Copy only the string after ```<Device>/``` prefix:

![Finding Action Name](/Documentation~/FindingActionName.png)

On each Sprite Asset, go to Sprite Character Table and paste this string on the field name:

![Finding Action Name](/Documentation~/RenameAction.png)

### Show Inputs using Animations

Create an **AnimationSpriteTag** asset using the create menu, ActionCode > Input System > Animation Sprite Tag and set its fields:

![Half Moon Action](/Documentation~/HalfMoonAction.png)

You can find the initial and final indexes in the Sprite Assets:

![Keyboard And Mouse Indexes](/Documentation~/Indexes_KeyboardAndMouse.png)
![Xbox Indexes](/Documentation~/Indexes_XBOX.png)

> Note that **Sprite Animation is done only using consecutive indexes** found at Sprite Asset. This is a limitation from Sprite Asset.

Reference it at your SpriteTMP or SpriteUITK component:

![Hadouken Action SpriteText](/Documentation~/HadoukenAction_SpriteText.png)

At runtime, the Sprite Animation will be played using the indexes and speed you've set:

![Hadouken Action Runtime](/Documentation~/HadoukenAction_Runtime.gif)

As you can see, this approach is useful to show an input sequence like a combo.

### Show Inputs using Custom Sprites

Sometimes we just want to use a custom sprite to show an input. Do do this, create an **CustomSpriteTag** asset using the create menu, ActionCode > Input System > Custom Sprite Tag and set its fields:

![Vertical Action](/Documentation~/VerticalAction.png)

Again, find the sprite names using the Sprite Assets:

![Name KeyboardAndMouse](/Documentation~/Name_KeyboardAndMouse.png)
![Name XBOX](/Documentation~/Name_XBOX.png)

Use this approach tho show sprites you know will never change or when the input system cannot detect the key.

## Using Unity Localization System

If your project uses the Localization System provided by Unity, you can also attach the [LocalizedSpriteTMP](/Runtime/SpriteTexts/TMP/LocalizedSpriteTMP.cs) component in the same GameObject where a SpriteTMP component is:

![Inspector for LocalizedSpriteText](/Documentation~/LocalizedSpriteText.png)

At runtime, the Sprite Tag will use the current language:

![LocalizedSpriteText Runtime](/Documentation~/LocalizedSpriteText_Runtime.gif)

If your project uses another Localization System, use this same [LocalizedSpriteTMP](/Runtime/SpriteTexts/TMP/LocalizedSpriteTMP.cs) component as a base to create your own implementation between your Localization provider and this package [SpriteTMP](/Runtime/SpriteTexts/TMP/SpriteTMP.cs).

## Other Features

### Truncate Stick Processor

This processor truncates an input axis using an absolute value.

### InputActionPopup Property

This property displays the actions from a given **InputActionMap** as a Popup field.

Suppose you have this Input Action Asset containing your player inputs:

![Player Input](/Documentation~/PlayerInput.png)

You can use the InputActionPopup as follow:

```csharp
using UnityEngine;
using ActionCode.InputSystem;
using UnityEngine.InputSystem;

public sealed class InputActionPopupTest : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private InputActionPopup actionPopup = new(nameof(inputAsset));
}
```

The [InputActionPopup](/Runtime/Properties/InputActionPopup.cs) will render a nice popup containing all your input actions:

![Input Action Popup Tester](/Documentation~/InputActionPopupTester.gif)

> **Tip**: there is a similar component to display only the Action Maps from a InputActionMap: [InputActionMapPopup](/Runtime/Properties/InputActionMapPopup.cs)

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