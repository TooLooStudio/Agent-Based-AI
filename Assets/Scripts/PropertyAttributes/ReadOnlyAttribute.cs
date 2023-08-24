using System;
using UnityEngine;

namespace TooLoo
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}