using System;
using System.Collections.Generic;
using System.Text;

namespace easychat.Classes
{
    public class GroupDetailPagePropagationInfo
    {
        public string PageName;
        public string KeyPage;
        public GroupDetailPagePropagationInfo(string PageName, string KeyPage)
        {
            this.KeyPage = KeyPage;
            this.PageName = PageName;
        }
    }
}
