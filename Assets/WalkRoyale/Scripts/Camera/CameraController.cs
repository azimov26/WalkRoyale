using UnityEngine;

namespace WalkRoyale
{
    [ExecuteInEditMode()]
    [AddComponentMenu("WalkRoyale/Camera/CameraController")]
    public partial class CameraController : MonoBehaviour
    {
        [Header("Camera Follow")]
        [SerializeField()] public Transform cameraFollowTarget;
        [SerializeField()] public Vector3 cameraFollowOffset;
        [SerializeField()] public float cameraFollowAngleY;

        [Header("CheckPoints")]
        [SerializeField()] public CheckPoint[] checkPoints;

        protected CheckPoint lastCheckedPoint;
        protected float checkPointMaximumAngle;
        protected float checkPointCurrentAngle;

        protected virtual CheckPoint GetCheckPoint(Transform intersectionTarget)
        {
            var pa = intersectionTarget.position;

            for (int i = 0, n = checkPoints.Length; i != n; i++)
            {
                var pb = checkPoints[i].transform.position;
                var localScale = checkPoints[i].transform.localScale;

                if (Vector3.Distance(pa, pb) <= ((localScale.x + localScale.y + localScale.z) / 3.0f) / 2.0f)
                    return checkPoints[i];
            }

            return null;
        }

        protected virtual void LateUpdate()
        {
            if (cameraFollowTarget != null)
            {
                var checkPoint = GetCheckPoint(cameraFollowTarget);

                transform.position = cameraFollowTarget.position + (Quaternion.Euler(0.0f, checkPointCurrentAngle, 0.0f) * cameraFollowOffset);
                transform.rotation = Quaternion.Euler(cameraFollowAngleY, checkPointCurrentAngle, 0.0f);

                if (lastCheckedPoint != checkPoint)
                {
                    if (checkPoint != null)
                        checkPointMaximumAngle = checkPoint.nextAngleX;
                    lastCheckedPoint = checkPoint;
                }

                checkPointCurrentAngle = Mathf.Lerp(checkPointCurrentAngle, checkPointMaximumAngle, Time.deltaTime);
            }
        }
    }
}