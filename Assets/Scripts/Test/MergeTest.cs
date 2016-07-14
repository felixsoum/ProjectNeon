using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MergeTest : MonoBehaviour
{
	void Start()
    {
        SceneManager.LoadSceneAsync("MergeSource", LoadSceneMode.Additive);
        Invoke("Unload", 5f);
    }

    void Unload()
    {
        SceneManager.UnloadScene("MergeSource");
    }
}
