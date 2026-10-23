using UnityEngine;
using UnityEngine.InputSystem;
namespace SebraeLabVR
{
    // O TrackedPoseDriver acompanha o controle; este componente trata o gatilho.
    public sealed class ControllerPointer : MonoBehaviour
    {
        [SerializeField] private bool leftHand;
        [SerializeField] private float distance = 8f;
        private InputAction trigger;
        private LineRenderer beam;
        public void Configure(bool isLeft) { leftHand = isLeft; }
        private void OnEnable()
        {
            string hand = leftHand ? "LeftHand" : "RightHand";
            trigger = new InputAction("Select", InputActionType.Button,
                "<XRController>{" + hand + "}/triggerPressed");
            trigger.Enable();
            beam = GetComponent<LineRenderer>();
        }
        private void Update()
        {
            bool hit = Physics.Raycast(transform.position, transform.forward,
                out RaycastHit info, distance, ~0, QueryTriggerInteraction.Ignore);
            if (beam != null)
            {
                beam.SetPosition(0, transform.position);
                beam.SetPosition(1, hit ? info.point : transform.position + transform.forward * distance);
            }
            if (hit && trigger.WasPressedThisFrame())
                info.collider.GetComponentInParent<ColorToggle>()?.Toggle();
        }
        private void OnDisable() { trigger?.Dispose(); trigger = null; }
    }
}