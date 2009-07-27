from charmap import charmap
from charmap import getNextChar
from charmap import stringToBytes
from charmap import bytesToString

def buildDict():
  result={}
  result[0]=0
  i=1
  while max(result.keys())!=3792:
    if i!=7934 and i!=7935 and i!=8190 and i!=8191:
      x = i - (i/256)*2
      result[x]=i
    i+=1
  return result

def processPointer(bytes):
  length = ((bytes[0]&0x03) << 3) + ((bytes[1]&0xE0)>>5) + 4
  j = ((bytes[1]&0x1F)<<8) + bytes[2];
  return length, j-(j/256)*2

def buildPointer(dict, jump, length):
  result=[0,0,0]
  l = length - 4
  j = dict[jump]
  result[0] = 0xF0 | ((l&0x18) >> 3)
  result[1] = ((l&0x07) << 5)
  result[1] |= ((j&0x1F00)>>8)
  result[2] = j&0xFF
  return result
  
def decompress(bytes):
  result=[]
  i=0
  while i < len(bytes):
    if bytes[i] >= 0xF0 and bytes[i] <= 0xF3:
      length, jump = processPointer(bytes[i:i+3])
      if (i-jump)<0 or (i-jump+length) >= len(bytes):
        result += bytes[i:i+3]
      else:
        result += bytes[i-jump:i-jump+length]
      i += 2
    else:
      result.append(bytes[i])
    i += 1
  return result

def findBestSubStringInWindow(window, bytes):
  if len(bytes) < 4: return -1,-1
  for i in range(len(bytes),3,-1):
    sub=bytes[:i]
    loc = window.find(sub)
    if (loc > -1):
      return i, len(window)-loc
  return -1,-1

def compress(bytes, windowSize):
  d=buildDict()
  m=max(d.keys())
  result=""
  i = 0
  while i < len(bytes):
    #if i%1000 == 0: print i
    if bytes[i]== "\xfe":
      result += bytes[i]
      i+=1
      continue
    fe = bytes.find("\xfe",i,i+35)
    if fe == -1: fe=i+35
    size, loc = findBestSubStringInWindow(result[0-m:], bytes[i:fe])
    if size!=-1 and loc!=-1:# and not (bytes[i]=="\xfa" and bytes[i+size-1]=="\xfa"):
      a,b,c=buildPointer(d, loc, size)
      result += chr(a)+chr(b)+chr(c)
      i += size
    else:
      result += bytes[i]
      i+=1
  return result

bytes=decompress(stringToBytes(open("WLDHELP.LZW").read()[0x80:]))
s = bytesToString(bytes)
comp=compress(s, max(buildDict().keys()))
dec=decompress(stringToBytes(comp))

print dec==bytes
open("compressed.bin","w").write(comp)
open("decompressed.bin","w").write(bytesToString(dec))