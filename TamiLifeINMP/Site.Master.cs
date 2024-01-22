using System;

namespace TamiLifeSA
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void ActivarMenu(bool activar)
        {
            NavigationMenu.Enabled = activar;
        }

        public void OcultarMenu()
        {
            NavigationMenu.Visible = false;
            HeadLoginView.Visible = false;
        }

        public void CambiarSiteMap(string sitemapName)
        {
            SiteMapDataSource1.SiteMapProvider = sitemapName;
        }
    }
}
