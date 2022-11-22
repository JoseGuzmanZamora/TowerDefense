using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public bool hideOnStart;
    public bool shown;
    private RectTransform canvasTransform;
    private Vector3 originalTransform;
    //private TimeScaleController parentManager;
    //public LevelChanger levelManager;
    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = gameObject.GetComponent<RectTransform>();
        originalTransform = canvasTransform.position;
        if (hideOnStart) HideMe();
        //parentManager = transform.parent.gameObject.GetComponent<TimeScaleController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shown)
        {
            if (Input.GetKeyDown(KeyCode.P) && gameObject.tag == "Shop") ShowMe();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P) && gameObject.tag == "Shop") HideMe();
        }
    }

    public void ShowMe()
    {
        //if (parentManager.pausedGame) return;
        CanvasGroup cg = this.gameObject.GetComponent<CanvasGroup>();
        cg.interactable = true;
        cg.alpha = 1;
        shown = true;
        canvasTransform.position = new Vector3(originalTransform.x, originalTransform.y, originalTransform.z);
    }

    public void HideMe()
    {
        CanvasGroup cg = this.gameObject.GetComponent<CanvasGroup>();
        if (transform.tag == "Shop")
        {
            // only move down
            canvasTransform.position = new Vector3(originalTransform.x, originalTransform.y - 700, originalTransform.z);
            shown = false;
        }
        else
        {
            cg.interactable = false;
            cg.alpha = 0;
            shown = false;
            canvasTransform.position = new Vector3(originalTransform.x - 5000, originalTransform.y, originalTransform.z);
        }
    }

    public void RestartScene()
    {
        //levelManager.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void RunCredits()
    {
        //levelManager.FadeToLevel(2);
    }

    public void StartPlaying()
    {
        //levelManager.FadeToLevel(1);
    }

    public void GoToMainMenu()
    {
        //levelManager.FadeToLevel(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
