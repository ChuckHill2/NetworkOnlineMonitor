@rem Document creation/editing is performed in Winword.
@rem This batch file will convert it into an entirely self-contained/stand-alone html file.
@rem This in turn, is successfully used in the Winforms WebBrowser control.
@rem Many more features than legacy RTF and the RTFTextBox control.

@rem https://github.com/tobya/DocTo - leverages winword to work from the command-line
docto_64.exe -F AboutInfo.docx -O AboutInfo.htm -T wdFormatFilteredHTML

@rem https://github.com/ChuckHill2/HtmlImageEmbedder
HtmlImageEmbedder.exe AboutInfo.htm

del AboutInfo.bak.htm
rd /s /q AboutInfo_files
move /y AboutInfo.htm ..
