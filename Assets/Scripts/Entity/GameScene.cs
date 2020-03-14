using Entity;
using UniRx;
using UnityEngine;

public class GameScene : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		MessageBroker.Default.Publish(new GameMessage<GameScene>(this,1));
	}

	// Update is called once per frame
	void Update()
	{
	}
}