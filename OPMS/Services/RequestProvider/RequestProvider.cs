using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using OPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPMS.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {


        private readonly JsonSerializerSettings _serializerSettings;

        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }


        private HttpClient CreateHttpClient(string token = "")
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return httpClient;
        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                return;

            httpClient.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
        }

        public async Task<TResult> DeleteAsync<TResult>(string uri, TResult response, Dictionary<string, string> queryParameters, string token = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(token);

                var uriWithQueryParameters = QueryHelpers.AddQueryString(uri, queryParameters);
                HttpResponseMessage responseMsg = await httpClient.DeleteAsync(uriWithQueryParameters);

                return await HandleResponse<TResult>(responseMsg);
            }
            catch (Exception e)
            {
                App.Logger.Error(e.Message);
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return default;
            }
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(token);
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                return await HandleResponse<TResult>(response);
            }
            catch (Exception e)
            {
                App.Logger.Error(e.Message);
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return default;
            }
            
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {

            try
            {
                HttpClient httpClient = CreateHttpClient(token);

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                return await HandleResponse<TResult>(response); ;
            }
            catch (Exception e)
            {
                App.Logger.Error(e.Message);
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return default;
            }
            
        }

        public Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret)
        {
            throw new NotImplementedException();
        }

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(token);

                if (!string.IsNullOrEmpty(header))
                {
                    AddHeaderParameter(httpClient, header);
                }

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PutAsync(uri, content);

                return await HandleResponse<TResult>(response); ;
            }
            catch (Exception e)
            {
                App.Logger.Error(e.Message);
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return default;
            }
        }

        private async Task<TResult> HandleResponse<TResult>(HttpResponseMessage response)
        {
            try
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return HandleOkResponse<TResult>(JsonConvert.DeserializeObject<ResponseModel>(responseContent, _serializerSettings));
                    case HttpStatusCode.BadRequest:
                        return HandleBadRequest<TResult>(JsonConvert.DeserializeObject<ResponseModel>(responseContent, _serializerSettings));
                    default:
                        App.Logger.Fatal(responseContent.ToString());
                        MessageBox.Show(response.StatusCode.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return default;
                }
            }
            catch (Exception e)
            {
                App.Logger.Error(e.Message);
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return default;
            }

            

        }

        private TResult HandleOkResponse<TResult>(ResponseModel responseModel)
        {

            if (responseModel.Status.Equals(ResponseStatusCode.success.ToString()))
            {
                var model = JsonConvert.DeserializeObject<TResult>(responseModel.Data.ToString(), _serializerSettings);
                return (TResult) model;
            }
            else
            {
                var error = JsonConvert.DeserializeObject<TResult>(responseModel.Data.ToString(), _serializerSettings);
                App.Logger.Error((error as ModelBase).Error.ErrorMessage);
                MessageBox.Show((error as ModelBase).Error.ErrorMessage, (error as ModelBase).Error.ErrorTitle ,MessageBoxButton.OK, MessageBoxImage.Error);

                return default;
            }
        }

        private TResult HandleBadRequest<TResult>(ResponseModel responseModel)
        {
            var error = JsonConvert.DeserializeObject<TResult>(responseModel.Data.ToString(), _serializerSettings);
            App.Logger.Error((error as ModelBase).Error.ErrorMessage);
            MessageBox.Show((error as ModelBase).Error.ErrorMessage, (error as ModelBase).Error.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            return default;
        }
    }

    public enum ResponseStatusCode
    {
        success,
        fail,
        error
    }

    public enum ErrorCodes
    {
        invalidData = 100,
        dataNotFound = 101
    }
}
