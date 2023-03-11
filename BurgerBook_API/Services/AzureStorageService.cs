using System;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace BurgerBook.Services
{
	public class AzureStorageService
	{
        private static readonly string BURGER_BOOK_IMAGES_BLOB_CONTAINER = "burgerbookimages";
		private BlobServiceClient _blobServiceClient;

        public AzureStorageService()
		{
            //Recommended Azure default authentication
            this._blobServiceClient = new BlobServiceClient(
                    new Uri("https://kisslacstorage.blob.core.windows.net"),
                    new DefaultAzureCredential());
        }

        public async Task<string> UploadBlobToStorage(string imageType, Stream imageStream, string existingImageName = "")
        {
            var imageGuid = Guid.NewGuid();
            var bloblName = imageGuid + "." + imageType;

            if (!string.IsNullOrEmpty(existingImageName))
            {
                bloblName = existingImageName;
            }

            var response = await this._blobServiceClient
                .GetBlobContainerClient(BURGER_BOOK_IMAGES_BLOB_CONTAINER)
                .UploadBlobAsync(bloblName, imageStream);

            if(response != null)
            {
                return bloblName;
            }

            return string.Empty;
        }
    }
}

