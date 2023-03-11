using Code.Enemy;

namespace Code.UI
{
    public class AliensTimerActor
    {
        private readonly AliensTimer _aliensTimer;

        public AliensTimerActor(UIDisplay uiDisplay, EnemiesFactory enemiesFactory)
        {
            _aliensTimer = uiDisplay.aliensTimer;
            enemiesFactory.OnUpdateAliensCooldown += UpdateAliensTimer;
        }

        private void UpdateAliensTimer(float currentCooldown)
        {
            _aliensTimer.UpdateTimerText(currentCooldown);
        }
    }
}