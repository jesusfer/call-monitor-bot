// < copyright file = "GraphConfiguration.cs" company = "Microsoft Corporation" >
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
// </copyright>

using CallingBotSample.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using Azure.Core;
using Azure.Identity;

namespace CallingBotSample.Extensions
{
    /// <summary>
    /// Graph Configuration.
    /// </summary>
    public static class GraphConfiguration
    {
        /// <summary>
        /// Configure Graph Component.
        /// </summary>
        /// <param name="services">IServiceCollection .</param>
        /// <param name="configuration">IConfiguration .</param>
        /// <returns>..</returns>
        public static IServiceCollection ConfigureGraphComponent(this IServiceCollection services, Action<AzureAdOptions> azureAdOptionsAction)
        {
            var options = new AzureAdOptions();
            // Execute the delegate to populate the options instance.
            azureAdOptionsAction(options);

           var cred = new ClientSecretCredential(options.TenantId, options.ClientId, options.ClientSecret);

            services.AddScoped<GraphServiceClient, GraphServiceClient>(sp =>
            {
                return new GraphServiceClient(cred);
            });

            return services;
        }
    }
}