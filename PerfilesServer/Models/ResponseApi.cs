using System.Net;

namespace PerfilesServer.Models
{
  public class ResponseApi<T>
  {
    public bool IsSucces { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ResponseApi(bool isSucces = true, string msg = "", T data = default)
    {
      this.IsSucces = isSucces;
      this.Message = msg;
      this.Data = data;
    }

  }
}
