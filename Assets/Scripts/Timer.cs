using UnityEngine;

class Timer
{
    private HUD hud;

    public bool isActive = false;
    private float time = 0f;

    public Timer(HUD hud) {
        this.hud = hud;

        hud.ChangeTimeText(time);
    }

    public void UpdateTime()
    {
        if (isActive)
        {
            time += Time.deltaTime;

            hud.ChangeTimeText(time);
        }
    }
}
