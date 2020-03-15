using UniRx;
using UnityEngine;
using Util;

namespace Entity
{
	public class GameScene : MonoBehaviour
	{
		public GameObject gameOverState;
		public GameObject startState;
		
	
		// Start is called before the first frame update
		private void Start()
		{
			MessageBroker.Default
				.Receive<GameMessage<Cell>>()
				.Where(message => message.Id == MessagesID.GameOver)
				.Subscribe(message => {gameOverState.SetActive(true); })
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
				.Publish(new GameMessage<GameScene>(this,MessagesID.StartGame,diffGame));
		}


		public void InMenu()
		{
			gameOverState.SetActive(false);
			startState.SetActive(true);
		}
	
	}
}