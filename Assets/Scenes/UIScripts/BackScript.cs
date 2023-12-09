using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour
{

    [SerializeField]
    private Button backButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(GoToBack);
        }
    }
    public void GoToBack()
    {
        Debug.Log("Clicked back button");
        SceneManager.LoadScene("MainMenu");
    }
}