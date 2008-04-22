"""Turns a binary file into a fftpatch file"""

import sys
import base64

def writeToFileWithLinesSplitTo80(fp, s):
  i = 0
  while (i*80+80) < len(s):
    fp.write(s[i*80:(i+1)*80] + "\n") 
    i+=1
  fp.write(s[i*80:] + "\n")

header="""<?xml version="1.0" encoding="utf-8"?>
<patch type="US_PSP">
"""
footer="""</patch>
"""

if len(sys.argv) != 4:
  print "usage: makebase64.py elementName fileName outputFile"
  sys.exit(0)

elementName = sys.argv[1]
fileName = sys.argv[2]
output = sys.argv[3]

bytes = open(fileName).read()
b64 = base64.b64encode(bytes)

fp = open(output, "w")
fp.write(header)
fp.write("<" + elementName + ">\n")
writeToFileWithLinesSplitTo80(fp, b64)
fp.write("\n</" + elementName + ">\n")
fp.write(footer)
fp.close()

