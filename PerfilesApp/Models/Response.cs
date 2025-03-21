using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfilesApp.Models
{
  public class Response<T>
  {
    public bool isSucces { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public Response(bool isSucces = true, string msg = "", T data = default)
    {
      this.isSucces = isSucces;
      this.Message = msg;
      this.Data = data;
    }
  }
}