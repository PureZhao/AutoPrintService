# AutoPrintService
#### Automatically print Word and PDF in fixed format name in LAN<br>


## Environment
- Visual Studio 2012
- .NetFramework 4.7.1

## How to use
1. Modify the **fileFolder** attribution of CS scripts in the **Threads** folder to the path of that the directory which is used to store documents(Microsoft Word or PDF)
2. Compile to a Windows Application
3. Documents filename is in required format

## Format ***copies,from page to page,distinguishing txt***
- *copies* is a number, you will get *copies* copies of documents from your printer
- *from page to page* is a section of pages that you wanna let the printer print.
- *distinguishing txt* is a customlized text, this is used to distinguish the documents which have the same *copies* and the same *from page to page*
- All parts can not be **Empty**

## Example
- **3,4-5,hfjsjndvf**
   - the number 3 will print 3 copies of documents
   - the section 4-5 will print the document from page 4 to page 5
   - hfjsjndvf is distinguishing part
- **2,0-0,sjcbdkek**
   - the number 2 will print 2 copies of documents
   - the section 0-0 will print the entire document
   - sjcbdkek is distinguishing part

## Notice
- In this repository, the document will be deleted immediately which is not in format filename
- In this repository, process scans this *fileFolder* every 5 seconds
- In this repository, porcess delete the **printed** documents every 10 minutes
- In this repository, when rebooting the process, porcess will delete all documents in *fileFolder*
- In this repository, the PDF file will be forcing to print all pages

If helpful, pleased.

