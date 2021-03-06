using ServiceStack.Common.Web;

namespace ServiceStack.WebHost.Endpoints
{
    public class ServiceEndpointsMetadataConfig
    {
        private ServiceEndpointsMetadataConfig() { }

        /// <summary>
        /// Changes the links for the servicestack/metadata page
        /// </summary>
        /// <param name="serviceStackHandlerPrefix"></param>
        /// <returns></returns>
        public static ServiceEndpointsMetadataConfig Create(string serviceStackHandlerPrefix)
        {
            var config = new MetadataConfig("{0}", "{0}", "/{0}/reply", "/{0}/oneway", "/{0}/metadata");
            return new ServiceEndpointsMetadataConfig {
                DefaultMetadataUri = "/metadata",
                Soap11 = new SoapMetadataConfig("soap11", "SOAP 1.1", "/soap11", "/soap11", "/soap11/metadata", "soap11"),
                Soap12 = new SoapMetadataConfig("soap12", "SOAP 1.2", "/soap12", "/soap12", "/soap12/metadata", "soap12"),
                Xml = config.Create("xml"),
                Json = config.Create("json"),
                Jsv = config.Create("jsv"),
                Custom = config
            };
        }

        public string DefaultMetadataUri { get; set; }
        public SoapMetadataConfig Soap11 { get; set; }
        public SoapMetadataConfig Soap12 { get; set; }
        public MetadataConfig Xml { get; set; }
        public MetadataConfig Json { get; set; }
        public MetadataConfig Jsv { get; set; }
        public MetadataConfig Custom { get; set; }

        public MetadataConfig GetEndpointConfig(string contentType)
        {
            switch (contentType)
            {
                case ContentType.Soap11:
                    return this.Soap11;
                case ContentType.Soap12:
                    return this.Soap12;
                case ContentType.Xml:
                    return this.Xml;
                case ContentType.Json:
                    return this.Json;
                case ContentType.Jsv:
                    return this.Jsv;
            }

            var format = ContentType.GetContentFormat(contentType);
            return Custom.Create(format);
        }
    }
}