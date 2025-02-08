using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Disable the deselection behavior on background click from a local 
    /// <see cref="InputSystemUIInputModule"/> component.
    /// 
    /// <para>
    /// Normally, when interacting with an UI Document, if you click outside a 
    /// Visual Element, the last selected element is disabled.
    /// Use this component to disable this behavior.
    /// </para>
    /// 
    /// <para>
    /// <b>Note</b>: in order to really disable deselection on background clicks,
    /// always set the Picking Mode to Ignore (instead of the default Position)
    /// inside every Visual Tree Asset topmost Visual Element.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EventSystem))]
    [RequireComponent(typeof(InputSystemUIInputModule))]
    public sealed class BackgroundClickDisabler : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private InputSystemUIInputModule module;

        private void Reset()
        {
            eventSystem = GetComponent<EventSystem>();
            module = GetComponent<InputSystemUIInputModule>();
        }

        private void Awake() => DisableDeselectOnBackgroundClick();

        private void DisableDeselectOnBackgroundClick()
        {
            module.deselectOnBackgroundClick = false;
            EventSystem.SetUITookitEventSystemOverride(
                eventSystem,
                sendEvents: true,
                createPanelGameObjectsOnStart: true
            );
        }
    }
}
