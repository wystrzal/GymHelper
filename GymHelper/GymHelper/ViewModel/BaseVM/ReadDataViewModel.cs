using GymHelper.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace GymHelper.ViewModel.BaseVM
{
    public abstract class ReadDataViewModel<TEntity> : BaseViewModel
        where TEntity : class
    {
        private bool isBusy;
        private const int pageSize = 10;

        public InfiniteScrollCollection<TEntity> Collection { get; }

        public ICommand PerformSearchCommand
            => new Command<string>(async (query) => await SearchData(query.ToLower()));

        public ReadDataViewModel()
        {
            Collection = new InfiniteScrollCollection<TEntity>()
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    var page = Collection.Count / pageSize;
                    var items = await GetData(page);

                    IsBusy = false;

                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Collection.Count < 11;
                }
            };
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public abstract Task<IEnumerable<TEntity>> GetData(int pageIndex, int pageSize = pageSize);
        public abstract Task SearchData(string query);

        public async Task ReadData()
        {
            var items = await GetData(pageIndex: 0);
            Collection.FillCollection(items as List<TEntity>);
        }
    }
}
