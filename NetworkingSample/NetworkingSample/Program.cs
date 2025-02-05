using System.Net;

namespace NetworkingSample
{
  internal class Program
  {
    static void Main(string[] args)
    {
      //URIDemo();
      //WebClientDemo();
      HttpClientDemo();
      Console.ReadLine();
    }

    private static async void HttpClientDemo()
    {
      try
      {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("http://google.com");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
      }
      catch (Exception ex)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message: " + ex.Message);
      }
    }

    private static void WebClientDemo()
    {
      WebRequest request = WebRequest.Create("http://google.com");
      request.Credentials = CredentialCache.DefaultCredentials;
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      Console.WriteLine("Status: " + response.StatusDescription);
      Console.WriteLine(new string('*', 50));

      Stream dataStream = response.GetResponseStream();
      StreamReader reader = new StreamReader(dataStream);
      string responseFromServer = reader.ReadToEnd();
      Console.WriteLine(responseFromServer);
      Console.WriteLine(new string('*', 50));
      reader.Close();
      dataStream.Close();
      response.Close();
    }

    private static void URIDemo()
    {
      Uri info = new Uri("http://www.domain.com:80/info?id=123#top5");
      Uri page = new Uri("http://www.domain.com/info/page.html");
      Console.WriteLine("Host: " + info.Host);
      Console.WriteLine("Port: " + info.Port);
      Console.WriteLine("PathAndQuery: " + info.PathAndQuery);
      Console.WriteLine("Query: " + info.Query);
      Console.WriteLine("Fragment: " + info.Fragment);
      Console.WriteLine("Default HTTP Port: " + info.Port);
      Console.WriteLine("IsBaseOf: " + info.IsBaseOf(page));

      Uri relative = info.MakeRelativeUri(page);
      Console.WriteLine("IsAbsoluteUri: " + relative.IsAbsoluteUri);
      Console.WriteLine("RelativeUri: " + relative.ToString());
    }
  }
}
