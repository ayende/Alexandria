using System;
using System.Transactions;
using System.Windows;

namespace Alexandria.Client.Infrastructure
{
    public class EnvironmentVerifier
    {
        public void Verify()
        {
            try
            {
                using(var tx  = new TransactionScope())
                {
                    Transaction.Current.EnlistDurable(Guid.NewGuid(), new FakeDurableEnlistment(), EnlistmentOptions.None);
                    Transaction.Current.EnlistDurable(Guid.NewGuid(), new FakeDurableEnlistment(), EnlistmentOptions.None);

                    tx.Complete();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "The Microsoft Distributed Transaction Coordinator (MSDTC) is not install or not started."+Environment.NewLine + 
                    "Alexandria requires MSDTC in order to work, please install MSDTC" + Environment.NewLine +
                    "Error: " + e.Message,
                    "Distributed Transactions are not installed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Environment.Exit(-2);
            }
        }

        public class FakeDurableEnlistment : IEnlistmentNotification
        {
            public void Prepare(PreparingEnlistment preparingEnlistment)
            {
                preparingEnlistment.Prepared();
            }

            public void Commit(Enlistment enlistment)
            {
                enlistment.Done();
            }

            public void Rollback(Enlistment enlistment)
            {
                enlistment.Done();
            }

            public void InDoubt(Enlistment enlistment)
            {
                enlistment.Done();
            }
        }
    }
}