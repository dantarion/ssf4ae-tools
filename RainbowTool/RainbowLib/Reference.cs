using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RainbowLib
{
    /* This class is used for when part of a file references another thing in the file.
     * TODO: Make this class where the Reference comes from so it can be serialized properly */
    [Serializable]
    public class Reference<T>
    {
        [NonSerialized]
        private WeakReference r;
        private Reference(T obj)
        {
            r = new WeakReference(obj);
        }
        public T Target
        {
            get { return (T)r.Target; }
            set { r = new WeakReference(value); }
            
        }
        public static implicit operator T (Reference<T> reference)
        {
            if (reference == null)
                return default(T);
            if (reference.r == null)
                return default(T);
            return (T)reference.r.Target;
        }
        public static implicit operator Reference<T>(T obj)
        {
            return new Reference<T>(obj);
        }
    }
}
