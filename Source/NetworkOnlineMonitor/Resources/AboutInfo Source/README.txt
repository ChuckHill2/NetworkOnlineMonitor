------------------------------------------------------
Steps to update the update the AboutBox documentation.
------------------------------------------------------

(1) Edit AboutBox.docx with WinWord.

(2) Run Docx2Html.bat
    - This writes out AboutBox.docx as html
    - Embeds the images directly into the html.
    - Moves the resulting html to the parent resources folder.

(3) Recompile NetworkOnlineMonitor to integrate the new html file.
