using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Views;
using System;
using Android.Support.V4.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android;

namespace App1
{
    [Activity(Label = "@string/app_name", MainLauncher = true,Icon ="@drawable/Fishlet", Theme = "@style/MyTheme")]
    public class MainActivity : Activity
    {
        protected override void OnStart()
        {
            base.OnStart();

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 0);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Permission Granted!!!");
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.Camera }, 0);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Permission Granted!!!");
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_main);
            SetActionBar(toolbar);
            TextView sBodyShape = FindViewById<TextView>(Resource.Id.promptBody);
            sBodyShape.Click += (s, arg) =>
            {
                LinearLayout body = FindViewById<LinearLayout>(Resource.Id.checkBoxBodyLayout);
                if (body.Visibility == ViewStates.Visible)
                {
                    body.Visibility = ViewStates.Gone;
                }
                else
                    body.Visibility = ViewStates.Visible;
                RadioGroup length = FindViewById<RadioGroup>(Resource.Id.checkBoxLength);
                length.Visibility = ViewStates.Gone;
                LinearLayout mouth = FindViewById<LinearLayout>(Resource.Id.checkBoxMouthLayout);
                mouth.Visibility = ViewStates.Gone;
                LinearLayout tail = FindViewById<LinearLayout>(Resource.Id.checkBoxTailLayout);
                tail.Visibility = ViewStates.Gone;
                LinearLayout backSpine = FindViewById<LinearLayout>(Resource.Id.checkBoxBackSpineLayout);
                backSpine.Visibility = ViewStates.Gone;

            };
            TextView sBodyLength = FindViewById<TextView>(Resource.Id.promptLength);
            sBodyLength.Click += (s, arg) =>
            {
                RadioGroup length = FindViewById<RadioGroup>(Resource.Id.checkBoxLength);
               
                if (length.Visibility == ViewStates.Visible)
                {
                    length.Visibility = ViewStates.Gone;
                }
                else
                    length.Visibility = ViewStates.Visible;
                LinearLayout body = FindViewById<LinearLayout>(Resource.Id.checkBoxBodyLayout);
                body.Visibility = ViewStates.Gone;
                LinearLayout mouth = FindViewById<LinearLayout>(Resource.Id.checkBoxMouthLayout);
                mouth.Visibility = ViewStates.Gone;
                LinearLayout tail = FindViewById<LinearLayout>(Resource.Id.checkBoxTailLayout);
                tail.Visibility = ViewStates.Gone;
                LinearLayout backSpine = FindViewById<LinearLayout>(Resource.Id.checkBoxBackSpineLayout);
                backSpine.Visibility = ViewStates.Gone;

            };
            TextView sMouthShape = FindViewById<TextView>(Resource.Id.promptMouth);
            sMouthShape.Click += (s, arg) =>
            {
                LinearLayout mouth = FindViewById<LinearLayout>(Resource.Id.checkBoxMouthLayout);
               
                if (mouth.Visibility == ViewStates.Visible)
                {
                    mouth.Visibility = ViewStates.Gone;
                }
                else
                    mouth.Visibility = ViewStates.Visible;
                RadioGroup length = FindViewById<RadioGroup>(Resource.Id.checkBoxLength);
                length.Visibility = ViewStates.Gone;
                LinearLayout body = FindViewById<LinearLayout>(Resource.Id.checkBoxBodyLayout);
                body.Visibility = ViewStates.Gone;
                LinearLayout tail = FindViewById<LinearLayout>(Resource.Id.checkBoxTailLayout);
                tail.Visibility = ViewStates.Gone;
                LinearLayout backSpine = FindViewById<LinearLayout>(Resource.Id.checkBoxBackSpineLayout);
                backSpine.Visibility = ViewStates.Gone;
            };
            TextView sTailShape = FindViewById<TextView>(Resource.Id.promptTail);
            sTailShape.Click += (s, arg) =>
            {
                LinearLayout tail = FindViewById<LinearLayout>(Resource.Id.checkBoxTailLayout);
               
                if (tail.Visibility == ViewStates.Visible)
                {
                    tail.Visibility = ViewStates.Gone;
                }
                else
                    tail.Visibility = ViewStates.Visible;
                LinearLayout mouth = FindViewById<LinearLayout>(Resource.Id.checkBoxMouthLayout);
                mouth.Visibility = ViewStates.Gone;
                RadioGroup length = FindViewById<RadioGroup>(Resource.Id.checkBoxLength);
                length.Visibility = ViewStates.Gone;
                LinearLayout body = FindViewById<LinearLayout>(Resource.Id.checkBoxBodyLayout);
                body.Visibility = ViewStates.Gone;
                LinearLayout backSpine = FindViewById<LinearLayout>(Resource.Id.checkBoxBackSpineLayout);
                backSpine.Visibility = ViewStates.Gone;

            };
            TextView sBackSpine = FindViewById<TextView>(Resource.Id.promptBackSpine);
            sBackSpine.Click += (s, arg) =>
            {
                LinearLayout backSpine = FindViewById<LinearLayout>(Resource.Id.checkBoxBackSpineLayout);
              
                if (backSpine.Visibility == ViewStates.Visible)
                {
                    backSpine.Visibility = ViewStates.Gone;
                }
                else
                    backSpine.Visibility = ViewStates.Visible;
                LinearLayout tail = FindViewById<LinearLayout>(Resource.Id.checkBoxTailLayout);
                tail.Visibility = ViewStates.Gone;
                LinearLayout mouth = FindViewById<LinearLayout>(Resource.Id.checkBoxMouthLayout);
                mouth.Visibility = ViewStates.Gone;
                RadioGroup length = FindViewById<RadioGroup>(Resource.Id.checkBoxLength);
                length.Visibility = ViewStates.Gone;
                LinearLayout body = FindViewById<LinearLayout>(Resource.Id.checkBoxBodyLayout);
                body.Visibility = ViewStates.Gone;

            };

            //BodyShape
            CheckBox bodyElongated = FindViewById<CheckBox>(Resource.Id.checkbox_Elongated);//Scale 0
            CheckBox bodyFusiform = FindViewById<CheckBox>(Resource.Id.checkbox_Fusiform);//Scale 1
            CheckBox bodyCompressed = FindViewById<CheckBox>(Resource.Id.checkbox_Compressed);//Scale 2
            CheckBox bodyDepressed = FindViewById<CheckBox>(Resource.Id.checkbox_Depressed);//Scale 3
            //scale 4 unknown
            CheckBox[] bodyCh = { bodyElongated, bodyFusiform, bodyCompressed, bodyDepressed };

            //BackSpine
            CheckBox backNone = FindViewById<CheckBox>(Resource.Id.checkbox_None);//Scale 0
            CheckBox backDorsal = FindViewById<CheckBox>(Resource.Id.checkbox_Dorsal_Spine);//Scale 1
            CheckBox backSerrated = FindViewById<CheckBox>(Resource.Id.checkbox_Serrated);//Scale 2
            //Scale 3 Dorsal Spine/none
            CheckBox backShortDorsal = FindViewById<CheckBox>(Resource.Id.checkbox_Short_dorsal_spine);//Scale 4
            CheckBox backLongDorsal = FindViewById<CheckBox>(Resource.Id.checkbox_Long_dorsal_spine);//Scale 5
            //Scale 6 Serrated dorsal spine
            //Scale 7 unknown combo
            CheckBox[] backCh = { backNone, backDorsal, backSerrated, backShortDorsal, backLongDorsal };

            


            //MouthShape
            CheckBox mouthTerminal = FindViewById<CheckBox>(Resource.Id.checkbox_Terminal);//Scale 0
            CheckBox mouthInferior = FindViewById<CheckBox>(Resource.Id.checkbox_Inferior);//Scale 1
            CheckBox mouthSubTerminal = FindViewById<CheckBox>(Resource.Id.checkbox_Sub_Terminal);//Scale 2
            //Scale 3 SubTerminal/Inferior
            CheckBox mouthDorsal = FindViewById<CheckBox>(Resource.Id.checkbox_Dorsal);//Scale 6 unkown
            CheckBox mouthUpturned = FindViewById<CheckBox>(Resource.Id.checkbox_Upturned);//Scale 5
            //scale 4 Dorsal/Upturned
            CheckBox[] mouthCh = { mouthTerminal, mouthInferior, mouthSubTerminal, mouthDorsal, mouthUpturned };

            //TailShape
            CheckBox tailRounded = FindViewById<CheckBox>(Resource.Id.checkbox_Rounded);//Scale 0
            CheckBox tailForked = FindViewById<CheckBox>(Resource.Id.checkbox_Forked);//Scale 1
            CheckBox tailPointed = FindViewById<CheckBox>(Resource.Id.checkbox_Pointed);//Scale 2
            CheckBox tailEmarginate = FindViewById<CheckBox>(Resource.Id.checkbox_Emarginate);//Scale 3
            CheckBox tailSemiTruncate = FindViewById<CheckBox>(Resource.Id.checkbox_Semi_Truncate);//Scale 4
            //Scale 5 Rounded & Semi truncated
            CheckBox[] tailCh = { tailRounded, tailForked, tailPointed, tailEmarginate, tailSemiTruncate };

            Classify c1 = new Classify();
            var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar_main);
            editToolbar.Title = "Continue";
            editToolbar.InflateMenu(Resource.Menu.bottom_menu);
            editToolbar.MenuItemClick += (sender, e) =>
            {
                List<string> BodyShape = new List<string>();
                List<string> BackSpine = new List<string>();
                List<string> MouthShape = new List<string>();
                List<string> TailShape = new List<string>();
                for (int i = 0; i < bodyCh.Length; i++)
                {
                    if ((bodyCh[i].Checked))
                    {
                        BodyShape.Add(bodyCh[i].Text);
                    }
                }
                for (int i = 0; i < backCh.Length; i++)
                {
                    if ((backCh[i].Checked))
                    {
                        BackSpine.Add(backCh[i].Text);
                    }
                }

                for (int i = 0; i < mouthCh.Length; i++)
                {
                    if ((mouthCh[i].Checked))
                    {
                        MouthShape.Add(mouthCh[i].Text);
                    }
                }
                for (int i = 0; i < tailCh.Length; i++)
                {
                    if ((tailCh[i].Checked))
                    {
                        TailShape.Add(tailCh[i].Text);
                    }
                }

                try
                {
                    
                    if(BodyShape.Count==0|| MouthShape.Count == 0 || TailShape.Count == 0 || BackSpine.Count == 0)
                    {
                        Toast.MakeText(this, "Please enter values", ToastLength.Short).Show();
                    }
                    else
                    {
                        //Length
                        RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.checkBoxLength);
                        RadioButton radioButton = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);
                        string BodyLength = (radioButton.Text);
                        int bodyShapeInt = c1.determineScaleBodyShape(BodyShape);
                        int backSpineInt = c1.determineScaleBackSpine(BackSpine);
                        int length = c1.determineScaleLength(BodyLength);
                        int mouthInt = c1.determineScaleMouthShape(MouthShape);
                        int tailInt = c1.determineScaleTailShape(TailShape);
                        string family = c1.ClassifyFish(bodyShapeInt, length, tailInt, mouthInt, backSpineInt);

                        Intent intent = new Intent(this, typeof(AdditionalDetails));
                        intent.PutExtra("BodyShape", BodyShape.ToArray());
                        intent.PutExtra("BodyLength", BodyLength);
                        intent.PutExtra("MouthShape", MouthShape.ToArray());
                        intent.PutExtra("TailShape", TailShape.ToArray());
                        intent.PutExtra("BackSpine", BackSpine.ToArray());
                        intent.PutExtra("Family", family);
                        this.StartActivity(intent);
                    }
                    
                }
                catch(Exception)
                {
                    Toast.MakeText(this, "Please enter values", ToastLength.Short).Show();
                }
                
            };

        }
    }
}


