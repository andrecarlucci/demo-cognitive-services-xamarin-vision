using Plugin.Media;
using Plugin.Media.Abstractions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinVision
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ImageSource _currentImageSource;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand TakeAPictureCommand => new Command(async () => await SeeLikeABoss());

        public ImageSource CurrentImageSource { 
            get => _currentImageSource; 
            set { 
                _currentImageSource = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentImageSource)));
            }
        }

        
        public async Task SeeLikeABoss()
        {
            await TakeAPicture();

        }

        public async Task TakeAPicture()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = false
                });

            if (file == null)
            {
                return;
            }

            CurrentImageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
    }
}
