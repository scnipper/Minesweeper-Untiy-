using UnityEngine;

namespace Util
{
	public static class Extensions
	{
		public static void RemoveAllChildren(this Transform transform)
		{
			int childCount = transform.childCount;

			for (int i = 0; i < childCount; i++)
			{
				Object.Destroy(transform.GetChild(i).gameObject);
			}
		}
	}
}