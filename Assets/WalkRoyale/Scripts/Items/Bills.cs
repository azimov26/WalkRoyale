using UnityEngine;

namespace WalkRoyale
{
	[AddComponentMenu("WalkRoyale/Items/Bills")]
	public partial class Bills : MonoBehaviour
	{
		[Header("References")]
        [SerializeField()] public GameObject grabParticleSystemPrefab;
        [SerializeField()] public AudioSource audioSourceAddMoney;

		[Header("Properties")]
		[SerializeField()] public int dollarsInPack;

		public virtual void Grab()
		{
			var grabParticleSystem = Instantiate(grabParticleSystemPrefab, transform.position, transform.rotation);
			Destroy(grabParticleSystem.gameObject, 2.5f);
			audioSourceAddMoney.Play();
			Destroy(gameObject);
		}
	}
}