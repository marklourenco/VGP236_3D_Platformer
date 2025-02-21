using UnityEngine;

public class TimerStopTrigger : MonoBehaviour
{
    public Hud hud;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hud.isTimerRunning = false;
        }
    }
}
