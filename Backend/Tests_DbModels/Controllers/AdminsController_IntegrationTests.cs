using Backend;
using Backend.Controllers;
using Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore;

namespace Tests
{


    public class AdminsController_IntegrationTests : IClassFixture<WebApplicationFactory<Backend.Startup>>
    {
        private TestServer _server;
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Backend.Startup> _factory;
        ConfigurationRoot Configuration;
        string big_string = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        Admins item_with_valid_data = new Admins()
        {
            Email = "email@email.com",
            Password = "Password",
            FullName = "Full Name"
        };

        Admins item_without_email = new Admins()
        {
            Password = "Password",
            FullName = "Full Name"
        };

        Admins item_without_password = new Admins()
        {
            Email = "email@email.com",
            FullName = "Full Name"
        };

        Admins item_without_full_name = new Admins()
        {
            Email = "email@email.com",
            Password = "Password",
        };

        Admins item_enabled = new Admins()
        {
            Email = "email@email.com",
            Password = "Password",
            FullName = "Full Name",
            IsActive = true
        };

        Admins item_disabled = new Admins()
        {
            Email = "email@email.com",
            Password = "Password",
            FullName = "Full Name",
            IsActive = false
        };
            
        public AdminsController_IntegrationTests(WebApplicationFactory<Backend.Startup> factory)
        {
            string configurationPath = "appsettings.json";
            ConfigurationBuilder confBuilder = new ConfigurationBuilder();
            confBuilder = confBuilder.AddJsonFile(configurationPath, optional: true, reloadOnChange: true) as ConfigurationBuilder;
            Configuration = confBuilder.Build() as ConfigurationRoot;
            Configuration["DatabaseConnectionString"] = string.Format("Host={0};Port={1};Database={2};Username={3};Password={4}", Configuration["Database:Host"], Configuration["Database:Port"], Configuration["Database:Database"], Configuration["Database:User"], Configuration["Database:Password"]);
            WebHostBuilder builder = new WebHostBuilder();
            builder.UseConfiguration(Configuration).UseStartup<Startup>();
            _server = new TestServer(builder);
            _server.BaseAddress =new Uri(Configuration["Jwt:Issuer"]);
            _client = _server.CreateClient();
        }

        cap01devContext db = null;

        private void Init()
        {
            if (db == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<cap01devContext>();
                optionsBuilder = optionsBuilder.UseNpgsql(Configuration["DatabaseConnectionString"]);
                db = new cap01devContext(optionsBuilder.Options);
            }
            Admins.Clear(db);
        }


        [Fact]
        public async Task create_new_valid_admin()
        {

            Init();
            RequestData requestData = new RequestData();

            requestData.Add("email", item_with_valid_data.Email);
            requestData.Add("comment", item_with_valid_data.Comment);
            requestData.Add("full_name", item_with_valid_data.FullName);
            requestData.Add("password_hash", item_with_valid_data.PasswordHash);

            StringContent content = new StringContent(requestData.Serialize(), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("ca/manage/new", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            if (!"{\"resultCode\":201,\"message\":\"Created\",\"nextURI\":\"\",\"prevURI\":null,\"data\":null}".Equals(responseString))
            {
                throw new Exception("Received not expected responce");
            }
        }



        [Fact]
        public async Task create_new_admin_without_email()
        {

            Init();
            RequestData requestData = new RequestData();

            requestData.Add("email", item_without_email.Email);
            requestData.Add("comment", item_without_email.Comment);
            requestData.Add("full_name", item_without_email.FullName);
            requestData.Add("password_hash", item_without_email.PasswordHash);

            StringContent content = new StringContent(requestData.Serialize(), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("ca/manage/new", content);

            if (response.StatusCode != System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception("Test failed");
            }
        }



        [Fact]
        public async Task create_new_admin_without_password()
        {

            Init();
            RequestData requestData = new RequestData();

            requestData.Add("email", item_without_password.Email);
            requestData.Add("comment", item_without_password.Comment);
            requestData.Add("full_name", item_without_password.FullName);
            requestData.Add("password_hash", item_without_password.PasswordHash);

            StringContent content = new StringContent(requestData.Serialize(), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("ca/manage/new", content);

            if (response.StatusCode != System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception("Test failed");
            }
        }


        [Fact]
        public async Task create_new_admin_without_full_name()
        {

            Init();
            RequestData requestData = new RequestData();

            requestData.Add("email", item_without_full_name.Email);
            requestData.Add("comment", item_without_full_name.Comment);
            requestData.Add("full_name", item_without_full_name.FullName);
            requestData.Add("password_hash", item_without_full_name.PasswordHash);

            StringContent content = new StringContent(requestData.Serialize(), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("ca/manage/new", content);

            if (response.StatusCode != System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception("Test failed");
            }
        }


        [Fact]
        public async Task enabling_admin()
        {

            Init();
            Admins.Add(item_disabled,db);

            RequestData requestData = new RequestData();

            requestData.Add("email", item_disabled.Email);

            StringContent content = new StringContent(requestData.Serialize(), Encoding.UTF8, "application/json");

            var response = await _client.PatchAsync("ca/manage/poweron", content);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task disabling_admin()
        {

            Init();
            Admins.Add(item_enabled,db);

            RequestData requestData = new RequestData();

            requestData.Add("email", item_disabled.Email);

            StringContent content = new StringContent(requestData.Serialize(), Encoding.UTF8, "application/json");

            var response = await _client.PatchAsync("ca/manage/shutdown", content);
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task delete_admin()
        {

            Init();
            Admins.Add(item_with_valid_data,db);

            RequestData requestData = new RequestData();

            requestData.Add("email", item_with_valid_data.Email);

            StringContent content = new StringContent(requestData.Serialize(), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("ca/manage/delete", content);
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task list_admins()
        {

            Init();
            Admins.Add(item_with_valid_data,db);


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "ca/manage/list");
            request.Content = new StringContent("",Encoding.UTF8,"application/json");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task test_incorrect_args()
        {

            Init();

            //method ca/manage/add
            StringContent content = new StringContent(big_string, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("ca/manage/new", content);
            if (response.StatusCode != System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception("test_incorrect_args failed. Method \"ca/manage/new\"");
            }

            // method ca/manage/poweron
            content = new StringContent(big_string, Encoding.UTF8, "application/json");
            response = await _client.PatchAsync("ca/manage/poweron", content);
            if (response.StatusCode != System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception("test_incorrect_args failed. Method \"ca/manage/poweron\"");
            }

            //method ca/manage/shutdown
            content = new StringContent(big_string, Encoding.UTF8, "application/json");
            response = await _client.PatchAsync("ca/manage/shutdown", content);
            if (response.StatusCode != System.Net.HttpStatusCode.Conflict)
            {
                throw new Exception("test_incorrect_args failed. Method \"ca/manage/shutdown\"");
            }

        }
    }
}
