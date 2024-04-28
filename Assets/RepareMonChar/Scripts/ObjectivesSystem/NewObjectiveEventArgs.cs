using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectiveEventArgs
{
    public NewObjectiveEventArgs(string text) { Text = text; }
    public string Text { get; }
}
