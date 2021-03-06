/* 
 * Higgs API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using NUnit.Framework;

using IO.Swagger.Client;
using IO.Swagger.Api;
using IO.Swagger.Model;

namespace IO.Swagger.Test
{
    /// <summary>
    ///  Class for testing AdminApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class AdminApiTests
    {
        private AdminApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new AdminApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of AdminApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' AdminApi
            //Assert.IsInstanceOfType(typeof(AdminApi), instance, "instance is a AdminApi");
        }

        
        /// <summary>
        /// Test AdminBotGet
        /// </summary>
        [Test]
        public void AdminBotGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? botId = null;
            //var response = instance.AdminBotGet(botId);
            //Assert.IsInstanceOf<BotResponse> (response, "response is BotResponse");
        }
        
        /// <summary>
        /// Test AdminBotsGet
        /// </summary>
        [Test]
        public void AdminBotsGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.AdminBotsGet();
            //Assert.IsInstanceOf<List<BotsResponse>> (response, "response is List<BotsResponse>");
        }
        
        /// <summary>
        /// Test AdminDeactiveateBotPost
        /// </summary>
        [Test]
        public void AdminDeactiveateBotPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //DeleteBotRequest request = null;
            //instance.AdminDeactiveateBotPost(request);
            
        }
        
        /// <summary>
        /// Test AdminEditBotPost
        /// </summary>
        [Test]
        public void AdminEditBotPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //EditBotRequest request = null;
            //instance.AdminEditBotPost(request);
            
        }
        
        /// <summary>
        /// Test AdminForceProcessReportsPost
        /// </summary>
        [Test]
        public void AdminForceProcessReportsPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.AdminForceProcessReportsPost();
            
        }
        
        /// <summary>
        /// Test AdminRegisterBotPost
        /// </summary>
        [Test]
        public void AdminRegisterBotPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateBotRequest request = null;
            //var response = instance.AdminRegisterBotPost(request);
            //Assert.IsInstanceOf<int?> (response, "response is int?");
        }
        
        /// <summary>
        /// Test AdminScopesGet
        /// </summary>
        [Test]
        public void AdminScopesGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.AdminScopesGet();
            //Assert.IsInstanceOf<List<string>> (response, "response is List<string>");
        }
        
        /// <summary>
        /// Test AdminUserGet
        /// </summary>
        [Test]
        public void AdminUserGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? userId = null;
            //var response = instance.AdminUserGet(userId);
            //Assert.IsInstanceOf<UsersResponse> (response, "response is UsersResponse");
        }
        
        /// <summary>
        /// Test AdminUserPost
        /// </summary>
        [Test]
        public void AdminUserPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //UpdateUserRequest request = null;
            //instance.AdminUserPost(request);
            
        }
        
        /// <summary>
        /// Test AdminUsersGet
        /// </summary>
        [Test]
        public void AdminUsersGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.AdminUsersGet();
            //Assert.IsInstanceOf<List<UsersResponse>> (response, "response is List<UsersResponse>");
        }
        
    }

}
