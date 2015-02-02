using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial01.Code
{
    public interface IMediaMessage
    {
        void PostMessage(string message);
    }

    public class MediaMessage
    {
        IMediaMessage mediaMessage = null;
        public MediaMessage(IMediaMessage  ms)
        {
            this.mediaMessage = ms;
        }
        public void PostMessage(string message)
        {
            mediaMessage.PostMessage(message);
        }
    }
}