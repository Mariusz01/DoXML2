using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace DoXML2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateData();
                LabelMessage.Text = "Atkualne dane";
            }
        }

        private void PopulateData()
        {
            using(MyDatabaseEntities to = new MyDatabaseEntities())
            {
                GridViewData.DataSource = to.Adresies.ToList();
                GridViewData.DataBind();
            }
        }

        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            if(FileUpload1.PostedFile.ContentType == "aplication/xml" || FileUpload1.PostedFile.ContentType == "text/xml")
            {
                try
                {
                    string fileName = Path.Combine(Server.MapPath("~/UploadDocument"), Guid.NewGuid().ToString() + ".xml");
                    FileUpload1.PostedFile.SaveAs(fileName);

                    XDocument xDoc = XDocument.Load(fileName);
                    List<Adresy> emList = (List<Adresy>)xDoc.Descendants("Adresy").Select(d =>
                     new Adresy
                     {
                         Id = (int)d.Element("Id"),
                         Imie = d.Element("Imie").Value,
                         Nazwisko = d.Element("Nazwisko").Value,
                         Miasto = d.Element("Miasto").Value,
                         Ulica = d.Element("Ulica").Value,
                         NrDomu = d.Element("NrDomu").Value
                     }).ToList();

                    using (MyDatabaseEntities to = new MyDatabaseEntities())
                    {
                        foreach (var i in emList){
                            var v = to.Adresies.Where(a => a.Id.Equals(i.Id)).FirstOrDefault();
                            if (v != null)
                            {
                                v.Imie = i.Imie;
                                v.Nazwisko = i.Nazwisko;
                                v.Miasto = i.Miasto;
                                v.Ulica = i.Ulica;
                                v.NrDomu = i.NrDomu;
                            }
                            else
                            {
                                to.Adresies.Add(i);
                            }
                        }
                        to.SaveChanges();
                    }
                    PopulateData();
                    LabelMessage.Text = "Udany import";
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            using (MyDatabaseEntities to = new MyDatabaseEntities())
            {
                List<Adresy> emList = to.Adresies.ToList();
                if(emList.Count > 0)
                {
                    var xEle = new XElement("Adresies",
                        from emp in emList
                        select new XElement("Adresy",
                        new XElement("Id", emp.Id),
                        new XElement("Imie", emp.Imie),
                        new XElement("Nazwisko", emp.Nazwisko),
                        new XElement("Miasto", emp.Miasto),
                        new XElement("Ulica", emp.Ulica),
                        new XElement("NrDomu", emp.NrDomu)
                        ));
                    HttpContext context = HttpContext.Current;
                    context.Response.Write(xEle);
                    context.Response.ContentType = "application/xml";
                    context.Response.AppendHeader("Content-Disposition", "attachment; filename=AdresData.xml");
                    context.Response.End();
                }
            }
                
        }
    }
}