using Gus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Gus.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            LoadData();
        }

        public List<Area> Areas { get; set; }

        private async Task LoadData()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://api-dbw.stat.gov.pl/api/1.1.0/area/area-area");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Areas = AreaSeeder.Seeder();
            //JsonReader reader = new JsonTextReader(new StringReader(content));
            //var deserializeList = JsonSerializer.CreateDefault().Deserialize<List<Area>>(reader);
            //Areas = deserializeList;
            OnPropertyChanged(nameof(Areas));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
