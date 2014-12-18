// From ASP.Net Web Stack [http://aspnetwebstack.codeplex.com/]
// Used under the Apache License

// ReSharper Disable All

using System.Collections;

namespace SimpleRazor.Attributes
{
    internal class HashCodeCombiner
    {
        private long combinedHash64 = 0x1505L;
        public int CombinedHash
        {
            get { return this.combinedHash64.GetHashCode(); }
        }
        public HashCodeCombiner Add(IEnumerable e)
        {
            if (e == null)
            {
                this.Add(0);
            }
            else
            {
                var count = 0;
                foreach (object o in e)
                {
                    this.Add(o);
                    count++;
                }
                this.Add(count);
            }
            return this;
        }
        public HashCodeCombiner Add(int i)
        {
            this.combinedHash64 = ((this.combinedHash64 << 5) + this.combinedHash64) ^ i;
            return this;
        }
        public HashCodeCombiner Add(object o)
        {
            var hashCode = (o != null) ? o.GetHashCode() : 0;
            this.Add(hashCode);
            return this;
        }
        public static HashCodeCombiner Start()
        {
            return new HashCodeCombiner();
        }
    }
}
