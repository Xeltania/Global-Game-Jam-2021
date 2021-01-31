using UnityEngine;
using UnityEngine.SceneManagement;


public class nextScene : MonoBehaviour
{
    public void MoveScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}