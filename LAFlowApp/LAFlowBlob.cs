using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Logging;


namespace LAFlowApp
{
    public static class LAFlowBlob
    {
        [FunctionName("WriteFlowFile")]
        public static void Run([BlobTrigger("laflowblob/{name}")]
                                CloudBlockBlob cloudBlockBlob,
                                [Blob("laflowblob/{name}",
                                FileAccess.ReadWrite)]
                                byte[] blobContents,
                                ILogger logger)
        {

            string tempDir = Path.Combine(Path.GetTempPath(), "MyData");
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            var tempFile = Path.Combine(tempDir, "test.png");
            File.WriteAllBytes(tempFile, blobContents);
            var result = File.ReadAllBytes(tempFile);
            logger.LogInformation(Encoding.UTF8.GetString(result));



        }
    }
}
