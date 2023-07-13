using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void StartButton(string name)
    {
        //if(Input.GetMouseButtonDown(0))
        SceneManager.LoadScene(name);
    }

}
