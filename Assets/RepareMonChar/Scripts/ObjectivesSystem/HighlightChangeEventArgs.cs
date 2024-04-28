

public class HighlightChangeEventArgs
{
    public HighlightChangeEventArgs(bool isOn, bool isConfigurable) { IsOn = isOn; IsConfigurable = isConfigurable; }
    public bool IsOn { get; }
    public bool IsConfigurable { get; }
}