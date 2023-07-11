namespace Quests
{
    public static class PlayerEvents
    {
        public class CardOutput : IGameEvent
        {
        }

        public class CardTaken : IGameEvent
        {
        }

        public class CashOutput : IGameEvent
        {
            public int amount;
        }

        public class CashTaken : IGameEvent
        {
            public int amount;
        }

        public class ScreenShown : IGameEvent
        {
            public string screenName;

            public ScreenShown(string screenName)
            {
                this.screenName = screenName;
            }
        }

        public class ButtonPushed : IGameEvent
        {
            public int buttonId;
        }
    }
}