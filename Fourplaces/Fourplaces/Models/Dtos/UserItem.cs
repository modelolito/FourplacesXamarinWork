using System;
using System.Threading.Tasks;
using Fourplaces.Models;
using Fourplaces.Models.Exceptions;
using Newtonsoft.Json;
using Storm.Mvvm;
using Xamarin.Forms;

namespace TD.Api.Dtos
{
    public class UserItem : NotifierBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("image_id")]
        public int? ImageId { get; set; }

        public ImageSource SourceImage;
        public ImageSource SOURCEIMAGE
        {

            get
            {

                Task t = GetImageResource();
                return SourceImage;

            }

            set
            {
                SetProperty(ref SourceImage, value);
            }

        }

        public async Task GetImageResource()
        {
            try
            {
                if (SourceImage == null)
                {
                    SOURCEIMAGE = await SingletonRestService.RS.GetRequestImageProfil(ImageId);
                }
            }
            catch (NoConnectE e) //not connected
            {
                if (SourceImage == null)
                {
                    String url = e.urlSave;

                    SOURCEIMAGE = SingletonRestService.RS.CacheImage(url);

                }
            }

        }
    }


}