﻿using System;
using System.IO;
using System.Threading.Tasks;
using Fourplaces.Models;
using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    public class MonCompteEditViewModel : ViewModelBase
    {
        private UserItem _user;
        private Command _editer;
        private Command _picture;

        private ImageSource _image;
        private byte[] imageB;

        private bool typeP=false;

        //private String imageId;

            

        public MonCompteEditViewModel()
        {

            //Task t = UserData();
            //Console.WriteLine("Dev_MCEVM:" + USER.Email);
            _editer = new Command(() => Editer());
            _picture = new Command(() => ChoosePicture());


        }

        

        [NavigationParameter]
        public UserItem USER
        {

            get
            {
                return _user;
            }
            set
            {
                Console.WriteLine("mcevm:" + value.FirstName);
                SetProperty(ref _user, value);
                IMAGE = USER.SOURCEIMAGE;
                //IMAGEID=USER.ImageId.ToString();



            }
        }

        public ImageSource IMAGE
        {

            get
            {

                return _image;
            }
            set
            {
                SetProperty(ref _image, value);

            }


        }

        public bool TYPEP
        {
            get
            {
                return (typeP);
            }

            set
            {
                SetProperty(ref typeP, value);
            }
        }

        //public String IMAGEID
        //{

        //    get
        //    {
        //        Console.WriteLine("IMAGEIDC:" + imageId);
        //        return imageId;

        //    }
        //    set
        //    {
        //        SetProperty(ref imageId, value);


        //    }
        //}

        public Command EDITER
        {
            get
            {
                return _editer;
            }
        }

        public Command PICTURE
        {
            get
            {
                return _picture;
            }
        }

        async private void Editer()
        {

            //USER.ImageId = int.Parse(IMAGEID, System.Globalization.CultureInfo.InvariantCulture);
            Console.WriteLine("EditTest:" + USER.FirstName + "|" + USER.LastName+"|");
            await SingletonRestService.RS.EditCountAsync(USER.FirstName,USER.LastName, USER.ImageId,imageB);
        }

        public async void ChoosePicture()
        {

            imageB = await SingletonRestService.RS.SendPicture(TYPEP); 
            IMAGE = ImageSource.FromStream(() => new MemoryStream(imageB));
            Console.WriteLine("Dev_ChoosePicture");
        }

    }
}

