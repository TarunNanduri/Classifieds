using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace Classifieds.App.Services.AzureRepositories
{
    public class UploadImage
    {
        public UploadImage(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        private void ConvertByteArrayToImage(byte[] byteArrayIn, string fileName)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                Image.FromStream(ms).Save(fileName);
            }
        }

        public async Task<string> UploadImages(string data)
        {
            var fileName = Guid.NewGuid() + ".jpg";
            var storageConnectionString = Configuration["storage-connection-string"];
            if (!CloudStorageAccount.TryParse(storageConnectionString, out var storageAccount)) return "";
            try
            {
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();

                var cloudBlobContainer = cloudBlobClient.GetContainerReference(Configuration["container"]);
                var imgBase64String = Convert.FromBase64String(data);
                ConvertByteArrayToImage(imgBase64String, fileName);
                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                await cloudBlockBlob.UploadFromFileAsync(fileName);
                var blobUrl = cloudBlockBlob.Uri.AbsoluteUri;
                return blobUrl;
            }
            finally
            {
                File.Delete(fileName);
            }
        }

        public void DeleteBlob(string url)
        {
            var storageConnectionString = Configuration["storage-connection-string"];
            if (!CloudStorageAccount.TryParse(storageConnectionString, out var storageAccount)) return;
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            var cloudBlobContainer =
                cloudBlobClient.GetContainerReference(Configuration["container"]);
            var blobName = url.Split('/').Last();
            var blob = cloudBlobContainer.GetBlockBlobReference(blobName);
            blob.DeleteIfExists();
        }
    }
}