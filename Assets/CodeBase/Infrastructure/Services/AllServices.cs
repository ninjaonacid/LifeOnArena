using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class AllServices 
    {
        private static AllServices _instance;

        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public void RegisterSingle<TService>(TService implementation) 
            where TService : IService =>

            Implementation<TService>.ServiceInstance = implementation;
        
        

        public TService Single<TService>() where TService : IService =>
           Implementation<TService>.ServiceInstance;
        
        

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}
