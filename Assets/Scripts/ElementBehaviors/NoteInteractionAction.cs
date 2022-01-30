using System;
using UnityEngine;

public class NoteInteractionAction : AbstractInteractionAction
{
    [SerializeField] private LoreNote loreNote;
    private bool wasCollected = false;

    private void Awake()
    {
        LevelData.NotesCrossed++;
    }

    public override void OnPlayerTrigger(Collider2D other)
    {
        wasCollected = true;
        ShowTextBehaviour.Instance.ShowText(loreNote.note);
        LevelData.NotesCollected++;
        AudioManager.Instance.PlaySound("Note");
    }

    private void OnDestroy()
    {
        if (!wasCollected)
        {
            LevelData.NotesCrossed--;
        }
    }
}
