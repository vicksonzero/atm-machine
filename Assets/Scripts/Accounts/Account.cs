using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Accounts
{
    public class Account
    {
        public string accountNumber;
        public string accountName;
        public bool isMale;
        public int hairStyle;

        protected Account(string accountNumber, string accountName, bool isMale, int hairStyle)
        {
            this.accountNumber = accountNumber;
            this.accountName = accountName;
            this.isMale = isMale;
            this.hairStyle = hairStyle;
        }

        public Account[] GetSeedData()
        {
            return new[]
            {
                new Account("050-0121", "Peter", true, 0),
                new Account("050-0133", "Jose", true, 1),
                new Account("050-0135", "Abigail", false, 3),
                new Account("050-0177", "Joshua", true, 5),
                new Account("050-0206", "Andrew", true, 2),
                new Account("050-0246", "Harold", true, 0),
                new Account("050-0280", "Isabella", false, 5),
                new Account("050-0286", "Donald", true, 1),
                new Account("050-0312", "Donna", false, 6),
                new Account("050-0352", "Sarah", false, 7),
                new Account("050-0356", "Alice", false, 5),
                new Account("050-0365", "Joe", true, 0),
                new Account("050-0372", "Billy", true, 2),
                new Account("050-0403", "Megan", false, 7),
                new Account("050-0422", "Andrea", false, 4),
                new Account("050-0431", "Cheryl", false, 5),
                new Account("050-0451", "Donald", true, 1),
                new Account("050-0465", "Dennis", true, 1),
                new Account("050-0475", "Elijah", true, 2),
                new Account("050-0493", "Madison", false, 4),
                new Account("050-0522", "Julia", false, 7),
                new Account("050-0560", "Michelle", false, 7),
                new Account("050-0561", "Bryan", true, 0),
                new Account("050-0607", "Jason", true, 0),
                new Account("050-0609", "Donald", true, 1),
                new Account("050-0611", "Christina", false, 6),
                new Account("050-0620", "Noah", true, 2),
                new Account("050-0645", "Keith", true, 8),
                new Account("050-0667", "Virginia", false, 7),
                new Account("050-0679", "Samuel", true, 1),
                new Account("050-0705", "Martha", false, 4),
                new Account("050-0736", "Kelly", false, 6),
                new Account("050-0777", "Sharon", false, 5),
                new Account("050-0812", "Christian", true, 1),
                new Account("050-0813", "Pamela", false, 6),
                new Account("050-0814", "Katherine", false, 7),
                new Account("050-0826", "Doris", false, 7),
                new Account("050-0849", "Grace", false, 9),
                new Account("050-0855", "Patricia", false, 9),
                new Account("050-0872", "Robert", true, 8),
                new Account("050-0879", "Willie", true, 8),
                new Account("050-0885", "Alexis", false, 9),
                new Account("050-0888", "Justin", true, 8),
                new Account("050-0922", "Philip", true, 2),
                new Account("050-0938", "Ruth", false, 7),
                new Account("050-0970", "Thomas", true, 1),
            };
        }
    }
}