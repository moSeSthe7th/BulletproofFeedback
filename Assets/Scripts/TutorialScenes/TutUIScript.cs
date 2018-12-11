using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutUIScript : MonoBehaviour
{
    private AudioSource audioSource;

    public Text tutorialText;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHitSound()
    {
        audioSource.Play();
    }

    public void SetTutorialText(string text){
        tutorialText.gameObject.SetActive(true);
        tutorialText.text = text;
    }

    public void RemoveTutorialText(){
        tutorialText.text = "";
        tutorialText.gameObject.SetActive(false);
    }

    public IEnumerator SetTutorialTextIE(string text)
    {
        tutorialText.gameObject.SetActive(true);
        tutorialText.text = text;
        while (tutorialText.color.a < 1)
        {
            tutorialText.color = new Color(tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, tutorialText.color.a + 0.1f);
            yield return new WaitForSeconds(.02f);
        }
        StopCoroutine(SetTutorialTextIE(text));
    }

    public IEnumerator DefaultTutorialTextIE(){
        while (tutorialText.color.a > 0)
        {
            tutorialText.color = new Color(tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, tutorialText.color.a - 0.2f);
            yield return new WaitForSeconds(.02f);
        }
        tutorialText.text = "";
        tutorialText.gameObject.SetActive(false);
        StopCoroutine(DefaultTutorialTextIE());
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        //energySlider.gameObject.SetActive(true);
        //pointCounterText.gameObject.SetActive(true);
        //setPointText();

    }
}
