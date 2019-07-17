using UnityEngine;

public static class MonoBehaviourExtension
{
	public static T GetOrAddComponent<T> (this Component child) where T: Component
	{
		T result = child.GetComponent<T>();
		if (result == null) {
			result = child.gameObject.AddComponent<T>();
		}
		return result;
	}

    public static bool IsNull(this UnityEngine.Object self)
        => object.ReferenceEquals(self, null);
}