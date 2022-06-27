using System;
using MvvmElF.Messaging;

namespace MvvmElF.TestApp.Mvvm.Services
{
    public class CustomMessanger : IMessenger
    {
        public void BeginSend<TMessage>(TMessage message, object token)
        {
            throw new NotImplementedException();
        }

        public bool Register<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            throw new NotImplementedException();
        }

        public bool Send<TMessage>(TMessage message, object token)
        {
            throw new NotImplementedException();
        }

        public bool Unregister(object recipient, object token)
        {
            throw new NotImplementedException();
        }

        public bool Unregister(object recipient)
        {
            throw new NotImplementedException();
        }
    }
}
