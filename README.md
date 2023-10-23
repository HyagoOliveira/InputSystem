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

Input sprites are displayed using TextMesh Pro (TMP) Sprite Tags. So, it's necessary to configure TMP Sprite Assets.

## Configuring TMP Settings

Locate **TMP Settings** asset on your project (generally at ```Assets/TextMesh Pro/Resources/TMP Settings.asset```).

Go to the Default Sprite Asset section and check for the path where your Sprites Assets will be loaded.

![TMPSettings Sprite Asset](/Documentation~/TMPSettings_SpriteAsset.png)

You need to place there all the Sprite Assets your project is going to use. Don't forget to configure them. Use the [official TMP Sprite Asset section](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2/manual/Sprites.html) to know how. Also, all Sprite Assets present in this folder should be named following the [InputDeviceType](/Runtime/InputDeviceType.cs) enum. You don't need to create a Sprite Assets for all elements on this enum since the code will search and use the nearest available on Resources folder.

If you wish to use the Sprites from **Free Prompts Pack**, just copy and paste any file from the [Sprite Assets folder](/SpriteAssets/) to your project Default Sprite Assets path. **You don't need to configure any of Free Prompts Pack TMP Sprite Assets**. They are already all set to use and its sprites are on the [Sprites Sheets folder](/Sprites/) (you don't need to copy any file from this folder for your project).

## Showing Input Sprites using TMP

Place a [SpriteText](/Runtime/SpriteTexts/SpriteText.cs) component into the same GameObject containing a **TextMeshPro** and edit its text field, placing one o more key worlds (like ```{input}```) where Sprite Tags will be placed. Next, set the **Input Sprite Tags** dictionary, using the key worlds as its keys.
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

You can find the initial and final indexes at the Sprite Assets:

![Keyboard And Mouse Indexes](/Documentation~/Indexes_KeyboardAndMouse.png)
![Xbox Indexes](/Documentation~/Indexes_XBOX.png)

Note that **Sprite Animation is done only using consecutive indexes** found at Sprite Asset. This is a limitation from TextMesh Pro package.

Reference it at your SpriteText component:

![Hadouken Action SpriteText](/Documentation~/HadoukenAction_SpriteText.png)

At runtime, TPM will play the Sprite Animation using the indexes and speed you've set:

![Hadouken Action Runtime](/Documentation~/HadoukenAction_Runtime.gif)

As you see, this approach is useful to show an input sequence like a combo.

### Show Inputs using Custom Sprites

Sometimes we just want to use a custom sprite to show an input. Do do this, create an **CustomSpriteTag** asset using the create menu, ActionCode > Input System > Custom Sprite Tag and set its fields:

![Vertical Action](/Documentation~/VerticalAction.png)

Again, find the sprite names using the Sprite Assets:

![Name KeyboardAndMouse](/Documentation~/Name_KeyboardAndMouse.png)
![Name XBOX](/Documentation~/Name_XBOX.png)

Use this approach tho show sprites you know will never change or when the input system cannot detect the key.

## Using Unity Localization System

If your project uses the Localization System provided by Unity, you can also attach the [LocalizedSpriteText](/Runtime/SpriteTexts/LocalizedSpriteText.cs) component in the same GameObject where a SpriteText component is:

![Inspector for LocalizedSpriteText](/Documentation~/LocalizedSpriteText.png)

At runtime, the Sprite Tag will use the current language:

![LocalizedSpriteText Runtime](/Documentation~/LocalizedSpriteText_Runtime.gif)

If your project uses another Localization System, use this same [LocalizedSpriteText](/Runtime/SpriteTexts/LocalizedSpriteText.cs) component as a base to create your own implementation between your Localization provider and this package [SpriteText](/Runtime/SpriteTexts/SpriteText.cs).

## Truncate Stick Processor

This processor truncates an input axis using an absolute value.

## InputActionPopup Property

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

**Tip**: there is a similar component to display only the Action Maps from a InputActionMap: [InputActionMapPopup](/Runtime/Properties/InputActionMapPopup.cs)

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