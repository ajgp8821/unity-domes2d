using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameController : MonoBehaviour {

    public void Quit() {
        Application.Quit();
        Debug.Log("You've just exit");
    }
}
