using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _02Iterator
{
    public abstract class AbstractIterator
    {
        public abstract void First();

        public abstract void Next();

        public abstract bool IsDone();

        public abstract object CurrentItem();
    }
}