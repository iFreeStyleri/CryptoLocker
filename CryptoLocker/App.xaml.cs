using CryptoLocker.Abstractions;
using CryptoLocker.Implements;
using CryptoLocker.Models;
using CryptoLocker.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoLocker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string DirectoryPath = @"G:\God of War\exec\cinematics";
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            _serviceProvider = ConfigureServices().BuildServiceProvider();
        }

        public IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<Log>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<Configurator>();
            services.AddSingleton<DecryptConfigurator>();
            services.AddSingleton<EncryptConfigurator>();
            services.AddTransient<IEncryptService, EncryptService>();
            services.AddTransient<IDecryptService, DecryptService>();
            return services;
        }

        public void OnStartUp(object sender, StartupEventArgs e)
        {
            var config = _serviceProvider.GetRequiredService<Configurator>();
            config.Execute();
            _serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }
}
