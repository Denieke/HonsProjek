using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace App1
{
    [Activity(Label = "@string/app_name")]
    public class AdditionalDetails : Activity
    {
        ImageView imageView;
        Intent intentCamera;
        Intent intent;
        Bitmap bitmap;
        byte[] bitmapData;
        string[] BodyShape;
        string BodyLength;
        string[] MouthShape;
        string[] TailShape;
        string[] BackSpine;
        string Family;
        string Anomalies;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdditionalDetails);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_additional);
            SetActionBar(toolbar);

            EditText eAnomalies = FindViewById<EditText>(Resource.Id.text_anomalies);

            BodyShape = Intent.GetStringArrayExtra("BodyShape") ;
            BodyLength = Intent.GetStringExtra("BodyLength") ?? string.Empty ;
            MouthShape = Intent.GetStringArrayExtra("MouthShape");
            BackSpine = Intent.GetStringArrayExtra("BackSpine") ;
            TailShape = Intent.GetStringArrayExtra("TailShape") ;
            Family = Intent.GetStringExtra("Family") ?? string.Empty;

            var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar_additional);
            editToolbar.InflateMenu(Resource.Menu.edit_menus);
            editToolbar.MenuItemClick += async (sender, e) =>
            {
                if (bitmap != null)
                {
                    MemoryStream stream = new MemoryStream();
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bitmapData = stream.ToArray();

                }

                if (e.Item.TitleCondensedFormatted.ToString() == "Continue")
                {

                    Anomalies = eAnomalies.Text;

                    Fish f1 = new Fish
                    {
                        BodyShape = BodyShape,
                        BodyLength = BodyLength,
                        MouthShape = MouthShape,
                        TailShape = TailShape,
                        BackSpine = BackSpine,
                        Anomalies = Anomalies,
                        Image = bitmapData,
                    };
                    try
                    {
                        // Classify c1 = new Classify();
                        //int[] answer = c1.ClassifyFish(BodyShape,BodyLength,TailShape,MouthShape,BackSpine);
                        // Console.Write("##########       ANSWER        ############");
                        // Console.Write(answer[0].ToString() + " " + answer[1].ToString() + " " + answer[2].ToString() + " " + answer[3].ToString() + " " + answer[4].ToString() + " " + answer[5].ToString() + " " + answer[6].ToString());
                        CreateIntentPossibleMatchings();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                    }

                    //var json = JsonConvert.SerializeObject(f1);

                    //SaveFishDataAsync(json);
                    ////Create log with all fish
                    //SaveFishDataLog(json);

                    //Console.WriteLine("Print: " + await ReadFishDataAsync());

                    //string data = ReadFishDataAsync();
                    //Toast.MakeText(this, data, ToastLength.Short);

                    //Display the values
                    //var fish = JsonConvert.DeserializeObject<Fish>(json);
                    //Console.WriteLine(fish.BodyShape);

                    //CreateIntentPossibleMatchings();


                }
                if (e.Item.TitleCondensedFormatted.ToString() == "Camera")
                {
                    imageView = FindViewById<ImageView>(Resource.Id.camera_preview);
                    intentCamera = new Intent(MediaStore.ActionImageCapture);
                    StartActivityForResult(intentCamera, 0);
                }

            };
        }


        void CreateIntentPossibleMatchings()
        {
            intent = new Intent(this, typeof(PossibleMatchings));
            intent.PutExtra("BodyShape", BodyShape);
            intent.PutExtra("BodyLength", BodyLength);
            intent.PutExtra("MouthShape", MouthShape);
            intent.PutExtra("TailShape", TailShape);
            intent.PutExtra("BackSpine", BackSpine);
            intent.PutExtra("Anomalies", Anomalies);
            intent.PutExtra("Family", Family);
            intent.PutExtra("Image", bitmap);

            this.StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode != Result.Canceled)
            {
                bitmap = (Bitmap)data.Extras.Get("data");
                imageView.SetImageBitmap(bitmap);

            }
        }

        /*public void SaveFishDataLog(string data)
        {
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FishLog.txt");
            if (File.Exists(backingFile))
            {
                using (var writer = File.AppendText(backingFile))
                {
                    writer.WriteLineAsync(data);
                }
            }
        }
        public void SaveFishDataAsync(string data)
        {
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "CurrentFish.txt");

            using (var writer = File.CreateText(backingFile))
            {
                writer.WriteLine(data);
            }
        }


        public string ReadFishDataAsync()
        {
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "CurrentFish.txt");

            if (backingFile == null || !File.Exists(backingFile))
            {
                return "";
            }

            var data = "";
            using (var reader = new StreamReader(backingFile, true))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    data += " " + line;
                }
            }

            return data;
        }*/


    }
}