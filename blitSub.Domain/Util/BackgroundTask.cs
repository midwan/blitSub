using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Util
{
    public class BackgroundTask : ProgressListener
    {
        public void updateProgress(string message)
        {
            throw new NotImplementedException();
        }

        public void updateProgress(int messageId)
        {
            throw new NotImplementedException();
        }

        public void updateCache(int changeCode)
        {
            throw new NotImplementedException();
        }
    }
}
