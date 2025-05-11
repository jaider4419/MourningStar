using System.Collections;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public GameObject[] Dialogues;
    public AudioClip[] DialogueAudioClips;
    public float[] DisplayTimes = new float[] { 4f, 2f, 5f, 3f, 3f };
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        StartCoroutine(Dialogue());
    }

    IEnumerator Dialogue()
    {
        yield return new WaitForSeconds(2);
        Dialogues[0].SetActive(true);
        PlayAudioClip(0);

        yield return new WaitForSeconds(GetDisplayTime(0));
        Dialogues[0].SetActive(false);

        yield return new WaitForSeconds(2);
        Dialogues[1].SetActive(true);
        PlayAudioClip(1);

        yield return new WaitForSeconds(GetDisplayTime(1));
        Dialogues[1].SetActive(false);

        yield return new WaitForSeconds(2);
        Dialogues[2].SetActive(true);
        PlayAudioClip(2);

        yield return new WaitForSeconds(GetDisplayTime(2));
        Dialogues[2].SetActive(false);

        Dialogues[3].SetActive(true);
        PlayAudioClip(3);

        yield return new WaitForSeconds(GetDisplayTime(3));
        Dialogues[3].SetActive(false);

        yield return new WaitForSeconds(3);
        Dialogues[4].SetActive(true);
        PlayAudioClip(4);

        yield return new WaitForSeconds(GetDisplayTime(4));
        Dialogues[4].SetActive(false);
        StopCoroutine(Dialogue());
    }

    void PlayAudioClip(int index)
    {
        if (DialogueAudioClips != null && index < DialogueAudioClips.Length && DialogueAudioClips[index] != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(DialogueAudioClips[index]);
        }
    }

    float GetDisplayTime(int index)
    {
        if (DisplayTimes != null && index < DisplayTimes.Length)
        {
            return DisplayTimes[index];
        }
        return 2f;
    }
}