public class TimerStateEventArgs
{
    public TimerStateEventArgs(TimerState timerState) { TimerState = timerState; }
    public TimerState TimerState { get; }
}