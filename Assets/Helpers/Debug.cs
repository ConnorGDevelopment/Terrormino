using UnityEngine;

namespace Helpers
{
    public static class Debug
    {
        // This is function uses a generic type parameter, named T
        // These will look scary at first, but it is literally just a way to pass a class as a parameter
        // "where T : className" adds a constraint that whatever T is, it must inherit a certain class
        public static T TryFindComponent<T>(GameObject sourceObject) where T : Behaviour
        {
            T foundComponent = sourceObject.GetComponent<T>();
            if (foundComponent == null)
            {
                UnityEngine.Debug.Log($"Could not find {typeof(T).Name} on {sourceObject.name}");
            }
            // If the component wasn't actually found, then this will return null because pretty much everything in Unity is a nullable type
            return foundComponent;
        }

        public static GameObject TryFindGameObjectByName(string name)
        {
            GameObject foundGameObject = GameObject.Find(name);
            if (foundGameObject == null)
            {
                UnityEngine.Debug.Log($"Could not find GameObject {name}");
            }
            return foundGameObject;
        }

        public static T TryFindComponentOnGameObjectByName<T>(string name) where T : Behaviour
        {
            GameObject foundGameObject = TryFindGameObjectByName(name);
            if (foundGameObject != null)
            {
                return TryFindComponent<T>(foundGameObject);
            }
            else
            {
                return null;
            }
        }

        public static GameObject TryFindGameObjectByNameOnlyIfNull(GameObject gameObject, string name)
        {
            // If the GameObject is null, try to find it
            // If the GameObject is already assigned, just hand it back
            if (gameObject == null)
            {
                return TryFindGameObjectByName(name);
            }
            else
            {
                return gameObject;
            }
        }

        public static void CheckIfSetInInspector(GameObject gameObject, object toCheck, string name)
        {
            if (toCheck == null)
            {
                UnityEngine.Debug.Log($"{name} in {gameObject} not set in Inspector");
            }
        }
    }
}
// This class is setup so we can basically copy and paste it into every project we work on
