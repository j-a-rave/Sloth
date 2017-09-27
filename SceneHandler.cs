using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//class to handle a mutable "stack" of scenes. upon hitting "back," will return to previous scene.

public class SceneHandler : MonoBehaviour {

    const float SCENE_TRANSITION_TIME = 3.3f;

    public static List<int> scenes = new List<int>();
    static IEnumerator coroutine;

	void Start () {
        if (scenes.Count == 0) //game start, no scenes in list. only scene to load w/o SceneManager assistance is 0 (the first one.), which is added after to avoid an edge case
        {
            scenes.Add(0);
        }
	}
	
	public void LoadScene(int id) //adds scene at build settings index id, and loads.
    {
        scenes.Add(id);
        SceneManager.LoadScene(scenes[scenes.Count - 1]);
    }

    public static void ExitScene() //removes current id from stack, return to previous or quit.
    {
        scenes.RemoveAt(scenes.Count - 1);
        if (scenes.Count > 0)
        {
            SceneManager.LoadScene(scenes[scenes.Count - 1]);
        }
        else
        {
            Application.Quit();
        }
    }

    public static void ResetSceneList(List<int> newScenes) //creates new scene stack, loads highest index
    {
        scenes = newScenes;
        SceneManager.LoadScene(scenes[scenes.Count - 1]);
    }

    //timer coroutines. only ~weird~ is that these must be non-static
    public void StartSceneAddTimer(int sceneID)
    {
        coroutine = AddTimer(sceneID);
        StartCoroutine(coroutine);
    }

    IEnumerator AddTimer(int sceneID)
    {
        yield return new WaitForSeconds(SCENE_TRANSITION_TIME);
        LoadScene(sceneID);
    }

    public void StartSceneResetTimer(List<int> scenes)
    {
        coroutine = ResetTimer(scenes);
        StartCoroutine(coroutine);
    }

    IEnumerator ResetTimer(List<int> scenes)
    {
        yield return new WaitForSeconds(SCENE_TRANSITION_TIME);
        ResetSceneList(scenes);
    }

    public void StartSceneExitTimer()
    {
        coroutine = ExitTimer();
        StartCoroutine(coroutine);
    }

    IEnumerator ExitTimer()
    {
        yield return new WaitForSeconds(SCENE_TRANSITION_TIME);
        ExitScene();
    }
}
