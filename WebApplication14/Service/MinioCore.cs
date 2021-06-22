using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApplication14.ResultCode;

namespace WebApplication14.Service
{
    public static class MinioCore<T>
    {
        public async static Task<ResultObject> UpdateFile(string objectName)
        {
            ResultObject result = new ResultObject();
            var log = new StringBuilder();
            try
            {
                var minio = new MinioClient(Consts.Consts.Endpoint, Consts.Consts.AccessKey, Consts.Consts.SecretKey);
                var client = new WebClient();
                byte[] data = await client.DownloadDataTaskAsync("https://i-sohoa.vnecdn.net/2020/10/22/akrales201018423804350webp-160-2149-7865-1603357307_680x0.jpg");
                MemoryStream filestream = new MemoryStream(data);
                var filePath = @"D:\jour111nalist.png";
                log.AppendLine($"filePath: {filePath}");
                var extension = Path.GetExtension(filePath).ToLowerInvariant();
                var filename = Path.GetFileName(filePath).ToLowerInvariant();
                bool butket = await minio.BucketExistsAsync(Consts.Consts.Bucket);
                if (butket)
                {
                    await minio.RemoveObjectAsync(Consts.Consts.Bucket, objectName);
                    await minio.PutObjectAsync(Consts.Consts.Bucket, Consts.Consts.Folder + filename, filestream, filestream.Length, GetMimeTypes()[extension]);
                    result.Code = 1;
                    result.Message = "Update file successfully";
                    result.Url = string.Concat(Consts.Consts.Bucket, "/", Consts.Consts.Folder, filename);
                    return result;
                }
                else
                {
                    result.Code = -1;
                    result.Message = "Bucket not fund";
                }
            }
            catch (Exception ex)
            {
                result.Code = -1;
                result.Message = "Update file failure: " + "Exception: " + ex.Message;
                result.Url = "";
                log.AppendLine($"Exception: {ex}");
            }
            return result;
        }
            private static Dictionary<string, string> GetMimeTypes()
            {
                return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
            };
            }
    }
}
