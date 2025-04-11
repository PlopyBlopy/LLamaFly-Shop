using Core.Interfaces;

namespace API.Properties
{
    public class WebStoragePathProvider : IStoragePathProvider
    {
        private readonly string _storageRoot;

        public WebStoragePathProvider(IConfiguration configuration, IWebHostEnvironment env)
        {
            var basePath = configuration["Storage:RootPath"] ?? env.ContentRootPath;

            //_storageRoot = Path.Combine(
            //    basePath,
            //    "products"
            //);
            _storageRoot = "/app/storage/images/products";
            EnsureDirectoryExists(_storageRoot);
        }

        private void EnsureDirectoryExists(string path)
        {
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //    // Для Linux устанавливаем права
            //    if (Environment.OSVersion.Platform == PlatformID.Unix)
            //    {
            //        File.SetUnixFileMode(path,
            //            UnixFileMode.UserRead |
            //            UnixFileMode.UserWrite |
            //            UnixFileMode.UserExecute |
            //            UnixFileMode.GroupRead |
            //            UnixFileMode.OtherRead);
            //    }
            //}
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                File.SetUnixFileMode(path,
                    UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute |
                    UnixFileMode.GroupRead | UnixFileMode.GroupWrite |
                    UnixFileMode.OtherRead);
            }
        }

        public string GetStorageRootPath() => _storageRoot;
    }
}