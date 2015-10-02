namespace NServiceBus
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus.ObjectBuilder;
    using NServiceBus.Sagas;

    class CustomFinderAdapter<TSagaData,TMessage> : SagaFinder where TSagaData : IContainSagaData
    {
        internal override async Task<IContainSagaData> Find(IBuilder builder, SagaFinderDefinition finderDefinition, SagaPersistenceOptions options, object message)
        {
            var customFinderType = (Type)finderDefinition.Properties["custom-finder-clr-type"];

            var finder = (IFindSagas<TSagaData>.Using<TMessage>)builder.Build(customFinderType);

            return await finder.FindBy((TMessage)message, options).ConfigureAwait(false);
        }
    }
}