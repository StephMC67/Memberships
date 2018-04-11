using System;

namespace Memberships.Areas.Admin.Models
{
    internal class DisplaynameAttribute : Attribute
    {
        private string v;

        public DisplaynameAttribute(string v)
        {
            this.v = v;
        }
    }
}