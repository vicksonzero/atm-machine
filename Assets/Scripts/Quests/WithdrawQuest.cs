using System.Collections;

namespace Quests
{
    public class Process
    {
        public void Withdraw()
        {
            // (Atm Present Card)
            // Player Should Take card
            // (Atm chooses [Withdraw])
            // Player Should Press Withdraw button to acknowledge
            // (Atm key in [amount] and press ok)
            // (Player can do whatever)
            // if amount is reasonable:
            //     Player Outputs Cash of [amount]
            //     Player Outputs Card
            // if amount is not reasonable:
            //     Player Should Show Withdraw Fail Screen
            //     Screen will auto go back to Menu
        }
        public IEnumerator Withdraw2()
        {
            // yield return (Atm Present Card)
            // // yield return wait for event;
            // Player Should Take card
            // // yield return wait for event;
            // (Atm chooses [Withdraw])
            // // yield return wait for event;
            // Player Should Press Withdraw button to acknowledge
            // // yield return wait for event;
            // (Atm key in [amount] and press ok)
            // // yield return wait for event;
            // (Player can do whatever)
            // if amount is reasonable:
            //     Player Outputs Cash of [amount]
            //     Player Outputs Card
            // if amount is not reasonable:
            //     Player Should Show Withdraw Fail Screen
            //     Screen will auto go back to Menu
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
    }
}