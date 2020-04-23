using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicPlayer.Helpers
{
    public static class DependencyHelper
    {
        public static IContainer Container { get; private set; }
        public static readonly ContainerBuilder Builder = new ContainerBuilder();

        public static void BuildContainer() =>
            Container = Builder.Build();
    }
}
