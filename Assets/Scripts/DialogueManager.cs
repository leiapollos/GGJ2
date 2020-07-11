using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text titleText;
    public Text DialogueText;

    public Animator animator;

    [Range(0,100)]
    public int probabilityOfGlitchedCharacter;

    protected Queue<string> sentences;

    protected string glitchedChars = "?!}{()[]&%€$§@";


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue " + dialogue.title);

        animator.SetBool("IsOpen", true);

        titleText.text = dialogue.title;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        yield return new WaitForSeconds(0.5f);
        foreach(char letter in sentence.ToCharArray())
        {
            int glitched = Random.Range(0, 100);
            if(glitched > probabilityOfGlitchedCharacter
               && DialogueText.text.Length > 0
               && glitchedChars.IndexOf(DialogueText.text[DialogueText.text.Length-1]) == -1
               && letter != ' '
            ){

                int rand = (int)Random.Range(0, glitchedChars.Length-1);
                DialogueText.text += glitchedChars[rand];

            }else{
                DialogueText.text += letter;
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(4f);
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
