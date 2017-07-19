using System;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace LongRunningProcess
{
    public class Process : Page
    {
        /// <summary>
        /// Upload page 'Start' button click
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Framed"] != "True")
            {
                // If user has navigated to page manually, redirect
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                // Show progress
                UpdateProgress("Starting");

                // Simulate long-running process
                int rowCount = 1000;
                for (int currentRow = 0; currentRow <= rowCount; currentRow++)
                {
                    Thread.Sleep(15);

                    // Show progress
                    UpdateProgress("Processing " + Math.Round(((double)currentRow / (double)rowCount) * 100) + "%");
                }

                // Show progress
                UpdateProgress("Complete");
            }
        }

        /// <summary>
        /// Method which outputs progress to the user
        /// </summary>
        /// <param name="output">The text to output</param>
        private void UpdateProgress(string output)
        {
            try
            {
                // Write out the parent script callback
                Response.Write(string.Format("<script type=\"text/javascript\">parent.UpdateProgress('{0}');</script>", output));

                // To be sure the response isn't buffered on the server
                Response.Flush();
            }
            catch (HttpException)
            {
                //throw new Exception("HTTP exception");
            }
            catch (Exception)
            {
                //throw new Exception("Other exception");
            }
        }
    }
}