using System.Collections;
using UnityEngine;

namespace Quests
{
    public class AtmActions : MonoBehaviour
    {
        /// <summary>
        /// Causes a button to light up, waiting for player to press
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IEnumerator ChooseCommand(string command)
        {
            return null;
        }
        public IEnumerator PresentCard(string accountNumber)
        {
            return null;
        }
        public IEnumerator TakeCard()
        {
            return null;
        }
        public IEnumerator PresentCash(int amount)
        {
            return null;
        }
        public IEnumerator TakeCash(int amount)
        {
            return null;
        }
        /// <summary>
        /// Atm key in [amount] and press ok virtually
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public IEnumerator FillInWithdrawScreen(int amount)
        {
            return null;
        }
        /// <summary>
        /// Atm key in [amount] and press ok virtually
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public IEnumerator FillInTransferScreen(int amount, string accountNumber)
        {
            return null;
        }
        public IEnumerator ToggleCashInBox(bool value)
        {
            return null;
        }
        public IEnumerator ToggleCashOutBox(bool value)
        {
            return null;
        }
        public IEnumerator ToggleButton(int buttonType, bool value)
        {
            return null;
        }
        public IEnumerator AddScore(int amount)
        {
            return null;
        }
        public IEnumerator DeductScore(int amount)
        {
            return null;
        }

        public IEnumerator ShowScreen(string screenName)
        {
            return null;
        }
        public IEnumerator PopScreen()
        {
            // store displayed screens in stacks. when the player makes a mistake, pop to the last correct screen
            return null;
        }

        public IEnumerator WaitForMachineTime()
        {
            return null;
        }

        public IEnumerator WaitForHumanTime()
        {
            return null;
        }
    }
}