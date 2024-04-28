using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDisplayEventArgs
{
    public NewDisplayEventArgs(string text) { Text = text; }
    public string Text { get; }
}
