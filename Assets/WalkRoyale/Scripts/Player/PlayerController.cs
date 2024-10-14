using UnityEngine;

namespace WalkRoyale
{
    [AddComponentMenu("WalkRoyale/Player/PlayerController")]
    public partial class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public Game game;
        [SerializeField()] public Transform cameraTransform;
        [SerializeField()] public Transform orientation;
        [SerializeField()] public Rigidbody targetRigidbody;
        [SerializeField()] public Animator characterAnimator;
        [SerializeField()] public AudioSource audioSourceFootstep;

        [Header("Walking Properties")]
        [SerializeField()] public float movementSpeed;
        [SerializeField()] public float rotationSpeed;

        [Header("DeadZone")]
        [SerializeField()] public BoxCollider deadZoneCollider;

        protected float characterAnimatorInitialSpeed;
        protected float movementVelocity;
        protected bool isMovementPressed;

        protected virtual void Move(Transform orientation, Vector2 move, float additionalSpeed, float deltaTime)
        {
            var speed = movementSpeed + additionalSpeed;

            if (Mathf.Approximately(move.x, 0.0f) && Mathf.Approximately(move.y, 0.0f))
                speed = 0.0f;

            var direction = new Vector3(move.x, 0.0f, move.y).normalized;
            var rotationTarget = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + orientation.eulerAngles.y;
            var moveDirection = Quaternion.Euler(0.0f, rotationTarget, 0.0f) * Vector3.forward;

            targetRigidbody.rotation = Quaternion.Lerp(targetRigidbody.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * deltaTime);
            targetRigidbody.position += moveDirection.normalized * (speed * deltaTime);
        }

        public virtual void Death()
        {
            Destroy(gameObject);
            game.Defeat();
        }

        protected virtual void InputMovementProcess(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
                isMovementPressed = false;

            if (Input.GetKey(KeyCode.A))
            {
                movementVelocity = Mathf.Clamp(movementVelocity - rotationSpeed * deltaTime, -2.5f, 2.5f);
                isMovementPressed = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movementVelocity = Mathf.Clamp(movementVelocity + rotationSpeed * deltaTime, -2.5f, 2.5f);
                isMovementPressed = true;
            }

            if (!isMovementPressed)
                movementVelocity = Mathf.Lerp(movementVelocity, 0.0f, rotationSpeed * deltaTime);

            Move(orientation, new Vector2(movementVelocity, movementSpeed), Mathf.Abs(movementVelocity), deltaTime);
        }

        protected virtual void DeadZoneProcess()
        {
            if (targetRigidbody.position.y <= deadZoneCollider.bounds.center.y + deadZoneCollider.bounds.size.y)
                Death();
        }

        public virtual void SetPauseState(bool state)
        {
            if (state)
            {
                characterAnimator.speed = 0.0f;
                audioSourceFootstep.Stop();
                return;
            }
            characterAnimator.speed = characterAnimatorInitialSpeed;
            audioSourceFootstep.Play();
        }

        protected virtual void Awake()
        {
            characterAnimatorInitialSpeed = characterAnimator.speed;
            audioSourceFootstep.Play();
        }

        protected virtual void Update()
        {
            if (!game.isGameOver)
            {
                InputMovementProcess(Time.deltaTime);
                DeadZoneProcess();
            }
        }
    }
}