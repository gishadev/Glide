using UnityEngine;

namespace Gisha.Glide.AirplaneGeneric
{
    [RequireComponent(typeof(Rigidbody))]
    public class AirplaneMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private MouseFlightController controller = null;

        [Header("Physics")]
        [SerializeField] private float defaultThrust = 100f;
        [SerializeField] private float boostedThrust = 200f;
        [SerializeField] private float boostAcceleration = 2f;
        [Space]
        [SerializeField] [Tooltip("Pitch, Yaw, Roll")] public Vector3 turnTorque = new Vector3(90f, 25f, 45f);
        [SerializeField] [Tooltip("Multiplier for all forces")] public float forceMult = 1000f;

        [Header("Autopilot")]
        [SerializeField] [Tooltip("Sensitivity for autopilot flight")] public float sensitivity = 5f;
        [SerializeField] [Tooltip("Angle at which airplane banks fully into target")] public float aggressiveTurnAngle = 10f;

        [Header("Input")]
        [SerializeField] private float thrust = 0f;
        [SerializeField] [Range(-1f, 1f)] private float pitch = 0f;
        [SerializeField] [Range(-1f, 1f)] private float yaw = 0f;
        [SerializeField] [Range(-1f, 1f)] private float roll = 0f;

        public float Thrust { get => thrust; private set => thrust = Mathf.Max(value, defaultThrust); }
        public float Pitch { get => pitch; private set => pitch = Mathf.Clamp(value, -1f, 1f); }
        public float Yaw { get => yaw; private set => yaw = Mathf.Clamp(value, -1f, 1f); }
        public float Roll { get => roll; private set => roll = Mathf.Clamp(value, -1f, 1f); }

        bool _rollOverride = false;
        bool _pitchOverride = false;

        Rigidbody _rb;
        Airplane _airplane;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _airplane = GetComponent<Airplane>();

            if (controller == null)
                Debug.LogError($"{name}: Plane - Missing reference to MouseFlightController!");

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            // When the player commands their own stick input, it should override what the
            // autopilot is trying to do.
            _rollOverride = false;
            _pitchOverride = false;

            float keyboardRoll = Input.GetAxis("Horizontal");
            if (Mathf.Abs(keyboardRoll) > .25f)
            {
                _rollOverride = true;
            }

            float keyboardPitch = Input.GetAxis("Vertical");
            if (Mathf.Abs(keyboardPitch) > .25f)
            {
                _pitchOverride = true;
                _rollOverride = true;
            }

            // Calculate the autopilot stick inputs.
            float autoYaw = 0f;
            float autoPitch = 0f;
            float autoRoll = 0f;
            if (controller != null)
                RunAutopilot(controller.MouseAimPos, out autoYaw, out autoPitch, out autoRoll);

            // Use either keyboard or autopilot input.
            Yaw = autoYaw;
            Pitch = (_pitchOverride) ? keyboardPitch : autoPitch;
            Roll = (_rollOverride) ? keyboardRoll : autoRoll;

            var aimThrust = _airplane.IsBoostedSpeed ? boostedThrust : defaultThrust;
            Thrust = Mathf.Lerp(Thrust, aimThrust, Time.deltaTime * boostAcceleration);
        }

        private void RunAutopilot(Vector3 flyTarget, out float yaw, out float pitch, out float roll)
        {
            var localFlyTarget = transform.InverseTransformPoint(flyTarget).normalized * sensitivity;
            var angleOffTarget = Vector3.Angle(transform.forward, flyTarget - transform.position);

            // ====================
            // PITCH AND YAW
            // ====================

            yaw = Mathf.Clamp(localFlyTarget.x, -1f, 1f);
            pitch = -Mathf.Clamp(localFlyTarget.y, -1f, 1f);

            // ====================
            // ROLL
            // ====================

            var agressiveRoll = Mathf.Clamp(localFlyTarget.x, -1f, 1f);
            var wingsLevelRoll = transform.right.y;

            var wingsLevelInfluence = Mathf.InverseLerp(0f, aggressiveTurnAngle, angleOffTarget);
            roll = Mathf.Lerp(wingsLevelRoll, agressiveRoll, wingsLevelInfluence);
        }

        private void FixedUpdate()
        {
            _rb.AddRelativeForce(Vector3.forward * Thrust * forceMult, ForceMode.Force);
            _rb.AddRelativeTorque(new Vector3(turnTorque.x * Pitch,
                                                turnTorque.y * Yaw,
                                                -turnTorque.z * Roll) * forceMult,
                                    ForceMode.Force);
        }
    }
}
