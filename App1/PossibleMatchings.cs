using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Plugin.CurrentActivity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace App1
{
    [Activity(Label = "@string/app_name")]
    public class PossibleMatchings : Activity
    {
        string[] items;
        ListView mainList;
        string location;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.PossibleMatchings);

            //Get Values from previous activity
            string[] BodyShape = Intent.GetStringArrayExtra("BodyShape");
            string BodyLength = Intent.GetStringExtra("BodyLength") ?? string.Empty;
            string[] MouthShape = Intent.GetStringArrayExtra("MouthShape");
            string[] BackSpine = Intent.GetStringArrayExtra("BackSpine");
            string[] TailShape = Intent.GetStringArrayExtra("TailShape");
            string Family = Intent.GetStringExtra("Family") ?? string.Empty;
            string Anomalies = Intent.GetStringExtra("Anomalies") ?? string.Empty;

            Bitmap imageTaken = (Bitmap)Intent.Extras.Get("Image");


            //string Location = Intent.GetStringExtra("Location") ?? string.Empty;
            string bodyShape = "";

            if (BodyShape.Length == 1)
                bodyShape = BodyShape[0] ?? string.Empty;
            if (BodyShape.Length == 2)
                bodyShape += BodyShape[0] + "/" + BodyShape[1];
            else if (BodyShape.Length == 3)
                bodyShape += BodyShape[0] + "/" + BodyShape[1] + "/" + BodyShape[2];
            else if (BodyShape.Length == 4)
                bodyShape += BodyShape[0] + "/" + BodyShape[1] + "/" + BodyShape[2] + "/" + BodyShape[3];

            string mouthShape = "";

            if (MouthShape.Length == 1)
                mouthShape = MouthShape[0] ?? string.Empty;
            if (MouthShape.Length == 2)
                mouthShape += MouthShape[0] + "/" + MouthShape[1];
            else if (MouthShape.Length == 3)
                mouthShape += MouthShape[0] + "/" + MouthShape[1] + "/" + MouthShape[2];
            else if (MouthShape.Length == 4)
                mouthShape += MouthShape[0] + "/" + MouthShape[1] + "/" + MouthShape[2] + "/" + MouthShape[3];
            else if (MouthShape.Length == 5)
                mouthShape += MouthShape[0] + "/" + MouthShape[1] + "/" + MouthShape[2] + "/" + MouthShape[3] + "/" + MouthShape[4];

            string tailShape = "";

            if (TailShape.Length == 1)
                tailShape = TailShape[0] ?? string.Empty;
            if (TailShape.Length == 2)
                tailShape += TailShape[0] + "/" + TailShape[1];
            else if (TailShape.Length == 3)
                tailShape += TailShape[0] + "/" + TailShape[1] + "/" + TailShape[2];
            else if (TailShape.Length == 4)
                tailShape += TailShape[0] + "/" + TailShape[1] + "/" + TailShape[2] + "/" + TailShape[3];
            else if (TailShape.Length == 5)
                tailShape += TailShape[0] + "/" + TailShape[1] + "/" + TailShape[2] + "/" + TailShape[3] + "/" + TailShape[4];

            string backSpine = "";

            if (BackSpine.Length == 1)
                backSpine = BackSpine[0] ?? string.Empty;
            if (BackSpine.Length == 2)
                backSpine += BackSpine[0] + "/" + BackSpine[1];
            else if (BackSpine.Length == 3)
                backSpine += BackSpine[0] + "/" + BackSpine[1] + "/" + BackSpine[2];
            else if (BackSpine.Length == 4)
                backSpine += BackSpine[0] + "/" + BackSpine[1] + "/" + BackSpine[2] + "/" + BackSpine[3];
            else if (BackSpine.Length == 5)
                backSpine += BackSpine[0] + "/" + BackSpine[1] + "/" + BackSpine[2] + "/" + BackSpine[3] + "/" + BackSpine[4];
            else if (BackSpine.Length == 6)
                backSpine += BackSpine[0] + "/" + BackSpine[1] + "/" + BackSpine[2] + "/" + BackSpine[3] + "/" + BackSpine[4] + "/" + BackSpine[5];


            Position position = getPosition();
            string timestamp = DateTime.Today.ToShortDateString();
            var output = string.Format("\t\t\t\t\t\t\t\tTime: {0} \n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tLatitude: {1} \n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tLongitude: {2}",
                         timestamp, position.Latitude, position.Longitude);
            
            //var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
            //                    position.Timestamp, position.Latitude, position.Longitude,
            //                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);


            TextView sBodyShape = FindViewById<TextView>(Resource.Id.BodyShapeChosen);
            TextView sBodyLength = FindViewById<TextView>(Resource.Id.LengthChosen);
            TextView sMouthShape = FindViewById<TextView>(Resource.Id.MouthShapeChosen);
            TextView sTailShape = FindViewById<TextView>(Resource.Id.TailShapeChosen);
            TextView sBackSpine = FindViewById<TextView>(Resource.Id.BackSpineChosen);
            TextView sAnomalies = FindViewById<TextView>(Resource.Id.AnomaliesEntered);
            TextView sFamily = FindViewById<TextView>(Resource.Id.FamilyPredicted);
            TextView sLocation = FindViewById<TextView>(Resource.Id.LocationChosen);
            sBodyShape.Text = "Body Shape: \t\t\t\t\t" + bodyShape;
            sBodyLength.Text = "Body Length: \t\t\t\t\t" + BodyLength;
            sMouthShape.Text = "Mouth Shape: \t\t\t\t" + mouthShape;
            sTailShape.Text = "Tail Shape: \t\t\t\t\t\t" + tailShape;
            sBackSpine.Text = "Back Spine: \t\t\t\t\t\t" + backSpine;
            if (Anomalies == "")
                sAnomalies.Text = "Anomalies: \t\t\t\t\t\t" + "None";
            else
                sAnomalies.Text = "Anomalies: \t\t\t\t\t\t" + Anomalies;
            sFamily.Text = "Predicted Family: \t" + Family;
            sLocation.Text = "Location: " + output; 

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_top_possibleMatchings);
            SetActionBar(toolbar);
            var editToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_bottom_possibleMatchings);
            editToolbar.InflateMenu(Resource.Menu.possible_matchings_menu);
            var imageview = FindViewById<ImageView>(Resource.Id.fish_image);
            imageview.SetImageBitmap(imageTaken);
            // var textview = FindViewById<TextView>(Resource.Id.edit_toolbar_possible_matchings);
            //imageview.SetImageBitmap();

        }

        public Position getPosition()
        {
            Task<Position> pos = GetCurrentLocation();
            return pos.Result;
        }

        public static async Task<Position> GetCurrentLocation()
        {

            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;

            return position;

        }

    }
}