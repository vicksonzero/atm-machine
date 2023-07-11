namespace Quests
{
    public static class AtmEvents
    {
        public class CommandChosen : IGameEvent
        {
            public string command;
        }
        public class CardPresented : IGameEvent
        {
        }

        public class CardTaken : IGameEvent
        {
        }

        public class CashPresented : IGameEvent
        {
            public int amount;
        }

        public class CashTaken : IGameEvent
        {
            public int amount;
        }

        public class WithdrawScreenFilled : IGameEvent
        {
            public int amount;
        }

        public class TransferScreenFilled : IGameEvent
        {
            public int amount;
            public int accountId;
        }
    }
}