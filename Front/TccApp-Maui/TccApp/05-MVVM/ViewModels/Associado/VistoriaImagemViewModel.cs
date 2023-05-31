using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccApp.Domain.Dtos;
using TccApp.Domain.Interfaces;
using TccApp.Models;

namespace TccApp.ViewModels
{
    [QueryProperty(nameof(ParamDto), "veiculoDto")]
    public partial class VistoriaImagemViewModel : BaseViewModel
    {
        public VeiculoDto ParamDto
        {
            set
            {
                veiculoDto = value;
            }
        }
        private VeiculoDto veiculoDto;

        [ObservableProperty]
        string imagemUrl;

        [RelayCommand]
        public async Task SelectImg()
        {
            await SelectImgAsync();
        }

        [RelayCommand]
        public async Task CaptureImg()
        {
            await CaptureImgAsync();
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await GoToBackAsync();
        }

        [RelayCommand]
        public async Task Save()
        {
            await SaveAsync();
        }

        private readonly IVistoriaImagemService service;

        public VistoriaImagemViewModel(IVistoriaImagemService service) : base()
        {
            Title = "Imagem Veículo";

            this.service = service;
        }

        protected async Task SaveAsync()
        {
            if (string.IsNullOrEmpty(ImagemUrl))
            {
                return;
            }

            var model = new VistoriaImagemModel();
            model.VeiculoId = veiculoDto.VeiculoId;
            model.ImagemUrl = imagemUrl;

            service.Create(model);

            await Shell.Current.DisplayAlert("Info", "Imagem adicionada!", "Ok");

            await GoToBackAsync();
        }

        protected async Task GoToBackAsync()
        {
            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                ["veiculoDto"] = veiculoDto
            });
        }

        private async Task SelectImgAsync()
        {
            var resultado = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Selecione uma foto"
            });

            if (resultado != null)
            {
                //var stream = await resultado.OpenReadAsync();

                //var imageSource = ImageSource.FromStream(() => stream);

                ImagemUrl = resultado.FullPath;
            }
        }

        private async Task CaptureImgAsync()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var foto = await MediaPicker.CapturePhotoAsync();

                if (foto != null)
                {
                    var stream = await foto.OpenReadAsync();

                    string localFilePath = Path.Combine(FileSystem.AppDataDirectory, "Imagens", foto.FileName);
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await stream.CopyToAsync(localFileStream);

                    ImagemUrl = localFilePath;
                }
            }
        }
    }
}
