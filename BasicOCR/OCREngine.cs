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
        #region Constats
        private const string SECTION = "Section";
        private const string SECTION_ATTRIB = "ID";
        private const string IMAGE_TAG = "Image";
        private const string DATA_TAG = "Data";
        private const string OCRDATA_TAG = "OCRData";
        private const string OCRTEXT_TAG = "OCRText";

        private const int MAX_ATTEMPTS = 3;

        #endregion

        #region Fields
        private Application app;
        #endregion

        #region Constructors
        public OCREngine()
        {
            app = new Application();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Takes an image and uses Microsoft OneNote OCR to try to get the text within the given image.
        /// </summary>
        /// <param name="image">Image to be processed.</param>
        /// <returns>OCR processed text, error message or null if no text is found.</returns>
        public string Recognize(Image image)
        {
            string result = null;
            string pageId = null;
            try
            {
                XDocument document = CreatePage(out pageId); // Creates and gets oneNote xml page.
                var nameSpace = document.Root.Name.Namespace.ToString(); //extract default namespace.
                var element = makeImageElementFrom(nameSpace, image); //Creates image node. 
                document.Root.Add(element);// Inserts node to xml page.
                document = updateAppWith(document, pageId); //Send xml to OneNote to reflect page updates.
                result = retrieveText(document, nameSpace, pageId); //Retrieves OCR text from updated xml.
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            finally
            {
                if (pageId != null)
                {
                    app.DeleteHierarchy(pageId); //Removes page created from OneNote.
                }
            }
            return result;
        }

        /// <summary>
        /// Creates a OneNote page and returns its content in XML format.
        /// </summary>
        /// <param name="pageId">Out parameter. Unique identifier of the page created.</param>
        /// <returns>OneNote page in XML format.</returns>
        private XDocument CreatePage(out string pageId)
        {
            pageId = null;
            string hierarchy; //String to cointain the main Hierarchy from OneNote.
            app.GetHierarchy(string.Empty, HierarchyScope.hsPages, out hierarchy); 
            var doc = XDocument.Parse(hierarchy); //XML representing OneNote main hierarchy.
            var section = doc.Descendants().FirstOrDefault(element => element.Name.LocalName.Equals(SECTION));
            if (section == null) //Make sure that we have the correct hierachy.
            {
                throw new Exception("No section found");
            }
            var sectionId = section.Attribute(SECTION_ATTRIB).Value; //Get required id to create a page inside current OneNote Book.
            app.CreateNewPage(sectionId, out pageId); //Create OneNote page in current book.
            return reloadDocumentWith(pageId); // Return a xml version of the page created.
        }

        /// <summary>
        /// Creates an XML Element using the given namespace containing the provided image as a base64 string.
        /// </summary>
        /// <param name="nameSpace">Namespace in which this element will be contained.</param>
        /// <param name="image">Image to be contained inside the XML element.</param>
        /// <returns>XML Element that contains the base64 string representation of an image.</returns>
        private XElement makeImageElementFrom(string nameSpace, Image image)
        {
            var imageElement = new XElement(XName.Get(IMAGE_TAG, nameSpace)); //Create image tag
            var dataElement = new XElement(XName.Get(DATA_TAG, nameSpace)); //Create data tag
            dataElement.Value = image.ToBase64(); //Add data tag's value (base64 string of the image).
            imageElement.Add(dataElement); //Add data tag inside image tag.
            return imageElement;
        } 

        /// <summary>
        /// Sends a request to OneNote engine to update the content of a page providing an updated xml page version and its unique identifier.
        /// </summary>
        /// <param name="document">XML version of the OneNote page including changes.</param>
        /// <param name="pageId">OneNote's page unique identifier.</param>
        /// <returns>XML version of the OneNote page.</returns>
        private XDocument updateAppWith(XDocument document, string pageId)
        {
            app.UpdatePageContent(document.ToString());

            return reloadDocumentWith(pageId);
        }

        /// <summary>
        /// Sends a request to OneNote engine to obtain the contents of the page of the given identifier.
        /// </summary>
        /// <param name="pageId">Unique identifier of the OneNote page.</param>
        /// <returns>XML version of the page requested.</returns>
        private XDocument reloadDocumentWith(string pageId)
        {
            string xmlString;
            app.GetPageContent(pageId, out xmlString, PageInfo.piBinaryData);
            return XDocument.Parse(xmlString);
        }

        /// <summary>
        /// Parses a XML version of OneNote's page looking for the OCR text Node and returns its value.
        /// </summary>
        /// <param name="doc">XML version of the OneNote's page t obe parsed.</param>
        /// <param name="nameSpace">Namespace used in the XML document.</param>
        /// <returns>Null if no tg is found. Otherwise Tag's value.</returns>
        private string getOCRText(XDocument doc, string nameSpace)
        {
            string text = null;
            var imageElement = doc.Root.Element(XName.Get(IMAGE_TAG, nameSpace)); //Gets the Image element inside XML
            var ocrElement = imageElement.Element(XName.Get(OCRDATA_TAG, nameSpace)); //Gets OCR element inside Image element
            if (ocrElement != null) //null check
            {
                text = ocrElement.Element(XName.Get(OCRTEXT_TAG, nameSpace)).Value; //Text extraction.
            }
            return text;
        }


        /// <summary>
        /// Extracts the text recovered from OneNote's OCR processor. 
        /// </summary>
        /// <param name="document">XML Version of the OneNote'page containing an image.</param>
        /// <param name="nameSpace">Namespace used in the XML.</param>
        /// <param name="pageId">OneNote's page unique identifier.</param>
        /// <returns>OCR text or null if nothing is foud.</returns>
        private string retrieveText(XDocument document, string nameSpace, string pageId)
        {
            string result;
            int total = 0;
            do // Controlled loop to give some time to OneNote to process the contents of the image
            {
                Thread.Sleep(300);
                result = getOCRText(document, nameSpace);
                if (result != null) //Break the loop if content is found before loop's completion.
                {
                    break;
                }
                document = reloadDocumentWith(pageId);
            } while (total++ < MAX_ATTEMPTS); //Loop restrictions.
            
            return result;
        }
        #endregion
    }
}
