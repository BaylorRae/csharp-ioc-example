using System;

namespace IoC
{
    public class Container
    {
        public object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}