using System;
using System.Collections.Generic;
using MessageBus.Contract;
using MessageBus.Contract.Messages;

namespace MessageBus.Impl
{
    public sealed class MessageBusImpl : IMessageBus
    {
        private enum UpdateType
        {
            Subscribe,
            Unsubscribe,
            Clear,
        }

        private readonly struct SubscriptionUpdate
        {
            public readonly UpdateType Type;
            public readonly Type MessageType;
            public readonly object Handler;

            public SubscriptionUpdate(UpdateType updateType, Type messageType, object handler)
            {
                Type = updateType;
                MessageType = messageType;
                Handler = handler;
            }
        }

        private int _publishingCount;
        private readonly Queue<SubscriptionUpdate> _updates = new();
        private readonly IReadOnlyDictionary<UpdateType, Action<SubscriptionUpdate>> _updaters;
        private readonly IDictionary<Type, ICollection<object>> _handlers =
            new Dictionary<Type, ICollection<object>>();

        public MessageBusImpl()
        {
            _updaters = new Dictionary<UpdateType, Action<SubscriptionUpdate>> {
                { UpdateType.Subscribe, update => Subscribe(update.MessageType, update.Handler) },
                { UpdateType.Unsubscribe, update => Unsubscribe(update.MessageType, update.Handler)},
                { UpdateType.Clear, update => ClearHandlers() },
            };
        }

        private void Subscribe(Type messageType, object handler)
        {
            if (!_handlers.TryGetValue(messageType, out var handlers))
                _handlers.Add(messageType, handlers = new HashSet<object>());

            handlers.Add(handler);
        }

        private void Unsubscribe(Type messageType, object handler)
        {
            if (_handlers.TryGetValue(messageType, out var handlers))
                handlers.Remove(handler);
        }

        private void ClearHandlers()
        {
            _handlers.Clear();
        }

        public void Subscribe<TSignal>(Action handler) where TSignal : struct, ISignal
        {
            if (_publishingCount == 0)
                Subscribe(typeof(TSignal), handler);
            else
                _updates.Enqueue(new SubscriptionUpdate(UpdateType.Subscribe, typeof(TSignal), handler));
        }

        public void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : struct, IMessage
        {
            if (_publishingCount == 0)
                Subscribe(typeof(TMessage), handler);
            else
                _updates.Enqueue(new SubscriptionUpdate(UpdateType.Subscribe, typeof(TMessage), handler));
        }

        public void Unsubscribe<TSignal>(Action handler) where TSignal : struct, ISignal
        {
            if (_publishingCount == 0)
                Unsubscribe(typeof(TSignal), handler);
            else
                _updates.Enqueue(new SubscriptionUpdate(UpdateType.Unsubscribe, typeof(TSignal), handler));
        }

        public void Unsubscribe<TMessage>(Action<TMessage> handler) where TMessage : struct, IMessage
        {
            if (_publishingCount == 0)
                Unsubscribe(typeof(TMessage), handler);
            else
                _updates.Enqueue(new SubscriptionUpdate(UpdateType.Unsubscribe, typeof(TMessage), handler));
        }

        public void UnsubscribeAll()
        {
            if (_publishingCount == 0)
                ClearHandlers();
            else
                _updates.Enqueue(new SubscriptionUpdate(UpdateType.Clear, null, null));
        }

        public void Publish<TSignal>() where TSignal : struct, ISignal
        {
            if (_handlers.TryGetValue(typeof(TSignal), out var handlers))
            {
                _publishingCount++;
                foreach (var handler in handlers)
                    ((Action)handler)();

                _publishingCount--;
                ActualizeSubscriptions();
            }
        }

        public void Publish<TMessage>(TMessage message) where TMessage : struct, IMessage
        {
            if (_handlers.TryGetValue(typeof(TMessage), out var handlers))
            {
                _publishingCount++;
                foreach (var handler in handlers)
                    ((Action<TMessage>)handler)(message);

                _publishingCount--;
                ActualizeSubscriptions();
            }
        }

        private void ActualizeSubscriptions()
        {
            if (_publishingCount == 0)
                while (_updates.TryDequeue(out var update))
                    _updaters[update.Type](update);
        }
    }
}