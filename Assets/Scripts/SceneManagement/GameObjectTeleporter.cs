using System.Collections;
using UnityEngine;

public class GameObjectTeleporter : MonoBehaviour
{
    private static GameObjectTeleporter _instance;
    public static GameObjectTeleporter Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindObjectOfType<GameObjectTeleporter>();

            if (_instance != null)
                return _instance;

            GameObject gameObjectTeleporter = new GameObject("GameObjectTeleporter");
            _instance = gameObjectTeleporter.AddComponent<GameObjectTeleporter>();

            return _instance;
        }
    }

    private bool _transitioning;
    public static bool Transitioning
    {
        get { return Instance._transitioning; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void Teleport(GameObject transitioningGameObject, Transform destination)
    {
        Instance.StartCoroutine(Instance.Transition(transitioningGameObject, destination.position, false));
    }

    public static void Teleport(GameObject transitioningGameObject, Vector3 destinationPosition)
    {
        Instance.StartCoroutine(Instance.Transition(transitioningGameObject, destinationPosition, false));
    }

    private IEnumerator Transition(GameObject transitioningGameObject, Vector3 destinationPosition, bool fade)
    {
        _transitioning = true;

        if (fade)
            yield return StartCoroutine(ScreenFader.FadeSceneOut());

        transitioningGameObject.transform.position = destinationPosition;

        if (fade)
            yield return StartCoroutine(ScreenFader.FadeSceneIn());

        _transitioning = false;
    }
}
