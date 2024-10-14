using UnityEngine;

namespace WalkRoyale
{
	[AddComponentMenu("WalkRoyale/Player/PlayerCharacterGrab")]
	public partial class PlayerCharacterGrab : MonoBehaviour
	{
		[Header("References")]
		[SerializeField()] public GameProgress gameProgress;
        [SerializeField()] public FloatingMessageProvider floatingMessageProvider;
		[SerializeField()] public PlayerController playerController;

		protected virtual void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Bottle>(out Bottle bottle))
			{
				floatingMessageProvider.RemoveFloatingMessageAmount(bottle.dollarsFromPack);
				gameProgress.SubtractDollars(bottle.dollarsFromPack);
				bottle.Grab();
				return;
			}

			if (other.TryGetComponent<Bills>(out Bills bills))
			{
				floatingMessageProvider.AddFloatingMessageAmount(bills.dollarsInPack);
				gameProgress.AddDollars(bills.dollarsInPack);
				bills.Grab();
				return;
			}

			if (other.TryGetComponent<ControlPoint>(out ControlPoint controlPoint))
			{
				controlPoint.OpenGate();
				return;
			}

			if (other.TryGetComponent<Door>(out Door door))
			{
				door.OpenGate();
				return;
			}

			if (other.TryGetComponent<WinZone>(out WinZone winZone))
			{
				winZone.PerformWin();
				return;
			}
		}
	}
}