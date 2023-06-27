using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroButton : MonoBehaviour
{

    ChangeScene changeScene;

    [SerializeField] Button startBtn;
    [SerializeField] Button optionBtn;
    [SerializeField] Button quitBtn;

    [SerializeField] GameObject levelpanel;

    // Start is called before the first frame update
    void Start()
    {
        changeScene = GetComponent<ChangeScene>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PeassStartBtn()
    {
    
    }

}
