using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public List<Dialogue> dialogue;
    protected int index = 0;

    public void TriggerNextDialogue(){
        if(index == 0)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[0]);
            index++;
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[(int)Random.Range(1,dialogue.Count-1)]);
        }
    }
}
