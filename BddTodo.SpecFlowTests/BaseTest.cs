using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using Respawn;
using BddTodo.Tests._Infrastructure;
using BddTodo.Tests._Infrastructure.Extensions;
using System.IO;
using Microsoft.Extensions.Configuration;
using BddTodo.Data;
using BddTodo.Controllers.Users.Authenticate.Commands;
using TechTalk.SpecFlow;

namespace BddTodo.Tests
{
    public class BaseTest
    {
        public static readonly Checkpoint Checkpoint = new Checkpoint
        {
            SchemasToExclude = new[]
            {
                "ref"
            },
            TablesToIgnore = new[]
            {
                "__EFMigrationsHistory",
                "UserRole",
                "UserAccountStatus"
            }
        };


        private FakeOAuthAuthenticationResponse _authenticationResult;
        private string _jwt;

        [BeforeScenario]
        public void BeforeScenario()
        {
            try
            {
                if (DatabaseHelpers.ConnString.ToLower().Contains("test"))
                {
                    Checkpoint.Reset(DatabaseHelpers.ConnString);
                }

                SetUpBddTodoDbContext();
                SetUpTestServer();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected void SetUpTestServer()
        {
            TestServer = new WebApplicationFactory<Startup>();

            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");
            TestServer = TestServer.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });
            });
        }

        protected void SetUpBddTodoDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BddTodoDbContext>();
            optionsBuilder.UseSqlServer(DatabaseHelpers.ConnString);

            Context = new BddTodoDbContext(optionsBuilder.Options);
            Context.Database.Migrate();
        }

        public BddTodoDbContext Context { get; set; }
        public WebApplicationFactory<Startup> TestServer { get; private set; }

        public void Authenticate(string username, string password = "testpw")
        {
            var authUserCommand = new PostAuthenticateUserCommand
            {
                Username = username,
                Password = password
            };
            var result = CallClientPost<PostAuthenticateUserCommandResult>("api/users/authenticate", authUserCommand);
            _jwt = result.Token;
        }


        public T CallClientGet<T>(string url)
        {
            using (var client = TestServer.CreateClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                if (!_jwt.IsNullOrEmpty())
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                var response = client.GetAsync(url)
                    .Result;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(message);
                }

                if (!response.IsSuccessStatusCode)
                    return Default<T>.Value;

                return JsonConvert.DeserializeObject<T>(
                    response.Content.ReadAsStringAsync().Result);
            }
        }

        public T CallClientPost<T>(string url, object value)
        {
            using (var client = TestServer.CreateClient())
            {
                using (var request = new System.Net.Http.HttpRequestMessage())
                {
                    client.BaseAddress = new Uri("http://localhost:5000");

                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!_jwt.IsNullOrEmpty())
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                    var content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(value));
                    content.Headers.ContentType.MediaType = "application/json";
                    request.RequestUri = new Uri(client.BaseAddress + url);
                    request.Content = content;
                    request.Method = new System.Net.Http.HttpMethod("POST");

                    if (!_jwt.IsNullOrEmpty())
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                    var response = client.SendAsync(request).Result;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(message);
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        return Default<T>.Value;
                    }


                    return JsonConvert.DeserializeObject<T>(
                        response.Content.ReadAsStringAsync().Result);
                }
            }

        }

        public T CallClientPut<T>(string url, object value)
        {


            using (var client = TestServer.CreateClient())
            {
                using (var request = new System.Net.Http.HttpRequestMessage())
                {
                    var content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(value));
                    content.Headers.ContentType.MediaType = "application/json";
                    request.RequestUri = new Uri(client.BaseAddress + url);
                    request.Content = content;
                    request.Method = new System.Net.Http.HttpMethod("PUT");
                    request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    if (!_jwt.IsNullOrEmpty())
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                    var response = client.SendAsync(request).Result;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(message);
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        return Default<T>.Value;
                    }


                    return JsonConvert.DeserializeObject<T>(
                        response.Content.ReadAsStringAsync().Result);
                }
            }

        }

        public T CallClientPatch<T>(string url, object value)
        {


            using (var client = TestServer.CreateClient())
            {
                using (var request = new System.Net.Http.HttpRequestMessage())
                {
                    var content = new System.Net.Http.StringContent(JsonConvert.SerializeObject(value));
                    content.Headers.ContentType.MediaType = "application/json";
                    request.RequestUri = new Uri(client.BaseAddress + url);
                    request.Content = content;
                    request.Method = new System.Net.Http.HttpMethod("PATCH");
                    request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    if (!_jwt.IsNullOrEmpty())
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                    var response = client.SendAsync(request).Result;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(message);
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        return Default<T>.Value;
                    }


                    return JsonConvert.DeserializeObject<T>(
                        response.Content.ReadAsStringAsync().Result);
                }
            }

        }

        public T CallClientDelete<T>(string url)
        {
            using (var client = TestServer.CreateClient())
            {
                using (var request = new System.Net.Http.HttpRequestMessage())
                {
                    request.RequestUri = new Uri(client.BaseAddress + url);
                    request.Method = new System.Net.Http.HttpMethod("DELETE");
                    request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    if (!_jwt.IsNullOrEmpty())
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwt);

                    var response = client.SendAsync(request).Result;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(message);
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        return Default<T>.Value;
                    }

                    return JsonConvert.DeserializeObject<T>(
                        response.Content.ReadAsStringAsync().Result);
                }
            }

        }

    }
}
