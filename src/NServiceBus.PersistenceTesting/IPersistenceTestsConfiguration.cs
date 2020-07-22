﻿namespace NServiceBus.PersistenceTesting
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Extensibility;
    using NServiceBus.Outbox;
    using NServiceBus.Sagas;
    using Persistence;

    public interface IPersistenceTestsConfiguration
    {
        bool SupportsDtc { get; }

        bool SupportsOutbox { get; }

        bool SupportsFinders { get; } // TODO: why do we require this?

        bool SupportsSubscriptions { get; }

        bool SupportsTimeouts { get; }

        bool SupportsPessimisticConcurrency { get; }

        ISagaIdGenerator SagaIdGenerator { get; }

        ISagaPersister SagaStorage { get; }

        ISynchronizedStorage SynchronizedStorage { get; }

        TimeSpan? SessionTimeout { get; }

        ISynchronizedStorageAdapter SynchronizedStorageAdapter { get; }

        IOutboxStorage OutboxStorage { get; }

        SagaMetadataCollection SagaMetadataCollection { get; }

        Task Configure(object testClass);

        Task Cleanup();

        Func<ContextBag> GetContextBagForSagaStorage { get; }
        Func<ContextBag> GetContextBagForOutbox { get; }
    }

    // Consumers of this source package have to implement the remaining properties via partial class to configure the tests infrastructure.
    public partial class PersistenceTestsConfiguration : IPersistenceTestsConfiguration
    {
        public PersistenceTestsConfiguration(TestVariant variant, TimeSpan? sessionTimeout = null)
        {
            SessionTimeout = sessionTimeout;
            Variant = variant;
        }

        public Func<ContextBag> GetContextBagForSagaStorage { get; private set; } = () => new ContextBag();
        public Func<ContextBag> GetContextBagForOutbox { get; private set; } = () => new ContextBag();
        public TimeSpan? SessionTimeout { get; }
        public TestVariant Variant { get; }

        public SagaMetadataCollection SagaMetadataCollection
        {
            get
            {
                if (sagaMetadataCollection == null)
                {
                    var sagaTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(Saga).IsAssignableFrom(t) || typeof(IFindSagas<>).IsAssignableFrom(t) || typeof(IFinder).IsAssignableFrom(t)).ToArray();
                    sagaMetadataCollection = new SagaMetadataCollection();
                    sagaMetadataCollection.Initialize(sagaTypes);
                }

                return sagaMetadataCollection;
            }
            set { sagaMetadataCollection = value; }
        }

        SagaMetadataCollection sagaMetadataCollection;
    }
}