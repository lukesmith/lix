﻿using System.Diagnostics.CodeAnalysis;

namespace Lix.Commons.Repositories
{
    public class InMemoryTransaction
    {
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
            Justification = "Reviewed. Suppression is OK here.")]
        private readonly InMemoryDataStore datastore;

        internal InMemoryTransaction(InMemoryDataStore datastore)
        {
            this.datastore = datastore;
        }

        public InMemoryDataStore CurrentTransactionDataStore { get; private set; }

        public void Begin()
        {
            this.CurrentTransactionDataStore = new InMemoryDataStore();
            this.CurrentTransactionDataStore.Merge(this.datastore);
        }

        public void Commit()
        {
            this.datastore.Merge(this.CurrentTransactionDataStore);
            this.CurrentTransactionDataStore = null;
        }

        public void Rollback()
        {
            if (this.CurrentTransactionDataStore != null)
            {
                this.CurrentTransactionDataStore.Clear();
                this.CurrentTransactionDataStore = null;
            }
        }
    }
}