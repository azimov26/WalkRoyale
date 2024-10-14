using UnityEngine;

namespace WalkRoyale
{
    [AddComponentMenu("WalkRoyale/Effectors/RotateEffector")]
    public partial class RotateEffector : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public Transform targetTransform;

        [Header("Effector Properties")]
        [SerializeField()] public Vector3 rotationAxis;
        [SerializeField()] public float rotationSpeed;

        public virtual void Rotate(float deltaTime) {
            targetTransform.rotation *= Quaternion.Euler(rotationAxis * (rotationSpeed * deltaTime));
        }

        protected virtual void Update() {
            Rotate(Time.deltaTime);
        }
    }
}