﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using MyDotNetCoreWpfApp.Contracts.Services;
using MyDotNetCoreWpfApp.Views;

namespace MyDotNetCoreWpfApp.Services
{
    public class WhatsNewWindowService : IWhatsNewWindowService
    {
        private readonly IServiceProvider _serviceProvider;
        private const string CurrentVersionKey = "currentVersion";
        private bool _shown = false;

        public WhatsNewWindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowIfAppropriate()
        {
            if (DetectIfAppUpdated() && !_shown)
            {
                _shown = true;
                var dialog = _serviceProvider.GetService(typeof(WhatsNewWindow)) as WhatsNewWindow;
                dialog.ShowDialog();
            }
        }

        private bool DetectIfAppUpdated()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var currentVersion = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;

            if (!App.Current.Properties.Contains(CurrentVersionKey))            
            {
                App.Current.Properties[CurrentVersionKey] = currentVersion;
            }
            else
            {
                var lastVersion = App.Current.Properties[CurrentVersionKey] as string;
                if (currentVersion != lastVersion)
                {
                    App.Current.Properties[CurrentVersionKey] = currentVersion;
                    return true;
                }
            }

            return false;
        }
    }
}