using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public void FinishGame(){
        Debug.Log("Finish");
        SceneManager.LoadScene(0);
    }
}
