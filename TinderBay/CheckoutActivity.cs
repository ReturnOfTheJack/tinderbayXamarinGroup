﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Com.Paypal.Android.Sdk.Payments;
using Java.Math;


/* Coded by: Danny
 * 
 * "LQFUY6ESACZKU"
 * TinderBayTester@test.com.au
 * tinderbaytest123 */

namespace TinderBay
{
    [Activity(Label = "CheckoutActivity")]
    public class CheckoutActivity : Activity
    {
        private PayPalConfiguration config = new PayPalConfiguration()
     .Environment(PayPalConfiguration.EnvironmentSandbox)
     .ClientId("AcR8bCwFgG6GHUG96oFFrG6e7SX9E5LGtvezyzDaYdMj_vuM--glz3W-JvdXpGSie3BU8nhRJHIfEM5n");

        protected Button btnToAccount;
        protected Button btnToHome;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);Window.RequestFeature(Android.Views.WindowFeatures.NoTitle);

            // Set out view from the layout resource
            SetContentView(Resource.Layout.ProfileLayout);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.buybutton);
            button.Click += Button_Click;

            btnToHome = FindViewById<Button>(Resource.Id.btnToHome);
            btnToAccount = FindViewById<Button>(Resource.Id.btnToAccount);

            btnToAccount.Click += BtnToAccount_Click;
            btnToHome.Click += BtnToHome_Click;

            // start paypal service
            var intent = new Intent(this, typeof(PayPalService));
            intent.PutExtra(PayPalService.ExtraPaypalConfiguration, config);
            this.StartService(intent);
        }

        private void Button_Click(object sender, EventArgs eventArgs)
        {
            var payment = new PayPalPayment(new BigDecimal("2.45"), "AUD", "the item",
                PayPalPayment.PaymentIntentSale);

            var intent = new Intent(this, typeof(PaymentActivity));
            intent.PutExtra(PayPalService.ExtraPaypalConfiguration, config);
            intent.PutExtra(PaymentActivity.ExtraPayment, payment);

            this.StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Ok)
            {
                var confirm = data.GetParcelableExtra(PaymentActivity.ExtraResultConfirmation);
                if (confirm != null)
                {

                }
            }
            else if (resultCode == Result.Canceled)
            {

            }
            else if ((int)resultCode == PaymentActivity.ResultExtrasInvalid)
            {

            }
        }

        protected override void OnDestroy()
        {
            this.StopService(new Intent(this, typeof(PayPalService)));
            base.OnDestroy();
        }

        public void BtnToAccount_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ProfileActivity));
        }

        public void BtnToHome_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HomeActivity));
        }
    }
}
