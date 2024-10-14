using UnityEngine;

namespace WalkRoyale
{
	[AddComponentMenu("WalkRoyale/Items/Bottle")]
	public partial class Bottle : MonoBehaviour
	{
		[Header("References")]
        [SerializeField()] public GameObject grabParticleSystemPrefab;
        [SerializeField()] public AudioSource audioSourceRemoveMoney;

		[Header("Properties")]
		[SerializeField()] public int dollarsFromPack;

		public virtual void Grab()
		{
			var grabParticleSystem = Instantiate(grabParticleSystemPrefab, transform.position, transform.rotation);
			Destroy(grabParticleSystem.gameObject, 2.5f);
			audioSourceRemoveMoney.Play();
			Destroy(gameObject);
		}
	}
}