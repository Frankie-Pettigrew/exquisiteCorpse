using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour
{
    Text theText;
    
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    GameObject player;
    MovePlayer mp;

    private bool isTyping = false;
    private bool cancelTyping = false;
    public float typeSpeed;

    public bool enableAtStart;

    AudioSource myVoice;
    public AudioClip[] mySounds;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mp = player.GetComponent<MovePlayer>();
        theText = GetComponent<Text>();
        myVoice = GetComponentInParent<AudioSource>();

        if (textLines.Length == 0)
        {
            textLines = (theText.text.Split('\n'));

        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length;
        }

        if (!enableAtStart)
        {
            theText.enabled = false;
        }
        else
        {
            EnableDialogue();
        }
    }

    void ProgressLine()
    {
        currentLine += 1;

        if (currentLine >= endAtLine)
        {
            DisableDialogue();
        }

        else
        {
            StartCoroutine(TextScroll(textLines[currentLine]));
        }
    }

    //Coroutine that types out each letter individually
    private IEnumerator TextScroll(string lineOfText) 
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            Speak();
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;

        yield return new WaitForSeconds(0.3f);

        ProgressLine();

    }

    public void EnableDialogue()
    {
        theText.enabled = true;

        StartCoroutine(TextScroll(textLines[currentLine]));
    }


    public void DisableDialogue()
    {
        theText.enabled = false;
    }

    public void Speak()
    {
        int randomSound = Random.Range(0, mySounds.Length);
        myVoice.clip = mySounds[randomSound];
        myVoice.PlayOneShot(mySounds[randomSound]);
    }
}

