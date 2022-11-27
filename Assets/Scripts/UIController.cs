using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public bool hideOnStart;
    public bool shown;
    private RectTransform canvasTransform;
    public Vector3 originalTransform;
    public Animator anim;
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

    public void ShowMe()
    {
        gameObject.SetActive(true);
        // CanvasGroup cg = this.gameObject.GetComponent<CanvasGroup>();
        // cg.interactable = true;
        // cg.alpha = 1;
        // shown = true;
        // canvasTransform.position = new Vector3(originalTransform.x, originalTransform.y, originalTransform.z);
    }

    public void HideMe()
    {
        gameObject.SetActive(false);
        // CanvasGroup cg = this.gameObject.GetComponent<CanvasGroup>();
        // cg.interactable = false;
        // cg.alpha = 0;
        // shown = false;
        // canvasTransform.position = new Vector3(originalTransform.x - 5000, originalTransform.y, originalTransform.z);
    }

    public void RestartScene()
    {
        //levelManager.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartUrbanHackDefense()
    {
        anim.SetTrigger("FadeIn");
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        yield return new WaitForSeconds(0.7f);
        anim.SetTrigger("FadeOut");
        HideMe();
    }

    IEnumerator FadeCoroutineFinish()
    {
        yield return new WaitForSeconds(0.7f);
        RestartScene();
    }

    public void RestartUrbanHackDefense()
    {
        anim.SetTrigger("FadeIn");
        StartCoroutine(FadeCoroutineFinish());
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
