using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts;
using UnityEngine;
using UnityEngine.Events;
using static Quests.PlayerEvents;

namespace Quests
{
    public class QuestProcess : MonoBehaviour
    {
        private AtmActions _atmActions;

        public IEnumerator Withdraw(Account account)
        {
            yield return StartCoroutine(_atmActions.PresentCard(account.accountNumber));
            yield return new WaitForPlayerEvent<CardTaken>(new CardTaken());

            yield return StartCoroutine(_atmActions.WaitForHumanTime());
            yield return StartCoroutine(_atmActions.ShowScreen("Main Menu"));

            yield return StartCoroutine(_atmActions.WaitForHumanTime());
            yield return StartCoroutine(_atmActions.ChooseCommand("Withdraw"));
            yield return new WaitForPlayerEvent<ScreenShown>(new ScreenShown("Withdraw"));

            yield return StartCoroutine(_atmActions.WaitForHumanTime());
            yield return StartCoroutine(_atmActions.FillInWithdrawScreen(4));

            var amountIsReasonable = account.balance >= 10;
            if (amountIsReasonable)
            {
                var wait = new WaitForPlayerEvent<CashOutput>(new CashOutput());
                yield return wait;

                var cashOutput = wait.TriggeredEvent;
                if (cashOutput.amount == 10)
                {
                    yield return StartCoroutine(_atmActions.AddScore(10));
                    
                    yield return StartCoroutine(_atmActions.WaitForMachineTime());
                    yield return StartCoroutine(_atmActions.ShowScreen("Withdraw Success"));
                    
                    yield return StartCoroutine(_atmActions.WaitForMachineTime());
                    yield return StartCoroutine(_atmActions.ShowScreen("Main Menu"));
                }
                else
                {
                    yield return StartCoroutine(_atmActions.DeductScore(10));
                }
            }
            else
            {
                yield return new WaitForPlayerEvent<ScreenShown>(new ScreenShown("Withdraw Fail"));
                // await random time by driver
                yield return StartCoroutine(_atmActions.ShowScreen("Main Menu"));
            }
        }

        public void Deposit()
        {
            // (Atm Present Card)
            // Player Should Take card
            // (Atm chooses [Deposit])
            // Player Should Press Deposit button to acknowledge
            // Player Outputs Card
            // (Atm puts in Cash of [amount], perhaps with rubbish or fake cash)
            // Player Takes Cash of [amount]
            // Player Should Show Confirm screen with [amount] of Cash
            // (Atm accepts or not)
            // if Atm accepts:
            //     Player Should Show Deposit Success screen
            // if Atm doesn't accept:
            //     Player should return Cash and rubbish
            //     Player Should Show Deposit Fail Screen
            //     Screen will auto go back to Menu 
        }

        public class WaitForPlayerEvent<TGameEvent> where TGameEvent : IGameEvent
        {
            private readonly TGameEvent _gameEvent;
            public TGameEvent TriggeredEvent;

            public WaitForPlayerEvent(TGameEvent gameEvent)
            {
                _gameEvent = gameEvent;
            }
        }

        public class WaitForPlayerEvents
        {
            private readonly IGameEvent[] _gameEvents;
            public IGameEvent TriggeredEvent;

            public WaitForPlayerEvents(IGameEvent[] gameEvents)
            {
                _gameEvents = gameEvents;
            }
        }

        public class WaitForAnyPlayerEvent
        {
            public IGameEvent TriggeredEvent;

            public WaitForAnyPlayerEvent()
            {
            }
        }
    }
}