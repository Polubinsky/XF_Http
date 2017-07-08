
using Android.App;
using Android.Content;
using Android.OS;
using Android.Security.Keystore;
using Java.Security;
using Javax.Crypto;


namespace UkrGo.Droid
{
    [Activity(Theme = "@style/Theme.Splash", //Indicates the theme to use for this activity
             MainLauncher = true, //Set it as boot activity
             NoHistory = true)] //Doesn't place it in back stack
    public class SplashActivity : Activity
    {
        KeyguardManager keyguardManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //Get the window from the context

            //DevicePolicyManager devicePolicyManager = (DevicePolicyManager)GetSystemService(Context.DevicePolicyService);
            //ComponentName demoDeviceAdmin = new ComponentName(this, Java.Lang.Class.FromType(typeof(DeviceAdmin)));
            //Intent intent = new Intent(DevicePolicyManager.ActionAddDeviceAdmin);
            //intent.PutExtra(DevicePolicyManager.ExtraDeviceAdmin, demoDeviceAdmin);
            //intent.PutExtra(DevicePolicyManager.ExtraAddExplanation, "Device administrator");
            //StartActivity(intent);

            StartActivity(typeof(MainActivity));
            
            //keyguardManager = (KeyguardManager)GetSystemService(Context.KeyguardService);
            //if (!keyguardManager.IsKeyguardSecure)
            //{
            //    // Show a message that the user hasn't set up a lock screen.
            //    Toast.MakeText(this, "Secure lock screen isn't set up.\n"
            //                    + "Go to 'Settings -> Security -> Screen lock' to set up a lock screen",
            //                    ToastLength.Short).Show();
            //}
            //else
            //{
            //    //We are free to create our Cyrpto Key
            //    CreateKey();
            //}


        }

        const string KeyName = "my_special_key";
        void CreateKey()
        {
            // Generate a key to decrypt payment credentials, tokens, etc.
            // This will most likely be a registration step for the user when they are setting up your app.
            var keyStore = KeyStore.GetInstance("AndroidKeyStore");
            keyStore.Load(null);
            var keyGenerator = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, "AndroidKeyStore");

            // Set the alias of the entry in Android KeyStore where the key will appear
            // and the constrains (purposes) in the constructor of the Builder
            keyGenerator.Init(new KeyGenParameterSpec.Builder(KeyName, KeyStorePurpose.Decrypt | KeyStorePurpose.Encrypt)
                                 .SetBlockModes(KeyProperties.BlockModeCbc)
                                 .SetUserAuthenticationRequired(true)
                                 // Require that the user has unlocked in the last 30 seconds
                                 .SetUserAuthenticationValidityDurationSeconds(30)
                                 .SetEncryptionPaddings(KeyProperties.EncryptionPaddingPkcs7)
                                 .Build());
            keyGenerator.GenerateKey();
        }

        static readonly byte[] SecretData = new byte[] { 1, 2, 3, 4, 5, 6 };
        void TryEncrypt()
        {
            try
            {
                var keyStore = KeyStore.GetInstance("AndroidKeyStore");
                keyStore.Load(null);
                var secretKey = keyStore.GetKey(KeyName, null);
                var cipher = Cipher.GetInstance(KeyProperties.KeyAlgorithmAes + "/" + KeyProperties.BlockModeCbc + "/" + KeyProperties.EncryptionPaddingPkcs7);
                cipher.Init(CipherMode.EncryptMode, secretKey);
                //attempt encrypting data
                cipher.DoFinal(SecretData);
                // If the user has recently authenticated, you will reach here.
            }
            catch (UserNotAuthenticatedException)
            {
                // User is not authenticated, let's authenticate with device credentials.
                ShowAuthenticationScreen();
            }
        }

        static readonly int ConfirmRequestId = 1;
        void ShowAuthenticationScreen()
        {
            var intent = keyguardManager.CreateConfirmDeviceCredentialIntent((string)null, (string)null);
            if (intent != null)
            {
                StartActivityForResult(intent, ConfirmRequestId);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == ConfirmRequestId)
            {
                // Credentials entered successfully!
                if (resultCode == Result.Ok)
                {
                    this.StartActivity(typeof(MainActivity));
                }
                else
                {
                    // The user canceled or didn’t complete the lock screen
                    // operation. Go to error/cancellation flow.
                }
            }
        }
    }
}