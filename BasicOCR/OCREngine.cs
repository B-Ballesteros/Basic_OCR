using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Office.Interop.OneNote;
using ExtensionMethods;

namespace BasicOCR
{
    class OCREngine
    {
        private const string SECTION = "Section";
        private const string SECTION_ATTRIB = "ID";
        private const string IMAGE_TAG = "Image";
        private const string DATA_TAG = "Data";
        private const string OCRDATA_TAG = "OCRData";
        private const string OCRTEXT_TAG = "OCRText";

        private const int MAX_ATTEMPTS = 3;


        private Application app;

        public OCREngine()
        {
            app = new Application();
        }

        public string Recognize(Image image)
        {
            string result;
            string pageId;
            XDocument document = CreatePage(out pageId);
            var nameSpace = document.Root.Name.Namespace.ToString();
            var element = makeImageElementFrom(nameSpace, image);
            document = addImageElementToDocument(document, element);
            document = updateAppWith(document, pageId);
            result = retrieveText(document, nameSpace, pageId);
            app.DeleteHierarchy(pageId);
            return result;
        }

        private XDocument CreatePage(out string pageId)
        {
            string hierarchy;
            app.GetHierarchy(string.Empty, HierarchyScope.hsPages, out hierarchy);
            var doc = XDocument.Parse(hierarchy);
            pageId = "";
            var section = doc.Descendants().FirstOrDefault(element => element.Name.LocalName.Equals(SECTION));
            if (section == null)
            {
                throw new Exception("No section found");
            }
            var sectionId = section.Attribute(SECTION_ATTRIB).Value;
            app.CreateNewPage(sectionId, out pageId);
            return reloadDocumentWith(pageId);
        }

        private XElement makeImageElementFrom(string nameSpace, Image image)
        {
            var imageElement = new XElement(XName.Get(IMAGE_TAG, nameSpace));
            var dataElement = new XElement(XName.Get(DATA_TAG, nameSpace));
            dataElement.Value = image.ToBase64();
            imageElement.Add(dataElement);
            return imageElement;
        } 
        private XDocument addImageElementToDocument(XDocument doc, XElement ImageElement)
        {
            doc.Root.Add(ImageElement);
            return doc;
        }

        private XDocument updateAppWith(XDocument document, string pageId)
        {
            app.UpdatePageContent(document.ToString());

            return reloadDocumentWith(pageId);
        }


        private XDocument reloadDocumentWith(string pageId)
        {
            string xmlString;
            app.GetPageContent(pageId, out xmlString, PageInfo.piBinaryData);
            return XDocument.Parse(xmlString);
        }

        private string getOCRText(XDocument doc, string nameSpace)
        {
            string text = null;
            var imageElement = doc.Root.Element(XName.Get(IMAGE_TAG, nameSpace));
            var ocrElement = imageElement.Element(XName.Get(OCRDATA_TAG, nameSpace));
            if (ocrElement != null)
            {
                text = ocrElement.Element(XName.Get(OCRTEXT_TAG, nameSpace)).Value;
            }
            return text;
        }

        private string retrieveText(XDocument document, string nameSpace, string pageId)
        {
            string result;
            int total = 0;
            do
            {
                Thread.Sleep(300);
                result = getOCRText(document, nameSpace);
                if (result != null)
                {
                    break;
                }
                document = reloadDocumentWith(pageId);
            } while (total++ < MAX_ATTEMPTS);
            
            return result;
        }
    }
}
