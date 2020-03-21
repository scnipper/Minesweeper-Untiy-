using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Entity
{
	public class GameTimer : MonoBehaviour
	{
		private Text timeText;
		private Coroutine tickRoutine;

		private void Start()
		{
			timeText = GetComponent<Text>();

			MessageBroker.Default
				.Receive<GameMessage>()
				.Where(message => message.Id == MessagesID.StartGame)
				.Subscribe(message =>
				{
					tickRoutine = StartCoroutine(TickTime());
				}).AddTo(this);
			MessageBroker.Default
				.Receive<GameMessage>()
				.Where(message => message.Id == MessagesID.GameOver || message.Id == MessagesID.WinGame)
				.Subscribe(message =>
				{
					if (tickRoutine != null)
					{
						StopCoroutine(tickRoutine);
						tickRoutine = null;
					}
				}).AddTo(this);
		}

		private IEnumerator TickTime()
		{
			long time = 0;

			while (true)
			{
				timeText.text = time + "";
				yield return new WaitForSeconds(1);
				time++;
			}
		}
	}
}