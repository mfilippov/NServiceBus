﻿namespace NServiceBus.Pipeline.Contexts
{
    using System;
    using System.Collections.Generic;
    using NServiceBus.Unicast;

    /// <summary>
    /// Outgoing pipeline context.
    /// </summary>
    public class OutgoingContext : BehaviorContext
    {
        /// <summary>
        /// Creates a new instance of <see cref="OutgoingContext"/>.
        /// </summary>
        /// <param name="parentContext">The parent context.</param>
        /// <param name="deliveryMessageOptions">The delivery options.</param>
        /// <param name="headers">The headers fór the message</param>
        /// <param name="messageId">The id of the message</param>
        /// <param name="intent">The intent of the message</param>
        /// <param name="messageType">The message type</param>
        /// <param name="messageInstance">The message instance</param>
        /// <param name="isControlMessage">Tells if this is a control message</param>
        public OutgoingContext(BehaviorContext parentContext, DeliveryMessageOptions deliveryMessageOptions,Dictionary<string, string> headers, string messageId, MessageIntentEnum intent,Type messageType,object messageInstance,bool isControlMessage = false)
            : base(parentContext)
        {
            DeliveryMessageOptions = deliveryMessageOptions;
            Headers = headers;
            MessageId = messageId;
            Intent = intent;
            MessageType = messageType;
            MessageInstance = messageInstance;
            IsControlMessage = isControlMessage;
        }

        /// <summary>
        /// Sending options.
        /// </summary>
        public DeliveryMessageOptions DeliveryMessageOptions { get; private set; }

        /// <summary>
        ///     Gets other applicative out-of-band information.
        /// </summary>
        public Dictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// This id of this message
        /// </summary>
        public string MessageId { get; private set; }

        /// <summary>
        /// The intent of the message
        /// </summary>
        public MessageIntentEnum Intent { get; private set; }

        /// <summary>
        /// The message type for this message
        /// </summary>
        public Type MessageType { get; set; }

        /// <summary>
        /// The actual message instance
        /// </summary>
        public object MessageInstance { get; set; }

        /// <summary>
        /// Tells if this outgoing message is a control message
        /// </summary>
        /// <returns></returns>
        public bool IsControlMessage { get; private set; }
    }
}