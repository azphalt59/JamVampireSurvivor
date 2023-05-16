using System;
using UnityEngine;

namespace Common.Tools
{
    public class InstantiableAttribute : PropertyAttribute
    {
        public Type Type;

        public InstantiableAttribute(Type type)
        {
            Type = type;
        }
    }

    public class InstantiableListAttribute : PropertyAttribute
    {
        public Type Type;

        public InstantiableListAttribute(Type type)
        {
            Type = type;
        }
    }
}