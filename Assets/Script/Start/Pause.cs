using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public GameObject buttonObject;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        buttonObject.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void ContinueGame()
    {
        buttonObject.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void Disconnect()
    {
        pausePanel.SetActive(false);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Start");
    }
}
