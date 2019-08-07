using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPSnippets.FaceBookAPI;


namespace SportGames.Models
{
    public class FB : System.Web.UI.Page
    {
        protected void Post(string msg)
        {
            FaceBookConnect.API_Key = "1170695436457988";
            FaceBookConnect.API_Secret = "446de0936512acb68cab70eaf176c582";
            if (!IsPostBack)
            {

                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    data.Add("message",msg);
                    FaceBookConnect.Post(code, "me/feed", data);
                    

                }
            }
        }


    }
}