using UnityEngine;
using UnityEngine.Playables;

public class PlayTimelineOnSignal : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public SignalEmitterSource signalSource;

    private void Start()
    {
        signalSource.EmitSignalToNext += OnSignalEmitted;
    }

    public void OnSignalEmitted(SignalInfo info)
    {
        Debug.Log("Playing director");
        playableDirector.Play();
    }
}
