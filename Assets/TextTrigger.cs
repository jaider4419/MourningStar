using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour
{
    public GameObject chosenText;
    [SerializeField] private AudioSource audioSource;
    private Coroutine displayCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && chosenText != null)
        {
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }

            chosenText.SetActive(true);
            PlayTriggerSound();

            displayCoroutine = StartCoroutine(HideAfterDelay(6f));
        }
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (chosenText != null) 
        {
            chosenText.SetActive(false);
        }
        displayCoroutine = null;
    }

    private void PlayTriggerSound()
    {
        if (audioSource != null && audioSource.clip != null && audioSource.isActiveAndEnabled)
        {
            audioSource.Play();
        }
    }

    private void OnDisable()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }

        if (chosenText != null)
        {
            chosenText.SetActive(false);
        }
    }
}