using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USBDisplay
{
    // USB_IOException //
    public class USB_IOException : ApplicationException
    {
        public USB_IOException(string message)
            : base(message) { }

        protected USB_IOException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public USB_IOException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
