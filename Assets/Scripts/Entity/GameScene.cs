using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Entity
{
	public class GameScene : MonoBehaviour
	{
		public GameObject gameOverState;
		public GameObject winGameState;
		public GameObject startState;
		
	
		private void Start()
		{
			MessageBroker.Default
				.Receive<GameMessage>()
				.Where(message => message.Id == MessagesID.GameOver)
				.Subscribe(message => {gameOverState.SetActive(true); })
				.AddTo(this);
			
			MessageBroker.Default
				.Receive<GameMessage>()
				.Where(message => message.Id == MessagesID.WinGame)
				.Subscribe(message =>
				{
					winGameState.SetActive(true);
					winGameState.transform.Find("Score").GetComponent<Text>().text = "Your time: " + GameTimer.time;
				})
				.AddTo(this);
		}

		public void StartGame(int diff)
		{
			startState.SetActive(false);

			DiffGame diffGame = null;
			switch (diff)
			{
				case 0:
					diffGame = new DiffGame(new Vector2(10,10),new Vector2(6,10) );
					break;
				case 1:
					diffGame = new DiffGame(new Vector2(20,20),new Vector2(30,35) );
					break;
				case 2:
					diffGame = new DiffGame(new Vector2(30,30),new Vector2(60,70) );
					break;
			}
			MessageBroker.Default
				.Publish(new GameMessage(MessagesID.StartGame,diffGame));
		}


		public void InMenu()
		{
			gameOverState.SetActive(false);
			winGameState.SetActive(false);
			startState.SetActive(true);
		}
	
	}
}