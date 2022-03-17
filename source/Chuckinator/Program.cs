using Autofac;
using Chuckinator;

var builder = new ContainerBuilder();
builder.RegisterModule<ChuckinatorModule>();
var container = builder.Build();
var application = container.Resolve<Application>();
await application.Run();