namespace Model.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class DetailCollection<T> : 
        System.Collections.ObjectModel.ObservableCollection<T> 
        where T : Model.Common.Detail
    {
        private Common.Header header;
        
        public DetailCollection(Common.Header header)
        {
            this.header = header;
        }

        public Common.Header Header
        {
            get { return this.header; }
        }
    }
}
