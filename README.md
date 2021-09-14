# smart-print
Web Service to download and print PDF files

### Web Service
There is no installation needed. To start the Web Service just run `SmartPrint.exe`

### Open settings window
The app will start minimized in the system tray. To open the settings window just double click on the icon or left click and then select restore

### Change printer settings and print test file
From the settings window, you can change the printer and paper and even print a test file

### Download and print
To download a PDF file and send it to the local printer use the following endpoint:
`GET /api/pdf?url={pdfUrl}` 

e.g. https://localhost:5001/api/pdf?url=https%3A%2F%2Fbitcoin.org%2Fbitcoin.pdf

Note that the URL passed in as a parameter needs to be encoded
