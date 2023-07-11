using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    public void Next() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
