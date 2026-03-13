using Unity.VectorGraphics;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 void Awake()
    {
        int numberOfScenePersists = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length; // This line counts the number of ScenePersist objects in the scene.
        if (numberOfScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
