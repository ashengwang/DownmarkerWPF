using System;
using System.ServiceModel;
using Autofac;
using MarkPad.Services.Services;

namespace MarkPad.Shell
{
    /// <summary>
    /// Service Host for listening to the "Open File" commands from other processes
    /// </summary>
    public class OpenFileListener : IDisposable
    {
        private readonly IComponentContext context;
        private ServiceHost host;

        public OpenFileListener(IComponentContext context)
        {
            this.context = context;
        }

        public void Start()
        {
            var factory = new AutofacServiceFactory(context);
            host = factory.CreateService(typeof(OpenFileCommand), new[] { new Uri("net.pipe://localhost") });
            host.AddServiceEndpoint(typeof(IOpenFileCommand), new NetNamedPipeBinding(), "OpenFileCommand");
            host.Open();
        }

        public void Dispose()
        {
            host.Close();
        }
    }
}
