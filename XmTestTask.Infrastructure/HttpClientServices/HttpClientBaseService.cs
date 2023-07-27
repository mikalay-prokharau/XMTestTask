namespace XmTestTask.Infrastructure.HttpClientServices
{
    public abstract class HttpClientBaseService
    {
        private readonly HttpClient client;

        public HttpClientBaseService(HttpClient client)
        {
            this.client = client;
        }

        protected async Task<string?> SendRequest(string url, CancellationToken cancelationToken = default)
        {
            var response = await client.GetAsync(url, cancelationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return default;
        }
    }
}
