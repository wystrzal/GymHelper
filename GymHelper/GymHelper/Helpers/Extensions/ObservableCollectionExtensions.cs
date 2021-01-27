using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GymHelper.Data.Services
{
    public static class ObservableCollectionExtensions
    {
        public static void FillCollection<TEntity>(this ObservableCollection<TEntity> collection, List<TEntity> dataList)
            where TEntity : class
        {
            collection.Clear();

            if (dataList != null && dataList.Count > 0)
            {
                foreach (var data in dataList)
                {
                    collection.Add(data);
                }
            }
        }
    }
}
