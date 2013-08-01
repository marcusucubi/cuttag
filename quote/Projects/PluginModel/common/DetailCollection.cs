namespace Model.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class DetailCollection<T> : System.ComponentModel.BindingList<T> where T : Model.Common.Detail 
    {
        private Common.Header header;
        
        private bool sorted = false;
        
        private ListSortDirection sortDirection = ListSortDirection.Ascending;

        private PropertyDescriptor sortProperty = null;
        
        public DetailCollection(Common.Header header)
        {
            this.header = header;
        }

        protected override bool SupportsSearchingCore 
        {
            get { return true; }
        }

        protected override bool SupportsSortingCore 
        {
            get { return true; }
        }

        protected override ListSortDirection SortDirectionCore 
        {
            get { return this.sortDirection; }
        }

        protected override PropertyDescriptor SortPropertyCore 
        {
            get { return this.sortProperty; }
        }

        protected override bool IsSortedCore 
        {
            get { return this.sorted; }
        }

        public override void CancelNew(int itemIndex)
        {
            base.CancelNew(itemIndex);
        }

        protected override object AddNewCore()
        {
            Product p = new Product();
            Model.Common.Detail o = new Model.Template.Detail((Model.Template.Header)this.header, p);
            this.Add(o as T);
            return o;
        }

        protected override void OnAddingNew(System.ComponentModel.AddingNewEventArgs e)
        {
            base.OnAddingNew(e);
        }

        protected override int FindCore(System.ComponentModel.PropertyDescriptor prop, object key)
        {
            return base.FindCore(prop, key);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            this.sortDirection = direction;
            this.sortProperty = prop;
            DetailSortComparer comparer = new DetailSortComparer(prop, direction);
            this.ApplySort2Detail(comparer);
        }

        private void ApplySort2Detail(DetailSortComparer comparer)
        {
            List<T> listRef = new List<T>(this.Items);
            if (listRef != null)
            {
                listRef.Sort(comparer);
                this.sorted = true;
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }
    }
}
