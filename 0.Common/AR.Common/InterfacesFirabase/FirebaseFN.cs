using AR.Common.Dto;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace AR.Common.InterfacesFirabase
{
    public class FirebaseFN : IFirebaseFN
    {
        private readonly AppFirebase appFirebase;
        public FirebaseFN(IOptions<AppFirebase> options)
        {
            appFirebase = options.Value;
        }
        public async Task<ResponseDto<string>> UploadStorage(Stream stream, string folder, string name)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                if (string.IsNullOrEmpty(appFirebase.emailFirebase))
                {
                    result.Message = "Configure el email de firebase";
                    return result;
                }

                if (string.IsNullOrEmpty(appFirebase.passwordFirebase))
                {
                    result.Message = "Configure su contraseña de firebase";
                    return result;
                }

                if (string.IsNullOrEmpty(appFirebase.routerFirebase))
                {
                    result.Message = "Configure su ruta del storage de firebase";
                    return result;
                }

                if (string.IsNullOrEmpty(appFirebase.apiKeyFirebase))
                {
                    result.Message = "Configure su api de firebase";
                    return result;
                }

                if (string.IsNullOrEmpty(appFirebase.domainFirebase))
                {
                    result.Message = "Configure su dominio de firebase";
                    return result;
                }
                var config = new FirebaseAuthConfig
                {
                    ApiKey = appFirebase.apiKeyFirebase,
                    AuthDomain = appFirebase.domainFirebase,
                    Providers = new FirebaseAuthProvider[]
                    {
                        new EmailProvider()
                    },
                };
                var client = new FirebaseAuthClient(config);
                var resultAuth = await client.SignInWithEmailAndPasswordAsync(appFirebase.emailFirebase, appFirebase.passwordFirebase);
                var cancellation = new CancellationTokenSource();
                var token = await resultAuth.User.GetIdTokenAsync();
                var task = new FirebaseStorage(
                    appFirebase.routerFirebase,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(token),
                        ThrowOnCancel = true
                    }).Child(folder).Child(name).PutAsync(stream, cancellation.Token);
                result.Data = await task;
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}