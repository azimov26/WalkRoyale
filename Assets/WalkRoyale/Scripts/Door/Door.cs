using UnityEngine;

using System.Collections;

namespace WalkRoyale
{
    [ExecuteInEditMode()]
    [AddComponentMenu("WalkRoyale/Door/Door")]
    public partial class Door : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public Transform doorL;
        [SerializeField()] public Transform doorR;

        [Header("Animation Properties")]
        [SerializeField()] public Vector3 openAxisL;
        [SerializeField()] public float openAngleL;
        [SerializeField()] public Vector3 openAxisR;
        [SerializeField()] public float openAngleR;
        [SerializeField()] public float openDuration;

        protected virtual IEnumerator OpenGateDoor(Transform target, Vector3 openAxis, float openAngle)
        {
            var elapsed = 0.0f;

            var sourceAngle = target.eulerAngles;
            var targetAngle = openAxis * openAngle;

            while (elapsed <= openDuration)
            {
                elapsed += Time.deltaTime;

                var t = Mathf.Clamp01(elapsed / openDuration);
                var newAngle = Vector3.LerpUnclamped(sourceAngle, targetAngle, t);
                target.eulerAngles = newAngle;

                yield return new WaitForEndOfFrame();
            }
        }

        public virtual void OpenGate()
        {
            StartCoroutine(OpenGateDoor(doorL, openAxisL, openAngleL));
            StartCoroutine(OpenGateDoor(doorR, openAxisR, openAngleR));
        }
    }
}