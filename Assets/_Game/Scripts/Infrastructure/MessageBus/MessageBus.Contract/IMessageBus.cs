using System;
using MessageBus.Contract.Messages;

namespace MessageBus.Contract
{
    public interface IMessageBus
    {
        public void Subscribe<TSignal>(Action handler) where TSignal : struct, ISignal;
        public void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : struct, IMessage;

        public void Unsubscribe<TSignal>(Action handler) where TSignal : struct, ISignal;
        public void Unsubscribe<TMessage>(Action<TMessage> handler) where TMessage : struct, IMessage;
        public void UnsubscribeAll();

        public void Publish<TSignal>() where TSignal : struct, ISignal;
        public void Publish<TMessage>(TMessage message) where TMessage : struct, IMessage;
    }
}