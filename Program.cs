using Server.Hubs;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace Server
{
    public class Program
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        public static void Main(string[] args)
        {
            string[] config = GetConfig();
            string adress = config[0];
            string port = config[1];
            var builder = WebApplication.CreateBuilder(new string[] {"--urls", $"http://{adress}:{port}" });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<MainHub>("/main");
            app.MapControllers();
            

            app.Run();
        }

        public static string[] GetConfig()
        {
            string[] configBody = new string[] { "localhost", "21758" };
            string configPath = Environment.CurrentDirectory + "/networkConfig.json";

            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath, JsonConvert.SerializeObject(configBody));
                return configBody;
            }

            string config = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<string[]>(config);
        }
    }
}