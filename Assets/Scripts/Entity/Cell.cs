using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;
using Util;

namespace Entity
{
	public class Cell : MonoBehaviour
	{
		public GameObject openState;
		public GameObject closeState;
		public TextMeshPro textNum;
		public GameObject bomb;
		public GameObject flag;

		private bool isBomb;
		private bool isFlagged;
		private int countBombAround;
		private bool isOpen;
		private bool isDrag;
		private Vector2 posInField;
		private bool isLock;
		private bool isStartTap;
		private bool oneLock;

		private void Start()
		{
			textNum.text = countBombAround + "";
			bomb.SetActive(isBomb);

			MessageBroker.Default.Receive<GameMessage>()
				.Where((message => message.Id == MessagesID.IsDragging))
				.Subscribe(message => { isDrag = (bool) message.Data; })
				.AddTo(this);
			MessageBroker.Default
				.Receive<GameMessage>()
				.Where(message => message.Id == MessagesID.GameOver || message.Id == MessagesID.WinGame)
				.Subscribe(message => { isLock = true; })
				.AddTo(this);
		}


		private void OnMouseDown()
		{
			if (!isOpen)
			{
				isStartTap = true;
				MainThreadDispatcher.StartUpdateMicroCoroutine(LongTap());
			}
			
		}

		private IEnumerator LongTap()
		{
			float time = 0;

			while (time < 0.3f)
			{
				time += Time.deltaTime;
				yield return null;
			}

			if (isStartTap && !isDrag && !isLock)
			{
				SetFlag(true);
				oneLock = true;
			}

			yield return null;
		}

		private void SetFlag(bool isFlag)
		{
			isFlagged = isFlag;
			flag.SetActive(isFlag);
		}

		private void OnMouseUp()
		{
			isStartTap = false;

			if (oneLock)
			{
				oneLock = false;
				return;
			}
			if (isFlagged)
			{
				SetFlag(false);
				return;
			}
			if (isDrag || isLock) return;


			if (!isBomb && countBombAround == 0)
			{
				MessageBroker.Default.Publish(new GameMessage(MessagesID.EmptyCellDown, this));
			}
			else
			{
				OpenCell();
			}

			if (isBomb)
			{
				isLock = true;
				MessageBroker.Default
					.Publish(new GameMessage(MessagesID.GameOver, this));
			}
			else
			{
				MessageBroker.Default.Publish(new GameMessage(MessagesID.CheckWinGame, this));
			}
		}

		public void OpenCell()
		{
			isOpen = true;
			openState.SetActive(true);
			closeState.SetActive(false);

			if (isFlagged)
			{
				// show cross on bomb
				bomb.transform.GetChild(0).gameObject.SetActive(true);
			}
			if (countBombAround > 0 && !isBomb)
			{
				textNum.gameObject.SetActive(true);
			}
		}

		public Vector2 SizeCell =>
			openState.transform.GetChild(0).GetComponent<SpriteRenderer>().size * transform.localScale;

		public int CountBombAround
		{
			get => countBombAround;
			set => countBombAround = value;
		}

		public bool IsBomb
		{
			get => isBomb;
			set => isBomb = value;
		}

		public bool IsOpen => isOpen;

		public bool IsFlagged => isFlagged;

		public Vector2 PosInField
		{
			get => posInField;
			set => posInField = value;
		}
	}
}