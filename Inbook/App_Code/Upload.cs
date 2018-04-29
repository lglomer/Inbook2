using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// Summary description for Upload
/// </summary>
public class Upload
{
    const string filesGeneralLocation = "~/img/uploads/";
    const string fileCoverLocation = filesGeneralLocation + "covers/";
    const string filePhotoLocation = filesGeneralLocation + "photos/";

    private string fileName, fileExtension, newFileName;
    FileUpload fileUpload;

	public Upload()
	{
	}

    public Upload(FileUpload FileUpload1)
    {
        this.fileUpload = FileUpload1;
        this.fileExtension = Path.GetExtension(FileUpload1.FileName).ToLower();
        this.fileName = Path.GetFileNameWithoutExtension(FileUpload1.FileName).ToLower();
    }
    public void AddCoverPicture()
    {
        UploadService us = new UploadService();
        this.newFileName = us.AddPicture(fileName, fileExtension, "cover");

        string fullPath = HttpContext.Current.Server.MapPath(fileCoverLocation) + this.newFileName + this.fileExtension;
        this.fileUpload.SaveAs(fullPath);
    }

    public string GetLastUploadId()
    {
        return "";
    }

    public bool IsExtensionAllowed ()
    {
        string[] extensions = new string[5];
        extensions[0] = ".jpeg"; extensions[1] = ".jpg"; extensions[2] = ".jif"; extensions[3] = ".jfif"; extensions[4] = ".png";

        for (int i = 0; i < extensions.Length; i++)
        {
            if (extensions[i] == this.fileExtension)
            {
                return true;
            }
        }

        return false;
    }
}