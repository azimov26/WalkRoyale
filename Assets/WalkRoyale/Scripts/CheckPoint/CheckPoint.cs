using UnityEngine;

namespace WalkRoyale
{
    [ExecuteInEditMode()]
    [AddComponentMenu("WalkRoyale/CheckPoint/CheckPoint")]
    public partial class CheckPoint : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField()] public float nextAngleX;
    }
}