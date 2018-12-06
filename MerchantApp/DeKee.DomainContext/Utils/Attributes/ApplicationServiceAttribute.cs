using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Utils.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    sealed class ApplicationServiceAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string alias;

        // This is a positional argument
        public ApplicationServiceAttribute(string alias)
        {
            this.alias = alias;

            // TODO: Implement code here

            throw new NotImplementedException();
        }

        public string Alias
        {
            get { return alias; }
        }

        // This is a named argument
        public int AuditCode { get; set; }
    }
}
