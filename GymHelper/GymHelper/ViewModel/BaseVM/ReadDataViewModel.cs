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
        protected string query = "";

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
                    var data = await GetData(page);              

                    IsBusy = false;

                    return data;
                },
                OnCanLoadMore = () =>
                {
                    return Collection.Count < GetDataCount().Result;
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

        public async Task ReadData()
        {
            var items = await GetData(pageIndex: 0);
            Collection.FillCollection(items as List<TEntity>);
        }

        private async Task SearchData(string query)
        {
            this.query = query;

            var data = await GetData(pageIndex: 0);
            Collection.FillCollection(data as List<TEntity>);
        }

        protected abstract Task<IEnumerable<TEntity>> GetData(int pageIndex, int pageSize = pageSize);
        protected abstract Task<int> GetDataCount();
    }
}
