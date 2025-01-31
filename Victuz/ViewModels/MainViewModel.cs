using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Victuz.Models;

namespace Victuz.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly APIclass APIclass;
        public ObservableCollection<Post> Posts { get; private set; } = new();

        public MainViewModel()
        {
            APIclass = new APIclass();
            LoadData();
        }

        private async void LoadData()
        {
            var posts = await APIclass.GetPostsAsync();
            foreach (var post in posts)
            { 
                posts.Add(post);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) 
        
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }
}
