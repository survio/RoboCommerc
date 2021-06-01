using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RoboCommerc
{
    public sealed class TypeWrapper
    {
        private Type type;
    
        private readonly Func<MethodInfo, bool> methodSelector;

        public TypeWrapper(Type type, Func<MethodInfo,bool> methodSelector)
        {
            this.type = type;
            this.methodSelector = methodSelector;
        }
        public string Name => type.Name;
        public IEnumerable<MethodInfo> Methods => type.GetMethods().Where(methodSelector);
    }
}