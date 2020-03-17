using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SwitchToLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (NetworkManager.singleton == null)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
        else
            Destroy(gameObject);
    }
}
